#region Using

using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

#endregion

public partial class admin_Pages_Categories : System.Web.UI.Page
{
	/// <summary>
	/// Handles the Load event of the Page control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.MaintainScrollPositionOnPostBack = true;
        if (!Page.IsPostBack)
        {
            BindGrid();
            BindGrid1();
            LoadParentDropDown(ddlNewParent, null);
        }
        #region 分类
        grid.RowEditing += new GridViewEditEventHandler(grid_RowEditing);
        grid.RowUpdating += new GridViewUpdateEventHandler(grid_RowUpdating);
        grid.RowCancelingEdit += delegate { Response.Redirect(Request.RawUrl); };
        grid.RowDeleting += new GridViewDeleteEventHandler(grid_RowDeleting);
        grid.RowDataBound += new GridViewRowEventHandler(grid_RowDataBound);
        btnAdd.Click += new EventHandler(btnAdd_Click);
        btnAdd.Text = "添加分类";
        valExist.ServerValidate += new ServerValidateEventHandler(valExist_ServerValidate);
        Page.Title = "分类及领域";
        #endregion

        #region 领域
        grid1.RowEditing += new GridViewEditEventHandler(grid1_RowEditing);
        grid1.RowUpdating += new GridViewUpdateEventHandler(grid1_RowUpdating);
        grid1.RowCancelingEdit += delegate { Response.Redirect(Request.RawUrl); };
        grid1.RowDeleting += new GridViewDeleteEventHandler(grid1_RowDeleting);
        valExist1.ServerValidate += new ServerValidateEventHandler(valExist1_ServerValidate);
        #endregion
    }

	void grid_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowState == DataControlRowState.Edit ||
				e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
		{
            Category self = (Category)e.Row.DataItem;
			DropDownList ddlParent = (DropDownList)e.Row.FindControl("ddlParent");
			LoadParentDropDown(ddlParent, self);

			Category temp = (Category)e.Row.DataItem;
			if (temp.Parent != null)
			{
				foreach (ListItem item in ddlParent.Items)
				{
					if (item.Value == temp.Parent.ToString())
					{
						item.Selected = true;
						break;
					}
				}
			}
		}
	}
   
	private void LoadParentDropDown(DropDownList ddl, Category self)
	{
		// Load up the Parent DropDown
		ddl.ClearSelection();
		ddl.Items.Add(new ListItem("none", "0"));
		foreach (Category cat in Category.Categories)
		{
            if (self == null || !cat.Id.Equals(self.Id))
			    ddl.Items.Add(new ListItem(cat.CompleteTitle(), cat.Id.ToString()));
		}
	}

	/// <summary>
	/// Handles the ServerValidate event of the valExist control.
	/// </summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
	private void valExist_ServerValidate(object source, ServerValidateEventArgs args)
	{
		args.IsValid = true;

		foreach (Category category in Category.Categories)
		{
			if (category.Title.Equals(txtNewCategory.Text.Trim(), StringComparison.OrdinalIgnoreCase))
				args.IsValid = false;
		}
	}
    private void valExist1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;

        foreach (Field fld in Field.Fields)
        {
            if (fld.FieldName.Equals(txtNewField.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                args.IsValid = false;
        }
    }
	/// <summary>
	/// Handles the Click event of the btnAdd control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
	void btnAdd_Click(object sender, EventArgs e)
	{
		if (Page.IsValid)
		{
			string description = txtNewNewDescription.Text;
			if (description.Length > 255)
				description = description.Substring(0, 255);

			Category cat = new Category(txtNewCategory.Text, description);
			if (ddlNewParent.SelectedValue != "0")
				cat.Parent = new Guid(ddlNewParent.SelectedValue);

			cat.Save();
			Response.Redirect(Request.RawUrl, true);
		}
	}
    protected void btnAddField_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string description = txtNewFieldDescription.Text;
            if (description.Length > 255)
                description = description.Substring(0, 255);

            Field fld = new Field(txtNewField.Text, description);

            fld.Save();
            Response.Redirect(Request.RawUrl, true);
        }
    }
	/// <summary>
	/// Handles the RowDeleting event of the grid control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
	void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		Guid id = (Guid)grid.DataKeys[e.RowIndex].Value;
		Category cat = Category.GetCategory(id);

		cat.Delete();
		cat.Save();
		Response.Redirect(Request.RawUrl);
	}
    void grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Guid id = (Guid)grid1.DataKeys[e.RowIndex].Value;
        Field fld = Field.GetField(id);

        fld.Delete();
        fld.Save();
        Response.Redirect(Request.RawUrl);
    }
	/// <summary>
	/// Handles the RowUpdating event of the grid control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
	void grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		Guid id = (Guid)grid.DataKeys[e.RowIndex].Value;
		TextBox textboxTitle = (TextBox)grid.Rows[e.RowIndex].FindControl("txtTitle");
		TextBox textboxDescription = (TextBox)grid.Rows[e.RowIndex].FindControl("txtDescription");
		DropDownList ddlParent = (DropDownList)grid.Rows[e.RowIndex].FindControl("ddlParent");
		Category cat = Category.GetCategory(id);
		cat.Title = textboxTitle.Text;
		cat.Description = textboxDescription.Text;
		if (ddlParent.SelectedValue == "0")
			cat.Parent = null;
		else
			cat.Parent = new Guid(ddlParent.SelectedValue);
		cat.Save();

		Response.Redirect(Request.RawUrl);
	}
    void grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Guid id = (Guid)grid1.DataKeys[e.RowIndex].Value;
        TextBox textboxTitle = (TextBox)grid1.Rows[e.RowIndex].FindControl("txtTitle");
        TextBox textboxDescription = (TextBox)grid1.Rows[e.RowIndex].FindControl("txtDescription");
        
        Field fld = Field.GetField(id);
        fld.FieldName = textboxTitle.Text;
        fld.Description = textboxDescription.Text;
        
        fld.Save();

        Response.Redirect(Request.RawUrl);
    }
	/// <summary>
	/// Handles the RowEditing event of the grid control.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
	void grid_RowEditing(object sender, GridViewEditEventArgs e)
	{
		grid.EditIndex = e.NewEditIndex;
		BindGrid();
	}
    void grid1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grid1.EditIndex = e.NewEditIndex;
        BindGrid1();
    }
	/// <summary>
	/// Binds the grid with all the categories.
	/// </summary>
	private void BindGrid()
	{
		grid.DataKeyNames = new string[] { "Id" };
		grid.DataSource = Category.Categories;
		grid.DataBind();
	}
    private void BindGrid1()
    {
        grid1.DataKeyNames = new string[] { "Id" };
        grid1.DataSource = Field.Fields;
        grid1.DataBind();
    }
	protected string GetParentTitle(object item)
	{
		Category temp = (Category)item;
		if (temp.Parent == null)
			return "";
		else
			return Category.GetCategory((Guid)temp.Parent).Title;
	}
    
}

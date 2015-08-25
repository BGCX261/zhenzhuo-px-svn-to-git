using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_Training_GridTrainList : System.Web.UI.UserControl
{
    public string strName;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbCId.Text = Request.Params["cid"];
            lbFId.Text = Request.Params["fid"];
            strName = Request.Params["name"];
            BindGrid();
        }
    }

    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label ltDes = e.Row.Cells[0].FindControl("ltDes") as Label;
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;

            
            Training fd = e.Row.DataItem as Training;
            if (fd!=null)
            {
                aPages.HRef = GetEditHtml(fd.Id.ToString());
                ltDes.Text =  StripString(fd.Content,175, true);
            }
        }
    }
    void BindGrid()
    {
        if (strName != null)
        {
            List<Training> tsName = Training.Trainings.FindAll(
                delegate(Training tg)
                {
                    return tg.IsPublished == true && tg.Title.Contains(strName);
                });
            GridList.DataSource = tsName;
            GridList.RecordCount = tsName.Count;
        }
        else
        {
            if (lbFId.Text != string.Empty)
            {
                List<Training> tsField = Training.GetTrainingsByField(new Guid(lbFId.Text)).FindAll(
                delegate(Training tg)
                {
                    return tg.IsPublished == true;
                });
                GridList.DataSource = tsField;
                GridList.RecordCount = tsField.Count;
            }
            else
            {
                List<Training> tsCategory = Training.GetTrainingsByCategory(new Guid(lbCId.Text)).FindAll(
                delegate(Training tg)
                {
                    return tg.IsPublished == true;
                });
                GridList.DataSource = tsCategory;
                GridList.RecordCount = tsCategory.Count;
            }
        }
        GridList.DataBind();
    }
    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }
    private string GetEditHtml(string id)
    {
        string parentParam = "&";
        if (lbFId.Text != string.Empty)
        {
            Field field = Field.GetField(new Guid(lbFId.Text));
            parentParam = "fid=" + field.Id.ToString() + parentParam;
        }
        else if (lbCId.Text != string.Empty)
        {
            Category category = Category.GetCategory(new Guid(lbCId.Text));
            parentParam = "cid=" + category.Id.ToString() + parentParam;
        }
        else
        { 
            
        }
        
        return Utils.AbsoluteWebRoot + @"Views\TrainingView.aspx?" + parentParam + "id=" + id;
    }

    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
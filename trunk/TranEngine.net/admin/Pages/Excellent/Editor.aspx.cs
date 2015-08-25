using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Resources;
using System.IO;
public partial class admin_Pages_Excellent : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    #region Private members

    private static string _id;
    private static Excellent _excellent;
    public Excellent CurrentExcellent { get { return _excellent; } }
    private bool isFistLoadPage = false;
    private Guid fistLoadPageMastId = new Guid("00000000-0000-0000-0000-000000000000");
    StateList<Res> Ress;

    public bool IsActionNew = false;
    #endregion

    #region methods...
    protected void Page_Load(object sender, EventArgs e)
    {
        txtTitle.Focus();
        #region Regist Callback
        String cbReference =
        Page.ClientScript.GetCallbackEventReference(this,
        "arg", "ReceiveServerData", "context");
        String callbackScript;
        callbackScript = "function CallServer(arg, context)" +
            "{ " + cbReference + ";}";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            "CallServer", callbackScript, true);
        #endregion
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindTeacher();

            _id = HttpContext.Current.Request.QueryString["id"];
            if (_id != null && _id != string.Empty)
            {
                _excellent = Excellent.GetExcellent(new Guid(_id));

            }

            if (_excellent != null)
            {
                ViewState.Add("IsActionNew", false);
                fistLoadPageMastId = _excellent.MastPic;
                isFistLoadPage = true;
                BindExcellent();
            }
            else
            {
                IsActionNew = true;
                ViewState.Add("IsActionNew", true);
            }
        }
        else
        {
            IsActionNew = (bool)ViewState["IsActionNew"];
        }

    }

    private void BindExcellent()
    {
        txtTitle.Text = _excellent.Title;
        ddlObj.SelectedValue = _excellent.Teacher;
        txtTrainingDate.Text = _excellent.TrainingDate.ToString("yyyy-MM-dd");
        txtCityTown.Text = _excellent.CityTown;

        BindGrid();
    }

    private void GetCurrentRess()
    {
        Ress = new StateList<Res>();

        if (_excellent == null)
        {
            _excellent = new Excellent();
            _excellent.DateCreated = DateTime.Now.AddHours(-TrainSettings.Instance.Timezone);

        }
        foreach (Res item in _excellent.Ress)
        {
            Ress.Add(item);
        }


        //foreach (Res titem in _excellent.TempAddRess.Keys)
        //{
        //    if (_excellent.TempAddRess[titem] == "add")
        //    {
        //        Ress.Add(titem);

        //    }
        //    else
        //    {
        //        Ress.Remove(titem);

        //    }
        //}
        //Ress = _excellent.Ress;

    }

    private void BindGrid()
    {
        GetCurrentRess();
        gridComments.DataSource = Ress;
        gridComments.DataBind();

    }

    private void BindTeacher()
    {
        ddlObj.Items.Clear();
        ListItem dli = new ListItem("教师团", "教师团");
        ddlObj.Items.Add(dli);
        foreach (MembershipUser user in Membership.GetAllUsers())
        {

            ListItem li = new ListItem(user.UserName, user.UserName);
            ddlObj.Items.Add(li);
            ddlObj.SelectedValue = User.Identity.Name;
        }
    }

    private void Reload(string isRefresh)
    {
        ClientScript.RegisterClientScriptBlock(GetType(), "ClientScript",
            "<SCRIPT LANGUAGE='JavaScript'>parent.closeEditor(" + isRefresh + ");</SCRIPT>");
    }

    private int SuppliersSelectedIndex
    {
        get
        {
            if (string.IsNullOrEmpty(Request.Form["SuppliersGroup"]))
                return -1;
            else
                return Convert.ToInt32(Request.Form["SuppliersGroup"]);
        }
    }

    public static string GetEditHtml(string id, string page, int width, int height)
    {
        return string.Format("editComment('{0}','" + page + "',{1},{2});return false;", id, width, height);
    }
    #endregion

    #region ICallbackEventHandler 成员

    private string _Callback;

    public string GetCallbackResult()
    {
        return _Callback;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        switch (eventArgument)
        {
            case "checkDelete":
                if (gridComments.Rows.Count == 1)
                {
                    _Callback = "false";
                }
                else
                {
                    _Callback = "true";
                }
                break;
            default:
                if (IsActionNew && _excellent != null)
                {
                    foreach (Res item in _excellent.Ress)
                    {
                        item.Delete();
                        item.Save();
                    }
                }
                
                break;
        }
        
        //_Callback = Utils.RemoveIllegalCharacters(eventArgument.Trim());
    }

    #endregion

    #region Events...
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gridComments.Rows.Count == 0)
        {
            lblMess.Text = "请上传展示图片";
            lblMess.Visible = true;
            return;
        }
        else if (SuppliersSelectedIndex == -1)
        {
            lblMess.Text = "请选择一个上传展示图片作为主显示图片";
            lblMess.Visible = true;
            return;
        }
        else
        {
            lblMess.Visible = false;
        }


        string mId = gridComments.DataKeys[SuppliersSelectedIndex].Value.ToString();
        _excellent.Title = txtTitle.Text;
        _excellent.Teacher = ddlObj.SelectedValue;
        _excellent.CityTown = txtCityTown.Text;
        _excellent.TrainingDate = Convert.ToDateTime(txtTrainingDate.Text);
        _excellent.Author = User.Identity.Name;
        _excellent.MastPic = new Guid(mId);
        _excellent.Save();
        _excellent.Ress.MarkOld();
        Reload("true");

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (IsActionNew && _excellent != null)
        {
            foreach (Res item in _excellent.Ress)
            {
                item.Delete();
                item.Save();
            }
        }
        Reload("true");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GetCurrentRess();
        HttpPostedFile pf = FileUpload.PostedFile;

        int intDocLen = pf.ContentLength;
        string contentType = pf.ContentType;
        if (_excellent.Ress.Count >= 5)
        {
            lblMess.Text = "上传图片文件数目已达上限!";
            lblMess.Visible = true;
            return;
        }
        if (!contentType.ToLower().StartsWith("image"))
        {
            lblMess.Text = "请选择图片类型的文件!";
            lblMess.Visible = true;
            return;
        }
        else
        {
            lblMess.Text = "";
            lblMess.Visible = false;
        }
        byte[] Docbuffer = new byte[intDocLen];
        Stream objStream;

        objStream = pf.InputStream;

        objStream.Read(Docbuffer, 0, intDocLen);


        Res res = new Res();
        res.FileName = Path.GetFileName(pf.FileName);
        res.ResType = pf.ContentType;
        res.Description = "Update by Excellent";
        res.Author = User.Identity.Name;
        res.IsPublished = true;
        res.Save();

        res.CurrentPostFileBuffer = Docbuffer;
        if (res.BlobUpdate() > 0)
        {
            res.CurrentPostFileBuffer = null;
            //_excellent.TempAddRess.Add(res, "Add");
            _excellent.AddRes(res);
            BindGrid();
        }
    }
    protected void gridComments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (gridComments.Rows.Count == 1)
        {
            return;
        }
        Guid id = new Guid(gridComments.DataKeys[e.RowIndex].Value.ToString());
        Res res = Res.GetRes(id);
        _excellent.RemoveRes(res);
        res.Delete();
        res.Save();
        BindGrid();
    }
    protected void gridComments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[e.Row.Cells.Count - 2].Text = string.Format("{0} : {1} {2} <span style='colro:silver'>(上传图片上限为{3})</span>", labels.total, Ress.Count, "文件", "5");
        }

    }
    protected void gridComments_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Literal output = (Literal)e.Row.FindControl("RadioButtonMarkup");

            output.Text = string.Format(
             @"<input type=""radio"" name=""SuppliersGroup"" " +
              @"id=""RowSelector{0}"" value=""{0} """, e.Row.RowIndex);
            if (isFistLoadPage)
            {
                string id = gridComments.DataKeys[e.Row.RowIndex].Value.ToString();
                if (id == string.Empty)
                {
                    return;
                }
                else
                {
                    if (_excellent.MastPic.ToString() == id)
                    {
                        output.Text += @" checked=""checked""";
                    }
                }
            }
            else
            {
                if (SuppliersSelectedIndex == e.Row.RowIndex || (!Page.IsPostBack && e.Row.RowIndex == 0))
                    output.Text += @" checked=""checked""";
            }
            output.Text += " />";
        }


    }
    #endregion


}
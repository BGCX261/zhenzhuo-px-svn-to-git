using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using Resources;

public partial class admin_Pages_Curricula_DataGrid : System.Web.UI.UserControl
{
    static protected List<Curricula> Curriculas;
    protected enum ActionType
    {
        Approve, Delete, SetGold
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        gridComments.PageSize = 10;

        string confirm = "return confirm('{0}');";
        string msg = "";

        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            btnAction.Text = "允许发布";
            msg = string.Format(labels.areYouSure, "发布", "选中的课程");
            btnAction.OnClientClick = string.Format(confirm, msg);
            btnSetGold.Visible = true;
        }
        else if (Request.Path.ToLower().Contains("default.aspx"))
        {
            btnAction.Text = "新增";
            btnAction.ToolTip = "增加新的课程";
            if (!AuthorProfile.GetProfile( Page.User.Identity.Name).IsPrivate)
            {
                btnAction.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnAction.OnClientClick = GetEditHtml((Guid.NewGuid()).ToString(), "Editor.aspx", 1000, 560);
            }
            
            btnSetGold.Visible = false;
        }

        if (!Page.IsPostBack )
        {
            
            if (!String.IsNullOrEmpty(Request.QueryString["delete"]) && Request.QueryString["delete"].Length == 36)
            {
                Guid id = new Guid(Request.QueryString["delete"]);
                DeleteInfo(id);
            }
            BindGrid();


           


        }
    }

    private void DeleteInfo(Guid id)
    {

        CurriculaInfo crlinfo = CurriculaInfo.GetCurriculaInfo(id);
        Curricula crl = Curricula.GetCurriculaByCurriculaInfo(id);
        crl.RemoveCurriculaInfo(crlinfo);
        int i = HttpContext.Current.Request.RawUrl.IndexOf('?');
        Response.Redirect(HttpContext.Current.Request.RawUrl.Substring(0, i));
    }

    #region Binding

    protected void BindGrid()
    {
        List<Curricula> cls;
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            cls = Curricula.Curriculas;
            //cls = Curricula.Curriculas.FindAll(
            //delegate(Curricula c)
            //{
            //    return c.IsPublished == false;
            //});
        }
        else
        {
            if (Page.User.IsInRole("administrators"))
            {
                cls = Curricula.Curriculas;
            }
            else
            {
                cls = Curricula.Curriculas.FindAll(
                    delegate(Curricula c)
                    {
                        return c.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
        }
        // sort in descending order
        cls.Sort(delegate(Curricula c1, Curricula c2)
        { return DateTime.Compare(c2.DateCreated, c1.DateCreated); });
        Curriculas = cls;
        gridComments.DataSource = Curriculas;
        gridComments.DataBind();
    }

    #endregion


    protected void btnSelect_Click(object sender, EventArgs e)
    {
        BindGrid();
        foreach (GridViewRow row in gridComments.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("chkSelect");
            if (cb != null && cb.Enabled)
            {
                cb.Checked = true;
            }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        BindGrid();
        foreach (GridViewRow row in gridComments.Rows)
        {
            CheckBox cb = (CheckBox)row.FindControl("chkSelect");
            if (cb != null && cb.Enabled)
            {
                cb.Checked = false;
            }
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            ProcessSelected(ActionType.Approve);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ProcessSelected(ActionType.Delete);
    }
    protected void btnSetGold_Click(object sender, EventArgs e)
    {
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            ProcessSelected(ActionType.SetGold);
        }
    }
    protected void ProcessSelected(ActionType action)
    {
        List<Curricula> tmp = getTempSelect();

        foreach (Curricula cm in tmp)
        {
            if (action == ActionType.Approve)
            {
                ApproveCurricula(cm);
            }

            if (action == ActionType.Delete)
            {
                RemoveCurricula(cm);
            }
            if (action == ActionType.SetGold)
            {
               SetGoldCurricula(cm);
            }
        }

        BindGrid();
    }
    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        gridComments.PageIndex = e.NewPageIndex;
        gridComments.DataBind();
        
    }
    protected void gridComments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[e.Row.Cells.Count - 2].Text = string.Format("{0} : {1} {2}", labels.total, Curriculas.Count, "课程");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlGenericControl ulPages = e.Row.Cells[4].FindControl("ulPages") as HtmlGenericControl;
            HtmlGenericControl divPages = e.Row.Cells[4].FindControl("divPages") as HtmlGenericControl;
            HtmlAnchor aPages = e.Row.Cells[4].FindControl("aPages") as HtmlAnchor;
            HtmlAnchor aNew = e.Row.Cells[4].FindControl("aNew") as HtmlAnchor;
            aPages.HRef = "javascript:void(ToggleVisibility('" + ulPages.ClientID + "'));";
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aNew.HRef = "javascript:void(0)";
            aNew.Attributes.Add("onClick", GetEditHtml(crl.Id.ToString(), "CurriculaInfo.aspx", 600, 260));
            BindPageList(crl, ulPages, divPages, aPages);
        }
    }

    public static bool HasNoChildrenAndApped(Guid comId)
    {
        Curricula p = Curricula.GetCurricula(comId);
        if (p.IsPublished)
        {
            return false;
        }
        return true;
    }
    public static string GetAppInfo(Guid id)
    {
        string msg = Curricula.GetCurricula(id).IsPublished ? "已发布" : "未发布";
        msg += Curricula.GetCurricula(id).IsGold ? " 金牌" : "";
        return msg;
    }
    public static string GetEditHtml(string id, string page, int width, int height)
    {
        return string.Format("editComment('{0}','" + page + "',{1},{2});return false;", id, width, height);
    }
    

    private void RemoveCurricula(Curricula cm)
    {
        bool found = false;
        for (int i = 0; i < Curricula.Curriculas.Count; i++)
        {

            if (Curricula.Curriculas[i].Id == cm.Id)
            {
                Curricula.Curriculas[i].Delete();
                Curricula.Curriculas[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }

    private void ApproveCurricula(Curricula cm)
    {
        bool found = false;
        for (int i = 0; i < Curricula.Curriculas.Count; i++)
        {

            if (Curricula.Curriculas[i].Id == cm.Id)
            {
                if (!cm.IsPublished)
                {
                    Curricula.Curriculas[i].IsPublished = true;
                }
                else
                {
                    Curricula.Curriculas[i].IsPublished = false;
                }
                Curricula.Curriculas[i].DateModified = DateTime.Now;
                Curricula.Curriculas[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }

    private void SetGoldCurricula(Curricula cm)
    {
        bool found = false;
        for (int i = 0; i < Curricula.Curriculas.Count; i++)
        {

            if (Curricula.Curriculas[i].Id == cm.Id)
            {
                if (!cm.IsGold)
                {
                    Curricula.Curriculas[i].IsGold = true;
                }
                else
                {
                    Curricula.Curriculas[i].IsGold = false;
                }
                Curricula.Curriculas[i].DateModified = DateTime.Now;
                Curricula.Curriculas[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }
    private List<Curricula> getTempSelect()
    {
        List<Curricula> tmp = new List<Curricula>();

        foreach (GridViewRow row in gridComments.Rows)
        {
            try
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkSelect");
                if (cbx != null && cbx.Checked)
                {
                    Curricula crl = Curriculas.Find(
                    delegate(Curricula c)
                    {
                        return c.Id == (Guid)gridComments.DataKeys[row.RowIndex].Value;
                    });

                    if (crl != null) tmp.Add(crl);
                }

            }
            catch (Exception e)
            {
                Utils.Log(string.Format("Error processing selected row in comments data grid: {0}", e.Message));
            }
        }
        return tmp;
    }
    public bool hasSelected
    {
        get
        {
            return getTempSelect().Count > 0;
        }
    }
    public string AreYouSureDelete()
    {

        return string.Format(labels.areYouSure, labels.delete.ToLower(), "选中的课程");
    }

    private void BindPageList(Curricula crl, HtmlGenericControl ulPages, HtmlGenericControl divPages, HtmlAnchor aPages)
    {
        foreach (CurriculaInfo info in crl.CurriculaInfos)
        {
            HtmlGenericControl li = new HtmlGenericControl("li");
            HtmlGenericControl span = new HtmlGenericControl("span");
            //HtmlAnchor a = new HtmlAnchor();
            //a.HRef = "?infoid=" + info.Id.ToString();
            //a.InnerHtml = info.StartDate.ToString("yyyy年MM月dd") + "-" + info.EndDate.ToString("dd日");

            span.InnerText = info.StartDate.ToString("yyyy年MM月dd日") + "-" + info.EndDate.ToString("MM月dd日") +
                " (" + info.CityTown + "  ￥" + info.Cast + ") ";
            span.Attributes.CssStyle.Add("color", "#F3660E");
            //span.Attributes.CssStyle.Add("",)
            //System.Web.UI.LiteralControl text = new System.Web.UI.LiteralControl
            //(" (<span style='color:#F3660E;'>" + info.CityTown + "  ￥" + info.Cast + "<span>) ");

            string deleteText = string.Format(labels.areYouSure, labels.delete.ToLower(), "课程安排"); ;
            HtmlAnchor delete = new HtmlAnchor();
            delete.InnerText = Resources.labels.delete;
            delete.Attributes["onclick"] = "if (confirm('" + deleteText + "')){location.href='?delete=" + info.Id + "'}";
            delete.HRef = "javascript:void(0);";
            delete.Style.Add(System.Web.UI.HtmlTextWriterStyle.FontWeight, "normal");

            li.Controls.Add(span);
            //li.Controls.Add(text);
            li.Controls.Add(delete);

            li.Attributes.CssStyle.Remove("font-weight");

            ulPages.Controls.Add(li);
        }


        divPages.Visible = true;
        aPages.InnerHtml = crl.CurriculaInfos.Count + " 课程安排";
    }


    
}
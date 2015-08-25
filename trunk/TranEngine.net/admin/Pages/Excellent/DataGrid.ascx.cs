using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using Resources;

public partial class admin_Pages_Excellent_DataGrid : System.Web.UI.UserControl
{
    static protected List<Excellent> Excellents;
    protected enum ActionType
    {
        Approve,Delete
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        gridComments.PageSize = 10;

        string confirm = "return confirm('{0}');";
        string msg = "";

        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            btnAction.Text = "允许发布";
            msg = string.Format(labels.areYouSure, "发布", "选中的精彩展示");
            btnAction.OnClientClick = string.Format(confirm, msg);
           
        }
        else if (Request.Path.ToLower().Contains("default.aspx"))
        {
            btnAction.Text = "新增";
            btnAction.ToolTip = "增加新的培训现场";
            if (!AuthorProfile.GetProfile(Page.User.Identity.Name).IsPrivate)
            {
                btnAction.Enabled = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnAction.OnClientClick = GetEditHtml((Guid.NewGuid()).ToString(), "Editor.aspx", 600, 460);
            }
            
           
        }
        
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }

    

    #region Binding

    protected void BindGrid()
    {
        List<Excellent> cls;
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            cls = Excellent.Excellents.FindAll(
            delegate(Excellent c)
            {
                return c.IsPublished == false;
            });
        }
        else
        {
            if (Page.User.IsInRole("administrators"))
            {
                cls = Excellent.Excellents;
            }
            else
            {
                cls = Excellent.Excellents.FindAll(
                    delegate(Excellent c)
                    {
                        return c.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
        }
        // sort in descending order
        cls.Sort(delegate(Excellent c1, Excellent c2)
        { return DateTime.Compare(c2.TrainingDate, c1.TrainingDate); });
        Excellents = cls;
        gridComments.DataSource = Excellents;
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
            e.Row.Cells[e.Row.Cells.Count-2].Text = string.Format("{0} : {1} {2}", labels.total, Excellents.Count, "现场");
        }
        
    }

    public bool HasNoChildrenAndApped(Guid comId)
    {
        Excellent p = Excellent.GetExcellent(comId);
        if (p.IsPublished && Page.User.IsInRole("administrators"))
        {
            return false;
        }
        return true;
    }
    public static string GetAppInfo(Guid id)
    {
        string msg = Excellent.GetExcellent(id).IsPublished ? "已发布" : "未发布";
        return msg;
    }
    public static string GetEditHtml(string id,string page,int width,int height)
    {
        return string.Format("editComment('{0}','"+page+"',{1},{2});return false;", id,width,height);
    }
    protected void ProcessSelected(ActionType action)
    {
        List<Excellent> tmp = getTempSelect();

        foreach (Excellent cm in tmp)
        {
            if (action == ActionType.Approve)
            {
                ApproveExcellent(cm);
            }
            
            if (action == ActionType.Delete)
            {
                RemoveExcellents(cm);
            }
        }

        BindGrid();
    }

    private void RemoveExcellents(Excellent cm)
    {
        bool found = false;
        for (int i = 0; i < Excellent.Excellents.Count; i++)
        {

            if (Excellent.Excellents[i].Id == cm.Id)
            {
                Excellent.Excellents[i].Delete();
                Excellent.Excellents[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }

    private void ApproveExcellent(Excellent cm)
    {
        bool found = false;
        for (int i = 0; i < Excellent.Excellents.Count; i++)
        {

            if (Excellent.Excellents[i].Id == cm.Id)
            {
                if (!cm.IsPublished)
                {
                    Excellent.Excellents[i].IsPublished = true;
                    Excellent.Excellents[i].DateModified = DateTime.Now;
                    Excellent.Excellents[i].Save();
                }

                found = true;
                break;
            }

            if (found) { break; }
        }
    }
    private  List<Excellent> getTempSelect()
    {
        List<Excellent> tmp = new List<Excellent>();

        foreach (GridViewRow row in gridComments.Rows)
        {
            try
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkSelect");
                if (cbx != null && cbx.Checked)
                {
                    Excellent crl = Excellents.Find(
                    delegate(Excellent c)
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
        get{
            return getTempSelect().Count > 0;
        }
    }
    public string AreYouSureDelete()
    {

        return string.Format(labels.areYouSure, labels.delete.ToLower(), "选中的培训现场");
    }

    
}
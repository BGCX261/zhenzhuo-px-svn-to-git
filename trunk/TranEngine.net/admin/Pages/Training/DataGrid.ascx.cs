using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using Resources;
using System.Text;

public partial class admin_Pages_Training_DataGrid : System.Web.UI.UserControl
{
    static protected List<Training> Trainings;
    protected enum ActionType
    {
        Approve,Delete,SetGold
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        gridComments.PageSize = 10;

        string confirm = "return confirm('{0}');";
        string msg = "";

        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            btnAction.Text = "允许发布";
            msg = string.Format(labels.areYouSure, "发布", "选中的内训");
            btnAction.OnClientClick = string.Format(confirm, msg);
            btnSetGold.Visible = true;
        }
        else if (Request.Path.ToLower().Contains("default.aspx"))
        {
            btnAction.Text = "新增";
            btnAction.ToolTip = "增加新的内训";
            if (!AuthorProfile.GetProfile(Page.User.Identity.Name).IsPrivate)
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
        
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }

    

    #region Binding

    protected void BindGrid()
    {
        List<Training> cls;
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            cls = Training.Trainings;
            //cls = Training.Trainings.FindAll(
            //delegate(Training c)
            //{
            //    return c.IsPublished == false;
            //});
        }
        else
        {
            if (Page.User.IsInRole("administrators"))
            {
                cls = Training.Trainings;
            }
            else
            {
                cls = Training.Trainings.FindAll(
                    delegate(Training c)
                    {
                        return c.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
        }
        // sort in descending order
        cls.Sort(delegate(Training c1, Training c2)
        { return DateTime.Compare(c2.DateCreated, c1.DateCreated); });
        Trainings = cls;
        gridComments.DataSource = Trainings;
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
    protected void btnSetGold_Click(object sender, EventArgs e)
    {
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            ProcessSelected(ActionType.SetGold);
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
            e.Row.Cells[e.Row.Cells.Count-2].Text = string.Format("{0} : {1} {2}", labels.total, Trainings.Count, "内训");
        }
        
    }

    public static bool HasNoChildrenAndApped(Guid comId)
    {
        Training p = Training.GetTraining(comId);
        if (p.IsPublished)
        {
            return false;
        }
        return true;
    }
    public static string GetAppInfo(Guid id)
    {
        string msg = Training.GetTraining(id).IsPublished ? "已发布":"未发布";
        msg += Training.GetTraining(id).IsGold ? " 精品" : "";
        return msg;
    }
    public static string GetEditHtml(string id,string page,int width,int height)
    {
        return string.Format("editComment('{0}','"+page+"',{1},{2});return false;", id,width,height);
    }
    protected void ProcessSelected(ActionType action)
    {
        List<Training> tmp = getTempSelect();

        foreach (Training cm in tmp)
        {
            if (action == ActionType.Approve)
            {
                ApproveTraining(cm);
            }
            
            if (action == ActionType.Delete)
            {
                RemoveTraining(cm);
            }
            if (action == ActionType.SetGold)
            {
                SetGoldTraining(cm);
            }
        }

        BindGrid();
    }

    private void RemoveTraining(Training cm)
    {
        bool found = false;
        for (int i = 0; i < Training.Trainings.Count; i++)
        {

            if (Training.Trainings[i].Id == cm.Id)
            {
                Training.Trainings[i].Delete();
                Training.Trainings[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }

    private void ApproveTraining(Training cm)
    {
        bool found = false;
        for (int i = 0; i < Training.Trainings.Count; i++)
        {

            if (Training.Trainings[i].Id == cm.Id)
            {
                if (!cm.IsPublished)
                {
                    Training.Trainings[i].IsPublished = true;
                    Training.Trainings[i].DateModified = DateTime.Now;
                    Training.Trainings[i].Save();
                }

                found = true;
                break;
            }

            if (found) { break; }
        }
    }
    private void SetGoldTraining(Training cm)
    {
       
        bool found = false;
        for (int i = 0; i < Training.Trainings.Count; i++)
        {

            if (Training.Trainings[i].Id == cm.Id)
            {
                if (!cm.IsGold)
                {
                    int goldCount = Training.Trainings.FindAll(delegate(Training tg) { return tg.IsGold; }).Count;
                    if (goldCount >= 6)
                    {
                        String csname = "PopupScript";
                        Type cstype = this.GetType();
                        ClientScriptManager cs = Page.ClientScript;
                        if (!cs.IsStartupScriptRegistered(cstype, csname))
                        {
                            StringBuilder cstext = new StringBuilder();
                            cstext.Append("<script type=text/javascript> alert('仅能设置6个精品课程!') </script>");
                            cs.RegisterStartupScript(cstype, csname, cstext.ToString());
                        }

                        return;
                    }
                    Training.Trainings[i].IsGold = true;
                }
                else
                {
                    Training.Trainings[i].IsGold = false;
                }
                Training.Trainings[i].DateModified = DateTime.Now;
                Training.Trainings[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }
    private  List<Training> getTempSelect()
    {
        List<Training> tmp = new List<Training>();

        foreach (GridViewRow row in gridComments.Rows)
        {
            try
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkSelect");
                if (cbx != null && cbx.Checked)
                {
                    Training crl = Trainings.Find(
                    delegate(Training c)
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

        return string.Format(labels.areYouSure, labels.delete.ToLower(), "选中的内训");
    }


    
}
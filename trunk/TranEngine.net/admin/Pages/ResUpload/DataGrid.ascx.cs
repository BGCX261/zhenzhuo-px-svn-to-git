using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using Resources;
using System.IO;

public partial class admin_Pages_ResUpload_DataGrid : System.Web.UI.UserControl
{
    static protected List<Res> Ress;
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
            msg = string.Format(labels.areYouSure, "发布", "选中的资料");
            btnAction.OnClientClick = string.Format(confirm, msg);
           
        }
        else if (Request.Path.ToLower().Contains("default.aspx"))
        {
            if (!AuthorProfile.GetProfile(Page.User.Identity.Name).IsPrivate)
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
            
            btnAction.Visible = false;
        }
        
        if (!Page.IsPostBack)
        {
            BindGrid();
        }
    }

    

    #region Binding

    protected void BindGrid()
    {
        List<Res> cls;
        if (Request.Path.ToLower().Contains("approved.aspx"))
        {
            cls = Res.Ress.FindAll(
            delegate(Res c)
            {
                return (c.Description != "Update by Excellent" && c.Description != "Profile" && c.IsPublished == false);
            });
        }
        else
        {
            if (Page.User.IsInRole("administrators"))
            {
                cls = Res.Ress.FindAll(
                delegate(Res c)
                {
                    return (c.Description != "Update by Excellent" && c.Description != "Profile");
                });
            }
            else
            {
                cls = Res.Ress.FindAll(
                    delegate(Res c)
                    {
                        return (c.Description != "Update by Excellent" && c.Description != "Profile" && c.Author == Page.User.Identity.Name);//only return Created by Owner
                    });
            }
        }
        // sort in descending order
        cls.Sort(delegate(Res c1, Res c2)
        { return DateTime.Compare(c2.DateCreated, c1.DateCreated); });
        Ress = cls;
        gridComments.DataSource = Ress;
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
            e.Row.Cells[e.Row.Cells.Count-2].Text = string.Format("{0} : {1} {2}", labels.total, Ress.Count, "资料");
        }
        
    }

    public bool HasNoChildrenAndApped(Guid comId)
    {
        Res p = Res.GetRes(comId);
        if (p.IsPublished && Page.User.IsInRole("administrators"))
        {
            return false;
        }
        return true;
    }
    public static string GetAppInfo(Guid id)
    {
        string msg = Res.GetRes(id).IsPublished ? "已发布" : "未发布";
        return msg;
    }
    public static string GetEditHtml(string id,string page,int width,int height)
    {
        return Utils.AbsoluteWebRoot+ page+"?id="+id+";return false;";
    }
    protected void ProcessSelected(ActionType action)
    {
        List<Res> tmp = getTempSelect();

        foreach (Res cm in tmp)
        {
            if (action == ActionType.Approve)
            {
                ApproveRess(cm);
            }
            
            if (action == ActionType.Delete)
            {
                RemoveRess(cm);
            }
        }

        BindGrid();
    }

    private void RemoveRess(Res cm)
    {
        bool found = false;
        for (int i = 0; i < Res.Ress.Count; i++)
        {

            if (Res.Ress[i].Id == cm.Id)
            {
                Res.Ress[i].Delete();
                Res.Ress[i].Save();
                found = true;
                break;
            }

            if (found) { break; }
        }
    }

    private void ApproveRess(Res cm)
    {
        bool found = false;
        for (int i = 0; i < Res.Ress.Count; i++)
        {

            if (Res.Ress[i].Id == cm.Id)
            {
                if (!cm.IsPublished)
                {
                    Res.Ress[i].IsPublished = true;
                    Res.Ress[i].Save();
                }

                found = true;
                break;
            }

            if (found) { break; }
        }
    }
    private  List<Res> getTempSelect()
    {
        List<Res> tmp = new List<Res>();

        foreach (GridViewRow row in gridComments.Rows)
        {
            try
            {
                CheckBox cbx = (CheckBox)row.FindControl("chkSelect");
                if (cbx != null && cbx.Checked)
                {
                    Res crl = Ress.Find(
                    delegate(Res c)
                    {
                        return c.Id == (Guid)gridComments.DataKeys[row.RowIndex].Value;
                    });

                    if (crl != null) tmp.Add(crl);
                }
               
            }
            catch (Exception e)
            {
                Utils.Log(string.Format("Error processing selected row in data grid: {0}", e.Message));
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

        return string.Format(labels.areYouSure, labels.delete.ToLower(), "选中的资料");
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        HttpPostedFile pf = FileUpload.PostedFile;
        int intDocLen = pf.ContentLength;
        string contentType = pf.ContentType;
        byte[] Docbuffer = new byte[intDocLen];

        Stream objStream;
        objStream = pf.InputStream;
        objStream.Read(Docbuffer, 0, intDocLen);

        Res res = new Res();
        res.FileName = Path.GetFileName(pf.FileName);
        res.ResType = pf.ContentType;
        res.Description = txtDesription.Text;
        res.Author = Page.User.Identity.Name;
        res.Points = Convert.ToInt32(txtPoints.Text);
        res.Save();

        res.CurrentPostFileBuffer = Docbuffer;
        if (res.BlobUpdate() > 0)
        {
            res.CurrentPostFileBuffer = null;
            Response.Redirect(Request.RawUrl);
        }
    }
}
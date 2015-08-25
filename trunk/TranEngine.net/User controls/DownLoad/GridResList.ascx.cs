using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
using TrainEngine.Core.Classes;

public partial class User_controls_DownLoad_GridResList : System.Web.UI.UserControl
{
    static protected List<Res> Ress;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
    private void BindGrid()
    {
        List<Res> cls = Res.Ress.FindAll(
                   delegate(Res c)
                   {
                       return (c.Description != "Update by Excellent"
                           && c.Description != "Profile"
                           && c.IsPublished == true
                           );
                   });
        // sort in descending order
        cls.Sort(delegate(Res c1, Res c2)
        { return DateTime.Compare(c2.DateCreated, c1.DateCreated); });
        Ress = cls;
        GridList.DataSource = Ress;
        GridList.RecordCount = Ress.Count;
        GridList.DataBind();
    }
    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label ltDes = e.Row.Cells[0].FindControl("ltDes") as Label;
            LinkButton lbRes = e.Row.Cells[0].FindControl("lbRes") as LinkButton;

            Res res = e.Row.DataItem as Res;
            lbRes.Text = res.FileName;
            lbRes.ToolTip = res.Description;
            lbRes.CommandArgument = res.Id.ToString();
            ltDes.Text = StripString(res.Description, 175, true);
        }
    }
    
    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }

    protected void lbRes_Click(object sender, EventArgs e)
    {

        if (!Page.User.Identity.IsAuthenticated)
        {
            Response.Redirect(Utils.AbsoluteWebRoot + "login.aspx?Err=" + Server.HtmlEncode("您还未登录!") + "&rUrl=" + Server.UrlPathEncode(Request.RawUrl));
        }
        else
        {
            string id = ((LinkButton)sender).CommandArgument;
            AuthorProfile ap = AuthorProfile.GetProfile(Page.User.Identity.Name);
            Res res = Res.GetRes(new Guid(id));
            if (Convert.ToInt32(ap.Points) >= res.Points)
            {
                Response.Redirect(Utils.AbsoluteWebRoot + "GetRes.aspx?id=" + id);
            }
            else
            {
                String scriptText = "<script language='JavaScript'>alert('下载需要积分:"
                     + res.Points + " 您当前积分(" + ap.Points + ")不足!')</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(),
                    "alert", scriptText);

            }

        }
    }
    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
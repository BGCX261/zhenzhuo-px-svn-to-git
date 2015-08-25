using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;

public partial class User_controls_DownLoad_GrideResNews : System.Web.UI.UserControl
{
    
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
                           && c.IsPublished == true);
                   });
        // sort in descending order
        cls.Sort(delegate(Res c1, Res c2)
        { return DateTime.Compare(c2.DateCreated, c1.DateCreated); });
        List<Res> Ress = new List<Res>(10);
        int limit = 0;
        for (int i = 0; i < cls.Count; i++)//only top 10 display
        {
            if (limit<=10)
            {
                Ress.Add(cls[i]);
                limit++;
            }
        }
        
        GridList.DataSource = Ress;
        GridList.DataBind();
    }


    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 19) { _s = s.ToString().Substring(0, 17) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbRes = e.Row.Cells[0].FindControl("lbRes") as LinkButton;
            Res res = e.Row.DataItem as Res;
            lbRes.Text = res.FileName;
            lbRes.ToolTip = res.Description;
            lbRes.CommandArgument = res.Id.ToString();
        }
    }
    protected void lbRes_Click(object sender, EventArgs e)
    {

        if (!Page.User.Identity.IsAuthenticated)
        {
            Response.Redirect(Utils.AbsoluteWebRoot + "login.aspx?Err=" + Server.HtmlEncode("未登录") + "&rUrl=" + Server.UrlPathEncode(Request.RawUrl));
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
    
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using TrainEngine.Core.Classes;

public partial class User_controls_Teacher_GridOrgansList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    void BindGrid()
    {
        List<AuthorProfile> aps = new List<AuthorProfile>();
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
            AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
            if (ap!=null && ap.IsOrgan  && ap.IsPrivate && !ap.IsAdmin)
            {
                aps.Add(ap);                
            }            
        }
        GridList.DataSource = aps;
        GridList.DataBind();
    }

    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            HtmlAnchor a1 = e.Row.Cells[0].FindControl("a1") as HtmlAnchor;

            AuthorProfile crl = ((AuthorProfile)e.Row.DataItem) as AuthorProfile;
            aPages.HRef = GetEditHtml(crl.UserName.ToString());
            a1.HRef = GetEditHtml(crl.UserName.ToString());

            Label ltDes = e.Row.Cells[0].FindControl("ltDes") as Label;
            Label ltAddress = e.Row.Cells[0].FindControl("ltAddress") as Label;
            AuthorProfile fd = e.Row.DataItem as AuthorProfile;
            if (fd != null)
            {
                if (fd.AboutMe == null)
                {
                    ltDes.Text = "";
                }
                else
                {
                    ltDes.Text = StripString(fd.AboutMe, fd.AboutMe.Length, true);
                    ltDes.Text = jup(ltDes.Text);
                }
                if (fd.Address == null)
                {
                    ltAddress.Text = "";
                }
                else
                {
                    ltAddress.Text = fd.Address;
                }
            }
        }
    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\OrgansView.aspx?id=" + id;
    }

    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 85) { _s = s.ToString().Substring(0, 83) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
    public string SetImageUrl(object ResId)
    {
        if (ResId == null || ResId == string.Empty)
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";
           
        }
        
        Res rs = Res.GetRes(new Guid(ResId.ToString()));
        if (rs != null)
        {
            return rs.GetResTempFilePath();
        }
        else
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";
        }
    }
    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
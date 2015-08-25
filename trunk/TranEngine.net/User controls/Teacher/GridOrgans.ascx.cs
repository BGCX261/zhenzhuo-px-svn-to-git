using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_Teacher_GridOrgans : System.Web.UI.UserControl
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
        List<AuthorProfile> aps = new List<AuthorProfile>();
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
            AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
            if (ap!=null && ap.IsOrgan && !ap.IsAdmin && ap.IsPrivate)
            {
                aps.Add(ap);
            }
        }
        int iCount = aps.Count;
        if (iCount >= 10)
        {
            for (int i = iCount; i > 10; i--)
            {
                aps.RemoveAt(i - 1);
            }
        }
        
        GridView1.DataSource = aps;
        GridView1.DataBind();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            AuthorProfile crl = ((AuthorProfile)e.Row.DataItem) as AuthorProfile;
            aPages.HRef = GetEditHtml(crl.UserName.ToString());            
        }
    }
    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\OrgansView.aspx?id=" + id;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 20) { _s = s.ToString().Substring(0, 18) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
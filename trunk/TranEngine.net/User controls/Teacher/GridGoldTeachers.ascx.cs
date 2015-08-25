using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using System.Web.Security;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;

public partial class User_controls_Teacher_GridGoldTeachers : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }

    private void bindGrid()
    {
        List<AuthorProfile> tchs = new List<AuthorProfile>();
        int count = 0;
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
            AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
            if (ap!=null && ap.IsTeacher && !ap.IsAdmin && ap.IsPrivate && ap.IsGoldTch)
            {
                tchs.Add(ap);
                count++;
                if (count==3)
                {
                    break;
                }
            }
        }
        GridTeachers.DataSource = tchs;
        GridTeachers.DataBind();
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


    
    protected string StripString(object s, int len)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }
    protected void GridTeachers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            HtmlAnchor aTch = e.Row.Cells[0].FindControl("aTch") as HtmlAnchor;
            AuthorProfile ap = e.Row.DataItem as AuthorProfile;
            if (ap != null)
            {
                aPages.HRef = aTch.HRef = GetEditHtml(ap.UserName);
                
            }
        }
    }

    private string GetEditHtml(string userName)
    {
        return Utils.AbsoluteWebRoot + @"Views\TeacherView.aspx?uid=" + userName;
    }
}
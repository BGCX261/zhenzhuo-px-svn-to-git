using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

public partial class User_controls_Teacher_GridHotTeachers : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Table table = new Table();
            table.Style.Add(HtmlTextWriterStyle.Width, "100%");
            table.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");

            PlaceHolder.Controls.Add(table);
            TableRow tr = new TableRow();
            table.Rows.Add(tr);
            List<AuthorProfile> aplist = new List<AuthorProfile>();
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
                if (ap == null)
                {
                    continue;
                }
                aplist.Add(ap);
            }
            aplist.Sort(delegate(AuthorProfile a1, AuthorProfile a2)
                {
                    return a2.ViewCount.CompareTo(a1.ViewCount);
                });
            int num = 0;
            int rowCellCount = 0;
            for (int i = 0; i < aplist.Count; i++)
            {
                AuthorProfile ap = aplist[i];
                if (ap.IsTeacher && !ap.IsAdmin && ap.IsPrivate && num < 9)
                {
                    TableCell tc = new TableCell();
                    tc.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                    tc.Text = "<div><img style='border: 1px solid #C0C0C0' width='70px' height='72px' src='" + SetImageUrl(ap.PhotoURL) + "'  /></div><div><a href='" + Utils.AbsoluteWebRoot + @"Views\TeacherView.aspx?uid=" + ap.UserName + "'>" + ap.DisplayName + "</a></div>";
                    if (rowCellCount < 3)
                    {
                        tr.Cells.Add(tc);
                        rowCellCount++;
                        num++;
                    }
                    if (rowCellCount == 3 && num != 9)
                    {
                        tr = new TableRow();
                        table.Rows.Add(tr);
                        rowCellCount = 0;
                    }
                }
            }
        }
    }

    public string SetImageUrl(string ResId)
    {
        if (ResId == null || ResId == string.Empty)
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";

        }
        Guid id;
        try
        {
            id = new Guid(ResId);
        }
        catch
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png"; ;
        }
        Res rs = Res.GetRes(new Guid(ResId));
        if (rs != null)
        {
            return rs.GetResTempFilePath();
        }
        else
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";
        }
    }
}
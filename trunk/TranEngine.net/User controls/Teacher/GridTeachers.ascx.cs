using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;
using System.Web.Security;

public partial class User_controls_Teacher_GridTeachers : System.Web.UI.UserControl
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
        List<Field> fields = Field.Fields;

        GridTeachers.DataSource = fields;
        GridTeachers.DataBind();
    }
    protected void GridTeachers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            PlaceHolder ltTchs = e.Row.Cells[1].FindControl("ltTchs") as PlaceHolder;
            HtmlAnchor aMore = e.Row.Cells[0].FindControl("aMore") as HtmlAnchor;
            Field fd = e.Row.DataItem as Field;
            if (fd != null)
            {
                aPages.HRef = aMore.HRef = Utils.AbsoluteWebRoot + @"TeacherList.aspx?fid=" + fd.Id;
                int num = 0;
                foreach (MembershipUser user in Membership.GetAllUsers())
                {
                    AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
                    if (ap!=null && ap.IsTeacher && !ap.IsAdmin && ap.IsPrivate && ap.Fields.Contains(fd.Id.ToString()) && num<9)
                    {
                        HtmlAnchor aTch = new HtmlAnchor();
                        aTch.HRef = Utils.AbsoluteWebRoot + @"Views\TeacherView.aspx?uid=" + ap.UserName;
                        aTch.Title = ap.DisplayName;
                        aTch.InnerText = StripString(ap.DisplayName, 4);
                        //aTch.Style.Add(HtmlTextWriterStyle.Color, "#333333");
                        ltTchs.Controls.Add(aTch);

                        Literal lt = new Literal();
                        lt.Text = " ";
                        ltTchs.Controls.Add(lt);
                        num++;
                    }
                }
            }
        }
    }

    protected string StripString(object s, int len)
    {
        if (s ==null )
        {
            return string.Empty;
        }
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }
}
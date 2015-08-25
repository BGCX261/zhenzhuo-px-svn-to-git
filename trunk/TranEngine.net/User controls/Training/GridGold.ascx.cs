using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class User_controls_Training_GridGold : System.Web.UI.UserControl
{

    public bool isTopShow = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        List<Training> tList = Training.Trainings.FindAll(
            delegate(Training tg)
            {
                return tg.IsPublished == true && tg.IsGold == true;
            });
        if (!isTopShow)
        {
            GridFields.Columns[1].Visible = false;
        }
        GridFields.DataSource = tList;
        GridFields.DataBind();
    }

    protected string jup(object s)
    {
        int len = 19;
        if (isTopShow)
        {
            len = 23;
        }
        string _s = "";
        if (s.ToString().Trim().Length > len) { _s = s.ToString().Substring(0, len-2) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }


    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Training fd = e.Row.DataItem as Training;
            aPages.HRef = GetEditHtml(fd.Id.ToString());
            if (isTopShow)
            {
                HtmlAnchor aTch = e.Row.Cells[1].FindControl("aTch") as HtmlAnchor;
                aTch.HRef = GetProfileHtml(fd.Teacher);
            }
            

        }
    }

    private string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\TrainingView.aspx?id=" + id;
    }

    private string GetProfileHtml(string name)
    {
         AuthorProfile ap = AuthorProfile.GetProfile(name);
         if (ap != null)
         {
             return Utils.AbsoluteWebRoot + @"Views\TeacherView.aspx?uid=" + name;
         }
         else
         {
             return "javascript:void(0);";
         }
    }

    public string GetTeacherDisplayName(string name)
    {
        AuthorProfile ap = AuthorProfile.GetProfile(name);
        if (ap != null)
        {
            return ap.DisplayName;
        }
        else
        {
            return name;
        }
    }
}
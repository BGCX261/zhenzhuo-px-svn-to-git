using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class User_controls_Teacher_GridTeacherTraining : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        List<Training> tList = Training.Trainings.FindAll(
            delegate(Training tg)
            {
                return tg.IsPublished == true && tg.Teacher == Request["uid"];
            });


        GridFields.DataSource = tList;
        GridFields.DataBind();
    }

    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;

            Training fd = e.Row.DataItem as Training;
            aPages.HRef = GetEditHtml(fd.Id.ToString());


        }
    }
    private string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\TrainingView.aspx?id=" + id;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 19) { _s = s.ToString().Substring(0, 17) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
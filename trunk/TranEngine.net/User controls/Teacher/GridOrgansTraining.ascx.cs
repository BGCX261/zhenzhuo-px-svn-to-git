using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
public partial class User_controls_Teacher_GridOrgansTraining : System.Web.UI.UserControl
{
    private string strID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["id"] != null)
            {
                strID = Request.Params["id"];
            }
            BindGrid();
        }

    }

    private void BindGrid()
    {
        List<Training> tList = Training.Trainings.FindAll(
            delegate(Training tg)
            {
                return tg.IsPublished == true && tg.Author == strID;
            });
        tList.Sort(delegate(Training p1, Training p2) { return Comparer<int>.Default.Compare(p2.ViewCount, p1.ViewCount); });

        int iCount = tList.Count;
        if (iCount >= 9)
        {
            for (int i = iCount; i > 9; i--)
            {
                tList.RemoveAt(i - 1);
            }
        }
        GridView1.DataSource = tList;
        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
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
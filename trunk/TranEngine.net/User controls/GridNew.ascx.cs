using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
public partial class User_controls_GridNew : System.Web.UI.UserControl
{
    //最新公开课数据控件
    public string strNew;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void BindGrid()
    {
        List<Curricula> cls = Curricula.Curriculas.FindAll(
           delegate(Curricula c)
           {
               return (c.IsPublished == true);
           });
        cls.Sort(delegate(Curricula p1, Curricula p2) { return Comparer<DateTime>.Default.Compare(p2.DateCreated, p1.DateCreated); });

        int iCount = cls.Count;
        for (int i = iCount; i > 0; i--)
        {
            if (cls[i - 1].CurriculaInfos.Count == 0)
            {
                cls.RemoveAt(i - 1);
            }
        }
        iCount = cls.Count;
        if (iCount >= 1)
        {
            for (int i = iCount; i > 1; i--)
            {
                cls.RemoveAt(i - 1);
            }
        }

        GridView1.DataSource = cls;
        GridView1.DataBind();
    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\CurriculaView.aspx?id=" + id;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Curricula tc = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(tc.Id.ToString());

            string strMonth = tc.CurriculaInfos[0].StartDate.Month.ToString();
            if (strMonth.Length == 1)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = tc.CurriculaInfos[0].StartDate.Day.ToString();
            if (strDay.Length == 1)
            {
                strDay = "0" + strDay;
            }
            string strYear = tc.CurriculaInfos[0].StartDate.Year.ToString();
            strNew = strYear + "年" + strMonth + "月" + strDay + "日/";
            strNew += tc.CurriculaInfos[0].CityTown.ToString();
        }
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 22) { _s = s.ToString().Substring(0, 20) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
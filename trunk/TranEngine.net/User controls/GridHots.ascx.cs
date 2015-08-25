using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridHots : System.Web.UI.UserControl
{
    /// <summary>
    /// 热门公开课数据控件
    /// </summary>
    public string pSource;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        List<Curricula> cls= Curricula.Curriculas.FindAll(
           delegate(Curricula c)
           {
               return c.IsPublished == true;
           });
        cls.Sort(delegate(Curricula p1, Curricula p2) { return Comparer<int>.Default.Compare(p2.ViewCount, p1.ViewCount); });

        int iCount = cls.Count;
        if (pSource == "left")
        {
            if (iCount >= 10)
            {
                for (int i = iCount; i > 10; i--)
                {
                    cls.RemoveAt(i - 1);
                }
            }
        }
        else
        {
            if (iCount >= 12)
            {
                for (int i = iCount; i > 12; i--)
                {
                    cls.RemoveAt(i - 1);
                }
            }
        }
        GridView1.DataSource = cls;
        GridView1.DataBind();

    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\CurriculaView.aspx?id="+id;
    }
    
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(crl.Id.ToString());
        }
    }

    protected string jup(object s)
    {
        string _s = "";
        if (pSource == "left")
        {
            if (s.ToString().Trim().Length > 22) { _s = s.ToString().Substring(0, 20) + "..."; }
            else { _s = s.ToString(); }
        }
        else
        {
            if (s.ToString().Trim().Length > 20) { _s = s.ToString().Substring(0, 18) + "..."; }
            else { _s = s.ToString(); }
        }
        return _s;
    }
}
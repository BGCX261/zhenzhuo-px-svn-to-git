using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridSellWell : System.Web.UI.UserControl
{
    //畅销公开课数据表格控件
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {

        List<Curricula> cls = Curricula.Curriculas.FindAll(
           delegate(Curricula c)
           {
               return (c.IsPublished == true) && (c.IsGold == true);
           });
        int iCount = cls.Count;

        if (iCount >= 9)
        {
            for (int i = iCount; i > 9; i--)
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
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 12) { _s = s.ToString().Substring(0, 10) + "..."; }
        else { _s = s.ToString(); }
        return _s;
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
}
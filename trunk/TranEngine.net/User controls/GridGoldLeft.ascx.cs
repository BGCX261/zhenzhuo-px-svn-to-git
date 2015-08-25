using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
public partial class User_controls_GridGoldLeft : System.Web.UI.UserControl
{
    public string pSource;

    //金牌公开课数据控件，用于显示在LEFT区域
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
        if (pSource == "left")
        {
            if (iCount >= 7)
            {
                for (int i = iCount; i > 7; i--)
                {
                    cls.RemoveAt(i - 1);
                }
            }
        }
        else
        {
            if (iCount >= 9)
            {
                for (int i = iCount; i > 9; i--)
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
        return Utils.AbsoluteWebRoot + @"Views\CurriculaView.aspx?id=" + id;
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[1].FindControl("aPages") as HtmlAnchor;
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(crl.Id.ToString());      
        }
    }

    protected string jup(object s)
    {
        string _s = "";
        if (pSource == "left")
        {
            if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
            else { _s = s.ToString(); }
        }
        else
        {
            if (s.ToString().Trim().Length > 19) { _s = s.ToString().Substring(0, 17) + "..."; }
            else { _s = s.ToString(); }
        }
        return _s;
    }
}
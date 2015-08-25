using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridGold : System.Web.UI.UserControl
{
    //金牌公开课数据控件，用于显示在MIDDLE区域
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
        for (int i = iCount; i > 0; i--)
        {
            if (cls[i - 1].CurriculaInfos.Count == 0)
            {
                cls.RemoveAt(i - 1);
            }
        }
        iCount = cls.Count;
        if (iCount >= 12)
        {
            for (int i = iCount; i > 12; i--)
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
            HtmlAnchor aPages = e.Row.Cells[1].FindControl("aPages") as HtmlAnchor;
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(crl.Id.ToString());

            Curricula tc = e.Row.DataItem as Curricula;
            //绑定开课日期
            Label lblStartDate = e.Row.Cells[2].FindControl("lblStartDate") as Label;
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
            lblStartDate.Text = strMonth + "月" + strDay + "日/";
           
            //绑定地点
            Label lblCityTown = e.Row.Cells[3].FindControl("lblCityTown") as Label;            
            lblCityTown.Text = tc.CurriculaInfos[0].CityTown.ToString();
            if (lblCityTown.Text.Length > 2)
            {
                lblCityTown.Text = lblCityTown.Text.Substring(0, 2);
            }

            //绑定Cast
            Label lbc = e.Row.Cells[4].FindControl("lbCast") as Label;
            lbc.Text = "￥" + tc.CurriculaInfos[0].Cast.ToString();

        }
    }

    protected string jup(object s)
    {
        string _s = "";
        //byte[] subbyte = System.Text.Encoding.Default.GetBytes(s.ToString());
        //if (subbyte.Length > 46)
        //{
        //    _s = System.Text.Encoding.Default.GetString(subbyte, 0, 42)+"...";
        //}
        //else
        //{ _s = s.ToString(); }


        if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }

}
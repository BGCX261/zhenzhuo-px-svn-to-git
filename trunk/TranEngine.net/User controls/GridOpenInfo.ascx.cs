using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridOpenInfo : System.Web.UI.UserControl
{
    //开课信息表格控件
    public string Cid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Cid= Request.Params["id"];
        }
        BindGrid();
    }

    private void BindGrid()
    {
        List<CurriculaInfo> cls = CurriculaInfo.CurriculaInfos.FindAll(
            delegate(CurriculaInfo cc)
            {
                return cc.CurriculaId.ToString() == Cid && cc.Curricula.IsPublished==true;
            }
            ); 

        //List<Curricula> cls = Curricula.Curriculas.FindAll(
        //   delegate(Curricula c)
        //   {
        //       return (c.IsPublished == true) && (c.Id.ToString() == Cid);
        //   });
        int iCount = cls.Count;

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

            CurriculaInfo tc = e.Row.DataItem as CurriculaInfo;
            //绑定开课日期
            Label lblStartDate = e.Row.Cells[1].FindControl("lblStartDate") as Label;
            string strYear = tc.StartDate.Year.ToString();
            string strMonth = tc.StartDate.Month.ToString();
            //string strYear = tc.CurriculaInfos[0].StartDate.Year.ToString();
            //string strMonth = tc.CurriculaInfos[0].StartDate.Month.ToString();
            if (strMonth.Length == 1)
            {
                strMonth = "0" + strMonth;
            }
            string strDay = tc.StartDate.Day.ToString();
            if (strDay.Length == 1)
            {
                strDay = "0" + strDay;
            }
            lblStartDate.Text = strYear + "年" + strMonth + "月" + strDay + "日——";
            strYear = tc.EndDate.Year.ToString();
            strMonth = tc.EndDate.Month.ToString();
            if (strMonth.Length == 1)
            {
                strMonth = "0" + strMonth;
            }
            strDay = tc.EndDate.Day.ToString();
            if (strDay.Length == 1)
            {
                strDay = "0" + strDay;
            }
            lblStartDate.Text += strYear + "年" + strMonth + "月" + strDay + "日";

            DateTime dtStart = tc.StartDate;
            DateTime dtEnd = tc.EndDate;
            int iTotalDays = dtEnd.Subtract(dtStart).Days+1;
            Label lblTotalDays = e.Row.Cells[2].FindControl("lblTotalDays") as Label;
            lblTotalDays.Text = iTotalDays.ToString()+"天";


            //绑定地点
            Label lblCityTown = e.Row.Cells[3].FindControl("lblCityTown") as Label;
            lblCityTown.Text = tc.CityTown.ToString();

            //绑定Cast
            Label lbc = e.Row.Cells[4].FindControl("lbCast") as Label;
            lbc.Text = "￥" + tc.Cast.ToString();

            //绑定当前状态
            Label lblState = e.Row.Cells[5].FindControl("lblState") as Label;
            if (DateTime.Now > dtStart)
            {
                lblState.Text = "已过期";
            }
            else
            {
                lblState.Text = "报名中";
            }

            //绑定积分
            Label lblPoint = e.Row.Cells[6].FindControl("lblPoint") as Label;
            lblPoint.Text = tc.Curricula.Points.ToString()+"分";

            //绑定培训币
            Label lblScore = e.Row.Cells[7].FindControl("lblScore") as Label;
            lblScore.Text = tc.Curricula.Scores.ToString() + "币";

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

    protected string SetCid(object s)
    {
        Cid = s.ToString();
        return Cid;
    }

}
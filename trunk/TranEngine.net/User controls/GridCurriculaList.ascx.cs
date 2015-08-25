using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridCurriculaList : System.Web.UI.UserControl
{
    private int iParamType;
    public string strDateID, strCityName, strFieldID, strCategoryID, strName;
    public string strPosition;
    DateTime dtOpenStart, dtOpenEnd;
    public string strTime, strDays, strCity, strCost; 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["DateID"] != null)
            {
                strDateID = Request.Params["DateID"];
                iParamType = 1;
                strPosition = GetType1Str(strDateID);
            }
            if (Request.Params["CityName"] != null)
            {
                strCityName = Request.Params["CityName"];
                iParamType = 2;
                strPosition = GetType2Str(strCityName);
            }
            if (Request.Params["FieldID"] != null)
            {
                strFieldID = Request.Params["FieldID"];
                iParamType = 3;
                strPosition = Field.GetField(new Guid(strFieldID)).FieldName;
            }
            if (Request.Params["CategoryID"] != null)
            {
                strCategoryID = Request.Params["CategoryID"];
                iParamType = 4;
                strPosition = Category.GetCategory(new Guid(strCategoryID)).Title;
            }
            if (Request.Params["name"] != null)
            {
                strName = Request.Params["name"];
                iParamType = 5;
                strPosition = "搜索结果";
            }
            BindGrid();
        }
    }
    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\CurriculaView.aspx?id=" + id;
    }
    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            CurriculaInfo crl = ((CurriculaInfo)e.Row.DataItem) as CurriculaInfo;
            aPages.HRef = GetEditHtml(crl.CurriculaId.ToString());

            Label ltDes = e.Row.Cells[0].FindControl("ltDes") as Label;
            Label lbStartDate = e.Row.Cells[0].FindControl("lbStartDate") as Label;
            Label lbCast = e.Row.Cells[0].FindControl("lbCast") as Label;
            Label lbCity = e.Row.Cells[0].FindControl("lbCity") as Label;
            Label lbDays = e.Row.Cells[0].FindControl("lbDays") as Label;
            CurriculaInfo fd = e.Row.DataItem as CurriculaInfo;
            if (fd != null)
            {                
                ltDes.Text = StripString(fd.Curricula.Content, 175, true);
                lbStartDate.Text = fd.StartDate.ToShortDateString();
                lbCast.Text = fd.Cast.ToString();
                lbCity.Text = fd.CityTown.ToString();
                int iDays = fd.EndDate.Subtract(fd.StartDate).Days + 1;
                lbDays.Text = iDays.ToString();
            }

        }
    }
    public string GetTitle(string p_ID)
    {
        return CurriculaInfo.GetCurriculaInfo(new Guid(p_ID)).Curricula.Title;
    }
    void BindGrid()
    {
        switch (iParamType)
        {
            case 1:
                List<CurriculaInfo> infos = CurriculaInfo.CurriculaInfos.FindAll(
                   delegate(CurriculaInfo cinfo)
                   {
                       return cinfo.StartDate >= dtOpenStart && cinfo.StartDate <= dtOpenEnd;
                   });
                infos.Sort(
                    delegate(CurriculaInfo i1, CurriculaInfo i2)
                    {
                        return Comparer<DateTime>.Default.Compare(i2.StartDate, i1.StartDate);
                    });
                GridList.DataSource = infos;
                GridList.RecordCount = infos.Count;
                break;
            case 2:
                List<CurriculaInfo> infos1 = CurriculaInfo.CurriculaInfos.FindAll(
                   delegate(CurriculaInfo cinfo)
                   {
                       return cinfo.CityTown == strPosition;
                   });
                infos1.Sort(
                    delegate(CurriculaInfo i1, CurriculaInfo i2)
                    {
                        return Comparer<DateTime>.Default.Compare(i2.StartDate, i1.StartDate);
                    });
                GridList.DataSource = infos1;
                GridList.RecordCount = infos1.Count;
                break;
            case 3:
                Field cField = Field.GetField(new Guid(strFieldID));
                List<CurriculaInfo> infos2 = CurriculaInfo.CurriculaInfos.FindAll(
                   delegate(CurriculaInfo cinfo)
                   {
                       return cinfo.Curricula.Fields.Contains(cField);
                   });
                infos2.Sort(
                    delegate(CurriculaInfo i1, CurriculaInfo i2)
                    {
                        return Comparer<DateTime>.Default.Compare(i2.StartDate, i1.StartDate);
                    });
                GridList.DataSource = infos2;
                GridList.RecordCount = infos2.Count;
                break;
            case 4:
                Category cCategory = Category.GetCategory(new Guid(strCategoryID));
                List<CurriculaInfo> infos3 = CurriculaInfo.CurriculaInfos.FindAll(
                   delegate(CurriculaInfo cinfo)
                   {
                       return cinfo.Curricula.Categories.Contains(cCategory);
                   });
                infos3.Sort(
                    delegate(CurriculaInfo i1, CurriculaInfo i2)
                    {
                        return Comparer<DateTime>.Default.Compare(i2.StartDate, i1.StartDate);
                    });
                GridList.DataSource = infos3;
                GridList.RecordCount = infos3.Count;
                break;
            case 5:
                List<CurriculaInfo> infos5 = CurriculaInfo.CurriculaInfos.FindAll(
                   delegate(CurriculaInfo cinfo)
                   {
                       return cinfo.Curricula.Title.Contains(strName);
                   });
                infos5.Sort(
                    delegate(CurriculaInfo i1, CurriculaInfo i2)
                    {
                        return Comparer<DateTime>.Default.Compare(i2.StartDate, i1.StartDate);
                    });
                GridList.DataSource = infos5;
                GridList.RecordCount = infos5.Count;
                break;
        }
        GridList.DataBind();
    }
    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }
    private string GetType1Str(string strT)
    {
        string strRes = "";
        switch (strT)
        {
            case "1":
                strRes = "一月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/1/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/1/31");

                break;
            case "2":
                strRes = "二月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/2/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/2/28");
                break;
            case "3":
                strRes = "三月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/3/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/3/31");
                break;
            case "4":
                strRes = "四月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/4/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/4/30");
                break;
            case "5":
                strRes = "五月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/5/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/5/31");
                break;
            case "6":
                strRes = "六月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/6/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/6/30");
                break;
            case "7":
                strRes = "七月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/7/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/7/31");
                break;
            case "8":
                strRes = "八月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/8/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/8/31");
                break;
            case "9":
                strRes = "九月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/9/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/9/30");
                break;
            case "10":
                strRes = "十月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/10/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/10/31");
                break;
            case "11":
                strRes = "十一月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/11/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/11/30");
                break;
            case "12":
                strRes = "十二月";
                dtOpenStart = DateTime.Parse(DateTime.Now.Year.ToString() + "/12/1");
                dtOpenEnd = DateTime.Parse(DateTime.Now.Year.ToString() + "/12/31");
                break;

        }
        return strRes;
    }

    private string GetType2Str(string strT)
    {
        string strRes = "武汉地区课程列表";
        switch (strT)
        {
            case "1":
                strRes = "武汉";
                break;
            case "2":
                strRes = "上海";
                break;
            case "3":
                strRes = "北京";
                break;
            case "4":
                strRes = "广州";
                break;
            case "5":
                strRes = "重庆";
                break;
            case "6":
                strRes = "西安";
                break;
            case "7":
                strRes = "沈阳";
                break;
            case "8":
                strRes = "昆明";
                break;
            case "9":
                strRes = "海口";
                break;
            case "10":
                strRes = "深圳";
                break;
            case "11":
                strRes = "南京";
                break;
            case "12":
                strRes = "天津";
                break;
            case "13":
                strRes = "乌鲁木齐";
                break;
            case "14":
                strRes = "呼和浩特";
                break;
            case "15":
                strRes = "杭州";
                break;
            case "16":
                strRes = "成都";
                break;
            default:
                strRes = "武汉";
                break;
        }
        return strRes;
    }

    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
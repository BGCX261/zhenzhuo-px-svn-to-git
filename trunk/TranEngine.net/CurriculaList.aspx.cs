using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;

public partial class CurriculaList : TrainEngine.Core.Web.Controls.TrainBasePage
{
    private int iParamType;
    public string strDateID, strCityName, strFieldID, strCategoryID,strName;
    public string strPosition;
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
            this.Title = strPosition;
        }
    }

    private string GetType1Str(string strT)
    {
        string strRes = "";
        switch (strT)
        { 
            case "1":
                strRes = "一月";
                break;
            case "2":
                strRes = "二月";
                break;
            case "3":
                strRes = "三月";
                break;
            case "4":
                strRes = "四月";
                break;
            case "5":
                strRes = "五月";
                break;
            case "6":
                strRes = "六月";
                break;
            case "7":
                strRes = "七月";
                break;
            case "8":
                strRes = "八月";
                break;
            case "9":
                strRes = "九月";
                break;
            case "10":
                strRes = "十月";
                break;
            case "11":
                strRes = "十一月";
                break;
            case "12":
                strRes = "十二月";
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
                strRes = "武汉地区课程列表";
                break;
            case "2":
                strRes = "上海地区课程列表";
                break;
            case "3":
                strRes = "北京地区课程列表";
                break;
            case "4":
                strRes = "广州地区课程列表";
                break;
            case "5":
                strRes = "重庆地区课程列表";
                break;
            case "6":
                strRes = "西安地区课程列表";
                break;
            case "7":
                strRes = "沈阳地区课程列表";
                break;
            case "8":
                strRes = "昆明地区课程列表";
                break;
            case "9":
                strRes = "海口地区课程列表";
                break;
            case "10":
                strRes = "深圳地区课程列表";
                break;
            case "11":
                strRes = "南京地区课程列表";
                break;
            case "12":
                strRes = "天津地区课程列表";
                break;
            case "13":
                strRes = "乌鲁木齐课程列表";
                break;
            case "14":
                strRes = "呼和浩特课程列表";
                break;
            case "15":
                strRes = "杭州地区课程列表";
                break;
            case "16":
                strRes = "成都地区课程列表";
                break;
            default:
                strRes = "武汉地区课程列表";
                break;
        }
        return strRes;
    }

}
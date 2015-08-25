using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;

public partial class Views_TrainList :  TrainEngine.Core.Web.Controls.TrainBasePage
{
    public string strPosition;
    public bool isFields = false;
    string fieldId;
    string categoryId,strName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["fid"] != null)
        {
            fieldId = Request.Params["fid"];
            strPosition = Field.GetField(new Guid(fieldId)).FieldName;
            isFields = true;
        }
        else if (Request.Params["cid"] != null)
        {
            categoryId = Request.Params["cid"];
            strPosition = Category.GetCategory(new Guid(categoryId)).Title;
        }
        else if (Request.Params["name"] != null)
        {
            strName = Request.Params["name"];
            strPosition = "搜索结果";
        }
        this.Title = strPosition;
    }
}
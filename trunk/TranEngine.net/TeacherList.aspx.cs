using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;

public partial class TeacherList : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public string strPosition;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["fid"] != null)
        {
            string fieldId = Request.Params["fid"];
            strPosition = Field.GetField(new Guid(fieldId)).FieldName;
        }
        this.Title = strPosition;
    }
}
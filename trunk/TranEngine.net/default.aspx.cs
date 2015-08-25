using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class _Default : TrainEngine.Core.Web.Controls.TrainBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string strSearch = txtSearch.Text;
        string strType = DropDownList1.Text;
        string strURL = "";
        if (strType == "公开课程")
        {
            strURL = Utils.AbsoluteWebRoot + @"CurriculaList.aspx?name=" + strSearch;
        }
        else
        { 
            strURL = Utils.AbsoluteWebRoot + @"TrainList.aspx?name=" + strSearch;         
        }
        Response.Redirect(strURL);

    }
}
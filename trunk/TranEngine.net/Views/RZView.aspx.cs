using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_RZView : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public RzViewContent rvc;
    public string pagestring;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string title = Request["title"];
            string type = Request["type"];
            string ptype = Request["ptype"]==null?"":Request["ptype"];
            if (type == "产品认证" || type == "产品认证收费")
            {
                pagestring = "Rzcp.aspx";
            }
            else if (type == "体系认证" || type == "体系认证收费")
            {
                pagestring = "Rztx.aspx";
            }

            rvc = RZSource.Init.GetRzSourceByType(type, ptype).Find(
                delegate(RzViewContent rz)
                {
                    return rz.Title == title;
                });
        }
    }
}
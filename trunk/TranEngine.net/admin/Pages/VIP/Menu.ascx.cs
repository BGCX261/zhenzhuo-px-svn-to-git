using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Resources;

public partial class admin_Pages_VIP_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildMenuList();
    }

    protected void BuildMenuList()
    {
        string cssClass = "";
        string tmpl = "<a href=\"{0}.aspx\" class=\"{1}\"><span>{2}</span></a>";


        HtmlGenericControl inbx = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("default.aspx") ? "current" : "";
        inbx.InnerHtml = string.Format(tmpl, "Default", cssClass, "学员");
        if (Request.Path.ToLower().Contains("default.aspx"))
        {
            hdr.InnerHtml = string.Format("{0}: {1}", "VIP管理", "学员");
        }
        UlMenu.Controls.Add(inbx);


        HtmlGenericControl tech = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("teachers.aspx") ? "current" : "";
        tech.InnerHtml = string.Format(tmpl, "Teachers", cssClass, "教师");
        if (Request.Path.ToLower().Contains("teachers.aspx"))
        {
            hdr.InnerHtml = string.Format("{0}: {1}", "VIP管理", "教师");
        }
        UlMenu.Controls.Add(tech);

        HtmlGenericControl orgn = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("organs.aspx") ? "current" : "";
        orgn.InnerHtml = string.Format(tmpl, "Organs", cssClass, "机构");
        if (Request.Path.ToLower().Contains("organs.aspx"))
        {
            hdr.InnerHtml = string.Format("{0}: {1}", "VIP管理", "机构");
        }
        UlMenu.Controls.Add(orgn);
    }
}
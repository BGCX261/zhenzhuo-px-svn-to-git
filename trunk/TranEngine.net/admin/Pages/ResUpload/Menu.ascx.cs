using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Resources;

public partial class admin_Pages_ResUpload_Menu : System.Web.UI.UserControl
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
        inbx.InnerHtml = string.Format(tmpl, "Default", cssClass, labels.inbox);
        if (Request.Path.ToLower().Contains("default.aspx"))
        {
            hdr.InnerHtml = string.Format("{0}: {1}", "文件上传管理", labels.inbox);
        }
        UlMenu.Controls.Add(inbx);

        if (this.Page.User.IsInRole("administrators"))
        {
            HtmlGenericControl appr = new HtmlGenericControl("li");
            cssClass = Request.Path.ToLower().Contains("approved.aspx") ? "current" : "";
            appr.InnerHtml = string.Format(tmpl, "Approved", cssClass, labels.approved);
            if ( Request.Path.ToLower().Contains("approved.aspx"))
            {
                hdr.InnerHtml = string.Format("{0}: {1}", "文件上传管理", labels.approved);
            }
            UlMenu.Controls.Add(appr);
        }
    }
}
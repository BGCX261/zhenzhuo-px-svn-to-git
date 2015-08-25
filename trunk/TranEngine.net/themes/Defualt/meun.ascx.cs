using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Resources;
using TrainEngine.Core;

public partial class themes_Defualt_meun : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildMenuList();
    }

        
        
        
        //<li><a href="<%=Utils.AbsoluteWebRoot %>contact.aspx"><%=Resources.labels.contact %></a></li>
        //<li><a href="<%=Utils.FeedUrl %>" class="feed"><img src="<%=Utils.AbsoluteWebRoot %>pics/rssButton.gif" alt="Feed" /><%=Resources.labels.subscribe %></a></li>
    protected void BuildMenuList()
    {
        string cssClass = "";
        string tmpl = "<a href=\"{0}.aspx\" {2}>{1}</a>";
        HtmlGenericControl left = new HtmlGenericControl("li");
        left.Attributes.Add("Class", "nav_left");
        nav.Controls.Add(left);

        HtmlGenericControl inbx = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("default.aspx") ? "nav_current" : "";
        inbx.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot +"Default", labels.home,"");
        if (cssClass != string.Empty) inbx.Attributes.Add("Class", cssClass);
        nav.Controls.Add(inbx);

        HtmlGenericControl cul = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("curricula") ? "nav_current" : "";
        cul.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Curricula", labels.openCurricula, "");
        if (cssClass != string.Empty) cul.Attributes.Add("Class", cssClass);
        nav.Controls.Add(cul);

        HtmlGenericControl tng = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("training") ? "nav_current" : "";
        tng.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Training", labels.internalTraining, "");
        if (cssClass != string.Empty) tng.Attributes.Add("Class", cssClass);
        nav.Controls.Add(tng);

        HtmlGenericControl rztx = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("rztx.aspx") ? "nav_current" : "";
        rztx.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Rztx", labels.String2, "");
        if (cssClass != string.Empty) rztx.Attributes.Add("Class", cssClass);
        nav.Controls.Add(rztx);

        HtmlGenericControl rzcp = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("rzcp.aspx") ? "nav_current" : "";
        rzcp.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Rzcp", labels.String3, "");
        if (cssClass != string.Empty) rzcp.Attributes.Add("Class", cssClass);
        nav.Controls.Add(rzcp);

        HtmlGenericControl tch = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("teachers.aspx") ? "nav_current" : "";
        tch.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Teachers", labels.teachers, "");
        if (cssClass!=string.Empty) tch.Attributes.Add("Class", cssClass);
        nav.Controls.Add(tch);

        HtmlGenericControl org = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("organs.aspx") ? "nav_current" : "";
        org.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Organs", labels.organs, "");
        if (cssClass != string.Empty) org.Attributes.Add("Class", cssClass);
        nav.Controls.Add(org);

        HtmlGenericControl exc = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("excellent.aspx") ? "nav_current" : "";
        exc.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Excellent", labels.excellent, "");
        if (cssClass != string.Empty) exc.Attributes.Add("Class", cssClass);
        nav.Controls.Add(exc);

        HtmlGenericControl dwn = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("download.aspx") ? "nav_current" : "";
        dwn.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "Download", labels.download, "");
        if (cssClass != string.Empty) dwn.Attributes.Add("Class", cssClass);
        nav.Controls.Add(dwn);


        HtmlGenericControl con = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains("contact.aspx") ? "nav_current" : "";
        con.InnerHtml = string.Format(tmpl, Utils.AbsoluteWebRoot + "contact", labels.contact, "");
        if (cssClass != string.Empty) con.Attributes.Add("Class", cssClass);
        nav.Controls.Add(con);

        HtmlGenericControl feed = new HtmlGenericControl("li");
        cssClass = Request.Path.ToLower().Contains(Utils.FeedUrl) ? "nav_current" : "";
        feed.InnerHtml = string.Format("<a href=\"{0}\" {2}>{1}</a>", Utils.FeedUrl, "<img src=\"" + Utils.AbsoluteWebRoot + "pics/rssButton.gif\" alt=\"Feed\" />" + labels.subscribe, "class=\"feed\"");
        if (cssClass != string.Empty) feed.Attributes.Add("Class", cssClass);
        nav.Controls.Add(feed);

        HtmlGenericControl right = new HtmlGenericControl("li");
        right.Attributes.Add("Class", "nav_right");
        nav.Controls.Add(right);
    }
}
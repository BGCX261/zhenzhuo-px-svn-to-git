using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;

public partial class Views_ExcellentView : TrainEngine.Core.Web.Controls.TrainBasePage
{

    public Excellent ex;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbID.Text = Request.Params["eid"];
        ex = Excellent.GetExcellent(new Guid(lbID.Text));
        this.Title = ex.Title;
        SetPics(lbID.Text);
    }

    private void SetPics(string eid)
    {
        List<Res> rs = ex.Ress.FindAll(
            delegate(Res r)
            {
                return r.Id != ex.MastPic;
            });

        foreach (Res item in rs)
        {
            string tmpl = "<a target='_blank' href='{0}' title='{1}'><img src='{0}' width='150px' height='120px' alt='{1}'/></a>";
            HtmlGenericControl li = new HtmlGenericControl("li");
            li.InnerHtml = string.Format(tmpl, SetImageUrl(item.Id), ex.Title);
            li.Attributes.Add("Class", "li1");
            picList.Controls.Add(li);
        }
       
    }
    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }

    public string SetImageUrl(object ResId)
    {
        if (ResId == null || ResId.ToString() == string.Empty)
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";

        }

        Res rs = Res.GetRes(new Guid(ResId.ToString()));
        if (rs != null)
        {
            return rs.GetResTempFilePath();
        }
        else
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";
        }
    }
}
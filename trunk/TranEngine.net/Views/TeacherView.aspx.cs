using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

public partial class Views_TeacherView : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public AuthorProfile tch;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbID.Text = Request.Params["uid"];
        tch = AuthorProfile.GetProfile(lbID.Text);
        this.Title = tch.DisplayName;
        if (Request.Cookies["tchViewCount_" + lbID.Text] == null)
        {
            HttpCookie MyCookie = new HttpCookie("tchViewCount_" + lbID.Text);
            DateTime now = DateTime.Now;

            MyCookie["IP"] = Request.UserHostAddress;
            MyCookie["tid"] = lbID.Text;
            MyCookie.Expires = now.AddHours(1);

            Response.Cookies.Add(MyCookie);
            tch.ViewCount++;
            tch.Save();
        }
    }

    public string GetFieldsString(string fields)
    {
        string[] fs = fields.Split('|');
        string newFstring = string.Empty;
        for (int i = 0; i < fs.Length; i++)
        {
            newFstring += Field.GetField(new Guid(fs[i]))+" ";
        }
        return newFstring;
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
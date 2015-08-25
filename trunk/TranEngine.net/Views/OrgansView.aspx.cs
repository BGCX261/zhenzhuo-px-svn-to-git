using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

public partial class Views_OrgansView : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public string strCity,strGSJS,strFWGKH,strTitle;
    public AuthorProfile ap;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["id"] != null)
            {
                lbID.Text = Request.Params["id"];
            }
            ap = AuthorProfile.GetProfile(lbID.Text);
            //机构名称
            lbTitle.Text = ap.Company;
            strTitle = ap.Company;
            strCity = ap.Address;
            strGSJS = ap.AboutMe;
            strFWGKH = ap.Description1;
            this.Title = ap.Company;
            

            if (Request.Cookies["OrgansViewCount_" + lbID.Text] == null)
            {
                HttpCookie MyCookie = new HttpCookie("OrgansViewCount_" + lbID.Text);
                DateTime now = DateTime.Now;

                MyCookie["IP"] = Request.UserHostAddress;
                MyCookie["tid"] = lbID.Text;
                MyCookie.Expires = now.AddHours(1);

                Response.Cookies.Add(MyCookie);
                ap.ViewCount++;
                ap.Save();
            }  
        }
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
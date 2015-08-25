using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using System.IO;
using TrainEngine.Core.Classes;
using System.Web.Security;

public partial class reg_regorg : TrainEngine.Core.Web.Controls.TrainBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fudPhoto.Attributes.Add("onfocus", "fnt_on('td_logo')");
            fudPhoto.Attributes.Add("onblur", "fnt_logo()");
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        string reg_uid = Request["reg_uid"];
        Membership.CreateUser(Request["reg_uid"], Request["reg_pwd1"], Request["reg_email"]);

        AuthorProfile pc = new AuthorProfile(reg_uid);
        pc.DisplayName = Request["reg_xingming"];
        
        //pc.Company = Request["reg_company"];
        if (Request["reg_phone2"] != string.Empty)
        {
            pc.PhoneMain = (Request["reg_phone1"].Trim() != string.Empty ? Request["reg_phone1"] + "-" : "") + Request["reg_phone2"] + (Request["reg_phone3"].Trim() != string.Empty ? "-" + Request["reg_phone3"] : "");
        }

        pc.PhoneMobile = Request["reg_mobile"];
        if (Request["reg_fax2"] != string.Empty)
        {
            pc.PhoneFax = (Request["reg_fax1"].Trim() != string.Empty ? Request["reg_fax1"] + "-" : "") + Request["reg_fax2"];
        }
        pc.CityTown = Request["reg_shi"]; 
        pc.MSN_QQ = Request["reg_qqmsn"];
        pc.Company = Request["reg_company"];
        pc.Address = Request["reg_address"];
        pc.AboutMe = Request["reg_jianjie"];
        pc.Description1 = Request["reg_kehu"];

        
        Uppic(pc);
        pc.Save();

        Roles.AddUserToRole(reg_uid, "organs");

        Response.Redirect(Utils.AbsoluteWebRoot + "reg/regok.aspx?uType=org", true);
    }
    #region 上传图片相关

    protected void Uppic(AuthorProfile ap)
    {
        if (fudPhoto.FileName == string.Empty)
        {
            return;
        }
        HttpPostedFile pf = fudPhoto.PostedFile;
        int intDocLen = pf.ContentLength;
        string contentType = pf.ContentType;

        byte[] Docbuffer = new byte[intDocLen];

        Stream objStream;
        objStream = pf.InputStream;
        objStream.Read(Docbuffer, 0, intDocLen);

        Res res = new Res();
        res.FileName = Path.GetFileName(pf.FileName);
        res.ResType = pf.ContentType;
        res.Description = "Profile";
        res.Author = ap.UserName;
        res.Points = 0;
        res.Save();

        res.CurrentPostFileBuffer = Docbuffer;
        res.BlobUpdate();

        res.CurrentPostFileBuffer = null;

        ap.PhotoURL = res.Id.ToString();
    }
    #endregion
}
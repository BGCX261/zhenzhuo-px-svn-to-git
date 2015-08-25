using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TrainEngine.Core;

public partial class reg_regstd : TrainEngine.Core.Web.Controls.TrainBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        string reg_uid = Request["reg_uid"];
        Membership.CreateUser(Request["reg_uid"], Request["reg_pwd1"],  Request["reg_email"]);
        //reg_nicheng reg_company
        //reg_phone1 reg_phone2 reg_phone3 reg_mobile reg_qqmsn
        AuthorProfile pc = new AuthorProfile(reg_uid);
        pc.DisplayName = Request["reg_nicheng"];
        pc.CityTown = Request["reg_shi"];
        pc.Company = Request["reg_company"];
        if (Request["reg_phone2"]!=string.Empty)
        {
            pc.PhoneMain =  Request["reg_phone1"].Trim() != string.Empty ?Request["reg_phone1"] + "-":"" + Request["reg_phone2"] + Request["reg_phone3"].Trim() != string.Empty ? "-" + Request["reg_phone3"] : "";
        }
        
        pc.PhoneMobile = Request["reg_mobile"];
        pc.MSN_QQ = Request["reg_qqmsn"];
        pc.IsPrivate = true;//学员注册直接审核通过
        pc.Save();

        Roles.AddUserToRole(reg_uid, "students");
        Response.Redirect(Utils.AbsoluteWebRoot + "reg/regok.aspx?uType=std", true);
    }
}
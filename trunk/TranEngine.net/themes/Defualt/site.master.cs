using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            lbluser.Text = "欢迎"+Page.User.Identity.Name+"访问";
            aLogin.InnerText = Resources.labels.logoff;
            aLogin.HRef = TrainEngine.Core.Utils.RelativeWebRoot + "login.aspx?logoff";
            aReg.Visible = false;
        }
        else
        {
            lbluser.Text = "欢迎游客访问";
            aLogin.HRef = TrainEngine.Core.Utils.RelativeWebRoot + "login.aspx";
            aLogin.InnerText = Resources.labels.login;
            aReg.HRef = TrainEngine.Core.Utils.RelativeWebRoot + "regist.aspx";
        }
    }
}

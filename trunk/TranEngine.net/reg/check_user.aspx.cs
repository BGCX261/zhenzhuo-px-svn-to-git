using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class reg_check_user : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userType = Request["user"];
        string type = Request["type"];
        string value = Request["txt_value"];
       
        switch (type)
        {
            case "uid":
                if ( Membership.GetUser(value)!=null)
                {
                    Response.Write("用户存在!");

                }
                else
                {
                    Response.Write("none");
                }
                break;
            case "email":
                int count = 0;
                foreach (MembershipUser item in  Membership.GetAllUsers())
                {
                    if (item.Email ==value)
                    {
                        count++;
                        break;
                       
                    }
                }
                if (count>0)
                {
                    Response.Write("email存在!");
                }
                else
                {
                    Response.Write("none");
                }
                break;
            default:
                break;
        }
    }
}
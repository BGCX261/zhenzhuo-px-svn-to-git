using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

public partial class GetRes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect(Utils.AbsoluteWebRoot + "login.aspx?Err=" + Server.HtmlEncode("未登录") + "&rUrl=" + Server.UrlPathEncode(Request.RawUrl));
                
            }
            else
            {
                Res rs = Res.GetRes(new Guid(Request.Params["id"]));
                byte[] buff = rs.CurrentPostFileBuffer;
                outPutFile(buff, rs.FileName);

                //积分增减
                AuthorProfile apSource = AuthorProfile.GetProfile(rs.Author);
                apSource.Points = (Convert.ToInt32(apSource.Points) + rs.Points).ToString();
                apSource.Save();
                AuthorProfile apDown = AuthorProfile.GetProfile(this.User.Identity.Name);
                apSource.Points = (Convert.ToInt32(apSource.Points) - rs.Points).ToString();
                apSource.Save();
            }
            
        }
    }


    void outPutFile(byte[] buff, string fileName)
    {
        Response.AddHeader("content-type", "application/x-msdownload");

        Response.AddHeader("Content-Disposition", "attachment;filename="

        + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(fileName)));

        Response.Flush();
        Response.BinaryWrite(buff);
        Response.End();
        
    }
   
}
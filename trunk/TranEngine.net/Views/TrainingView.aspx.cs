using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Net;

public partial class Views_TrainingView : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public string paramString, paramName, TeacherString,TeacherDes;

    public Training cTraining;
    protected void Page_Load(object sender, EventArgs e)
    {
        lbID.Text = Request.Params["id"];
        string fid = Request.Params["fid"];
        string cid = Request.Params["cid"];
        if (fid != null && fid != string.Empty)
        {
            paramString = "fid=" + fid;
            paramName = Field.GetField(new Guid(fid)).FieldName;
        }
        else if (cid != null && cid != string.Empty)
        {
            paramString = "cid=" + cid;
            paramName = Category.GetCategory(new Guid(cid)).Title;
        }
        else
        {
            paramName = paramString = string.Empty;

        }
        cTraining = Training.GetTraining(new Guid(lbID.Text));
        this.Title = cTraining.Title;
        AuthorProfile ap = AuthorProfile.GetProfile(cTraining.Teacher);
        if (ap != null)
        {
            TeacherString = "<a href='./TeacherView.aspx?uid=" + ap.UserName + "'>" + ap.DisplayName + "</a>";
            TeacherDes = ap.AboutMe;
            pnltch.Visible = true;
        }
        else
        {
            pnltch.Visible = false;
            TeacherString = cTraining.Teacher;
        }
        if (Request.Cookies["TrainViewCount_" + lbID.Text] == null)
        {
            HttpCookie MyCookie = new HttpCookie("TrainViewCount_" + lbID.Text);
            DateTime now = DateTime.Now;

            MyCookie["IP"] = Request.UserHostAddress;
            MyCookie["tid"] = lbID.Text;
            MyCookie.Expires = now.AddHours(1);

            Response.Cookies.Add(MyCookie);
            cTraining.ViewCount++;
            cTraining.UpdateViewCount();
        }
    }
}
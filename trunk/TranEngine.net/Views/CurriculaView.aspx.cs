using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;

public partial class Views_CurriculaView : TrainEngine.Core.Web.Controls.TrainBasePage
{
    private string strFieldID;
    public string FieldID;
    public string CName;
    public string zsdx, kcxq, qkc, hkc, qid, hid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Params["id"] == null)
            {
                return;
            }
            lbID.Text = Request.Params["id"];
            if (Request.Params["FieldID"] != null)
            {
                strFieldID = Request.Params["FieldID"];
            }
            else
            {
                strFieldID = "";
            }

            Curricula cl = Curricula.GetCurricula(new Guid(lbID.Text));
            this.Title = cl.Title;
            //信息
            StateList<CurriculaInfo> cis = cl.CurriculaInfos;
            foreach (CurriculaInfo item in cis)
            {
                DateTime start = item.StartDate;
                DateTime end = item.EndDate;
                int cast = item.Cast;
                string CityTown = item.CityTown;
                //积分
                int points = cl.Points;
                //培训币   
                int scores = cl.Scores;
            }
            //前课程
            Curricula pr = cl.Previous;
            if (pr == null)
            {
                qkc = "无";
                qid = lbID.Text;
            }
            else
            {
                qkc = pr.Title;
                qid = pr.Id.ToString();
            }
            //后课程
            Curricula nt = cl.Next;
            if (nt == null)
            {
                hkc = "无";
                hid = lbID.Text;
            }
            else
            {
                hkc = nt.Title;
                hid = nt.Id.ToString();
            }
            //
            CName = cl.Title;
            lbTitle.Text = cl.Title;
            //对象
            zsdx = cl.ObjectDes;
            //介绍
            kcxq = cl.Content;
            //领域
            if (strFieldID == "")
            {
                Field fd = cl.Fields[0];
                FieldID = fd.Id.ToString();
            }
            else
            {
                FieldID = strFieldID;
            }
            //分类
            Category cy = cl.Categories[0];
            if (Request.Cookies["CurriculaViewCount_" + lbID.Text] == null)
            {
                HttpCookie MyCookie = new HttpCookie("CurriculaViewCount_" + lbID.Text);
                DateTime now = DateTime.Now;

                MyCookie["IP"] = Request.UserHostAddress;
                MyCookie["tid"] = lbID.Text;
                MyCookie.Expires = now.AddHours(1);

                Response.Cookies.Add(MyCookie);
                cl.ViewCount++;
                cl.UpdateViewCount();
            }            
        }
    }

   
}
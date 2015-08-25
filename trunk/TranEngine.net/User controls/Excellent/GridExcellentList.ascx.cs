using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;

public partial class User_controls_Excellent_GridExcellentList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    void BindGrid()
    {
        List<Excellent> exs = Excellent.Excellents.FindAll(
            delegate(Excellent e)
            {
                return e.IsPublished == true;
            });
       
        GridList.DataSource = exs;
        GridList.DataBind();
    }

    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor a1 = e.Row.Cells[0].FindControl("a1") as HtmlAnchor;
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Excellent crl = e.Row.DataItem as Excellent;
            Res rs = Res.GetRes(crl.MastPic);
            if (rs!=null)
            {
                a1.HRef = rs.GetResTempFilePath();
            }
            
            aPages.HRef = GetEditHtml(crl.Id.ToString());

            Label ltDays = e.Row.Cells[0].FindControl("ltDays") as Label;
            Label ltAddress = e.Row.Cells[0].FindControl("ltAddress") as Label;
            Excellent ex = e.Row.DataItem as Excellent;
            if (ex != null)
            {
                ltDays.Text = ex.TrainingDate.ToShortDateString();
                ltAddress.Text = ex.CityTown;

            }
        }
    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\ExcellentView.aspx?eid=" + id;
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
    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
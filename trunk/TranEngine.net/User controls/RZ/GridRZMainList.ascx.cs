using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class User_controls_RZ_GridRZMainList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }
    void BindGrid()
    {
        List<RzViewContent> rz1 = RZSource.Init.GetRzSourceByType("产品认证", "");
        List<RzViewContent> rz1_tp = new List<RzViewContent>();
        for (int i = 0; (i < 10 && i < rz1.Count); i++)
        {
            rz1_tp.Add(rz1[i]);
        }
        GridList1.DataSource = rz1_tp;
        GridList1.DataBind();
        List<RzViewContent> rz2 = RZSource.Init.GetRzSourceByType("体系认证", "");
        List<RzViewContent> rz2_tp = new List<RzViewContent>();
        for (int i = 0; (i < 10 && i < rz2.Count); i++)
        {
            rz2_tp.Add(rz2[i]);
        }
        GridList2.DataSource = rz2_tp;
        GridList2.DataBind();
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RzViewContent crl = ((RzViewContent)e.Row.DataItem);
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            aPages.HRef = GetEditHtml(crl);

        }
    }
    public static string GetEditHtml(RzViewContent cr)
    {
        string title = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.Title);
        string type = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.RzType);
        return Utils.AbsoluteWebRoot + @"Views\RZView.aspx?title=" + title + "&type=" + type;
    }
}
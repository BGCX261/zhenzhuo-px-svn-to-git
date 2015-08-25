using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_RZ_GridRzList : System.Web.UI.UserControl
{
    public string RzType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    void BindGrid()
    {
        List<RzViewContent> rz = RZSource.Init.GetRzSourceByType(RzType,"");
        GridList.DataSource = rz;
        GridList.DataBind();
    }

    protected void GridList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RzViewContent crl = ((RzViewContent)e.Row.DataItem);
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Label ltDes = e.Row.Cells[0].FindControl("ltDes") as Label;
            ltDes.Text = StripString(crl.Content, 110,true);
            aPages.HRef = GetEditHtml(crl);

        }
    }

    public static string GetEditHtml(RzViewContent cr)
    {
        string title = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.Title);
        string type = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.RzType);
        return Utils.AbsoluteWebRoot + @"Views\RZView.aspx?title=" + title + "&type=" + type;
    }

    protected string StripString(object s, int len, bool isHtml)
    {
        string body = Utils.StripHtml(s.ToString());
        string _s = "";
        if (body.ToString().Trim().Length > len) { _s = body.ToString().Substring(0, len - 2) + "..."; }
        else { _s = body.ToString(); }
        return _s;
    }

   
    protected void GridList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGrid();
        GridList.PageIndex = e.NewPageIndex;
        GridList.DataBind();
    }
}
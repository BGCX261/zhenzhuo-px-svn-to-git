using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class User_controls_RZ_GridRzSf : System.Web.UI.UserControl
{
    public string RzType;
    public string pType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    void BindGrid()
    {
        List<RzViewContent> rz = RZSource.Init.GetRzSourceByType(RzType, pType);
        GridList.DataSource = rz;
        GridList.DataBind();
    }

    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RzViewContent crl = ((RzViewContent)e.Row.DataItem);
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            
            aPages.HRef = GetEditHtml(crl);

        }
    }
    public string GetEditHtml(RzViewContent cr)
    {
        string title = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.Title);
        string type = System.Web.HttpContext.Current.Server.UrlPathEncode(cr.RzType);
        string ptype = System.Web.HttpContext.Current.Server.UrlPathEncode(pType);
        return Utils.AbsoluteWebRoot + @"Views\RZView.aspx?title=" + title + "&type=" + type + "&ptype=" + ptype;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 19) { _s = s.ToString().Substring(0, 17) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
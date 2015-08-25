using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_GridCorrelation : System.Web.UI.UserControl
{
    public string FieldID;
    public string strID;
    //相关公开课数据控件
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["id"] == null)
        {
            return;
        }
        strID = Request.Params["id"];

        if (Request.Params["FieldID"] != null)
        {
            FieldID = Request.Params["FieldID"];
        }
        else
        {
            Curricula cl = Curricula.GetCurricula(new Guid(strID));
            Field fd = cl.Fields[0];
            FieldID = fd.Id.ToString();
        }
        BindGrid();
    }

    private void BindGrid()
    {
        List<Field> fls = Field.Fields.FindAll(
            delegate(Field f)
            {
                return (f.Id.ToString() == FieldID);
            });
        if (fls.Count == 0)
        {
            return;
        }
        Field cField = fls[0];

        List<Curricula> cls = Curricula.Curriculas.FindAll(
           delegate(Curricula c)
           {
               return (c.IsPublished == true) && (c.Fields.Contains(cField));
           });
        //int iCount = cls.Count;
        //List<Curricula> cls = Curricula.Curriculas.FindAll(
        //   delegate(Curricula c)
        //   {
        //       return c.IsPublished == true;
        //   });
        cls.Sort(delegate(Curricula p1, Curricula p2) { return Comparer<int>.Default.Compare(p2.ViewCount, p1.ViewCount); });
        List<Curricula> top5 = new List<Curricula>(5);
        List<Curricula> topTen = new List<Curricula>(5);

        int iCount = cls.Count;

        for (int i = 0; i < cls.Count; i++)
        {
            if (i % 2 == 0)
            {
                top5.Add(cls[i]);
            }
            else
            {
                topTen.Add(cls[i]);
            }            
            if (i == 9)
            {
                break;
            }
        }
        GridView1.DataSource = top5;
        GridView1.DataBind();
        GridView2.DataSource = topTen;
        GridView2.DataBind();

    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"Views\CurriculaView.aspx?id=" + id;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(crl.Id.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages1") as HtmlAnchor;
            Curricula crl = ((Curricula)e.Row.DataItem) as Curricula;
            aPages.HRef = GetEditHtml(crl.Id.ToString());
        }
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 22) { _s = s.ToString().Substring(0, 20) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
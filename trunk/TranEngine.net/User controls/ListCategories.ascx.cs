using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
public partial class User_controls_ListCategories : System.Web.UI.UserControl
{
    //行业表格
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        List<Category> fls = Category.Categories.FindAll(
            delegate(Category f)
            {
                return (f.Id.ToString() != "");
            });

        int iCount = fls.Count;
        List<Category> fls1 = new List<Category>();
        List<Category> fls2 = new List<Category>();
        List<Category> fls3 = new List<Category>();

        for (int i = 0; i < iCount; i++)
        {
            int imod = i % 3;
            switch (imod)
            {
                case 0:
                    fls1.Add(fls[i]);
                    break;
                case 1:
                    fls2.Add(fls[i]);
                    break;
                case 2:
                    fls3.Add(fls[i]);
                    break;
            }
        }
        int iCount1 = fls1.Count;
        int iCount2 = fls2.Count;
        int iCount3 = fls3.Count;



        if (iCount1 >= 5)
        {
            for (int i = iCount1; i > 5; i--)
            {
                fls1.RemoveAt(i - 1);
            }
        }

        if (iCount2 >= 5)
        {
            for (int i = iCount2; i > 5; i--)
            {
                fls2.RemoveAt(i - 1);
            }
        }

        if (iCount3 >= 5)
        {
            for (int i = iCount3; i > 5; i--)
            {
                fls3.RemoveAt(i - 1);
            }
        }


        GridView1.DataSource = fls1;
        GridView1.DataBind();
        GridView2.DataSource = fls2;
        GridView2.DataBind();
        GridView3.DataSource = fls3;
        GridView3.DataBind();

    }

    public static string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"CurriculaList.aspx?CategoryID=" + id;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Category crl = ((Category)e.Row.DataItem) as Category;
            aPages.HRef = GetEditHtml(crl.Id.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages1") as HtmlAnchor;
            Category crl = ((Category)e.Row.DataItem) as Category;
            aPages.HRef = GetEditHtml(crl.Id.ToString());
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages2") as HtmlAnchor;
            Category crl = ((Category)e.Row.DataItem) as Category;
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
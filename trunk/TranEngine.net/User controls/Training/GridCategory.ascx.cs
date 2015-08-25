using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;

public partial class User_controls_Training_GridCategory : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        List<Category> l1 = new List<Category>();
        List<Category> l2 = new List<Category>();
        int l1Count = getGridSourceCount();
        for (int i = 0; i < Category.Categories.Count; i++)
        {
            if (l1Count > i)
            {
                l1.Add(Category.Categories[i]);
            }
            else
            {
                l2.Add(Category.Categories[i]);
            }
        }
        GridFields.DataSource = l1;
        GridView1.DataSource = l2;
        GridFields.DataBind();
        GridView1.DataBind();
    }

    private int getGridSourceCount()
    {
        int count = Category.Categories.Count;
        return count / 2;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }

    public string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"TrainList.aspx?cid=" + id;
    }

    private int GetTrainsByCategory(Guid CategoryID)
    {
        return Training.GetTrainingsByCategory(CategoryID).FindAll(
            delegate(Training tg)
            {
                return tg.IsPublished == true;
            }).Count;
    }

    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Label lb = e.Row.Cells[1].FindControl("lbNum") as Label;

            Category fd = e.Row.DataItem as Category;
            aPages.HRef = GetEditHtml(fd.Id.ToString());

            lb.Text = GetTrainsByCategory(fd.Id).ToString();

        }
    }
}
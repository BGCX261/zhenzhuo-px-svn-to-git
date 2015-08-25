using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class User_controls_Training_GridField : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        List<Field> l1 = new List<Field>();
        List<Field> l2 = new List<Field>();
        int l1Count = getGridSourceCount();
        for (int i = 0; i < Field.Fields.Count; i++)
        {
            if (l1Count > i)
            {
                l1.Add(Field.Fields[i]);
            }
            else
            {
                l2.Add(Field.Fields[i]);
            }
        }
        GridFields.DataSource = l1;
        GridView1.DataSource = l2;
        GridFields.DataBind();
        GridView1.DataBind();
    }

    private int getGridSourceCount()
    {
        int count = Field.Fields.Count;
        return count / 2;
    }
    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
    protected void GridFields_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlAnchor aPages = e.Row.Cells[0].FindControl("aPages") as HtmlAnchor;
            Label lb = e.Row.Cells[1].FindControl("lbNum") as Label;

            Field fd = e.Row.DataItem as Field;
            aPages.HRef = GetEditHtml(fd.Id.ToString());

            lb.Text = GetTrainsByField(fd.Id).ToString();

        }
    }

    public string GetEditHtml(string id)
    {
        return Utils.AbsoluteWebRoot + @"TrainList.aspx?fid=" + id;
    }

    private int GetTrainsByField(Guid fieldId)
    {
        return Training.GetTrainingsByField(fieldId).FindAll(
            delegate(Training tg)
            {
                return tg.IsPublished == true;
            }).Count;
    }
}
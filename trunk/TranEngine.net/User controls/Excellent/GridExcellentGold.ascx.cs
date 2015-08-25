using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;

public partial class User_controls_Excellent_GridExcellentGold : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Table table = new Table();
            table.Style.Add(HtmlTextWriterStyle.Width, "100%");
            table.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");

            PlaceHolder.Controls.Add(table);
            TableRow tr = new TableRow();
            table.Rows.Add(tr);
            List<Excellent> exs = Excellent.Excellents.FindAll(
                delegate(Excellent ex)
                {
                    return ex.IsPublished == true;
                });

            exs.Sort(delegate(Excellent a1, Excellent a2)
            {
                return a2.DateCreated.CompareTo(a1.DateCreated);
            });
            int num = 0;
            int rowCellCount = 0;
            for (int i = 0; i < exs.Count; i++)
            {
                Excellent exl = exs[i];
                if ( num < 4)
                {
                    TableCell tc = new TableCell();
                    tc.Style.Add(HtmlTextWriterStyle.TextAlign, "center");
                    tc.Text = "<div><img style='border: 1px solid #C0C0C0' width='135px' height='100px' src='" + SetImageUrl(exl.MastPic.ToString()) + "'  /></div><div><a href='" + Utils.AbsoluteWebRoot + @"Views\ExcellentView.aspx?eid=" + exl.Id + "'>" + exl.Title + "</a></div>";
                    if (rowCellCount < 2)
                    {
                        tr.Cells.Add(tc);
                        rowCellCount++;
                        num++;
                    }
                    if (rowCellCount == 2 && num != 4)
                    {
                        tr = new TableRow();
                        table.Rows.Add(tr);
                        rowCellCount = 0;
                    }
                }
            }
        }
    }

    public string SetImageUrl(string ResId)
    {
        if (ResId == null || ResId == string.Empty)
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";

        }
        Guid id;
        try
        {
            id = new Guid(ResId);
        }
        catch
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png"; ;
        }
        Res rs = Res.GetRes(new Guid(ResId));
        if (rs != null)
        {
            return rs.GetResTempFilePath();
        }
        else
        {
            return Utils.RelativeWebRoot + "pics/no_avatar.png";
        }
    }
}
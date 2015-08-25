using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;

public partial class Training_FieldsNoMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fieldId = Request["id"];
        bindUL(fieldId);
    }

    public string fieldId;
    private void bindUL(string fieldId)
    {
        Field fd = Field.GetField(new Guid(fieldId));
        string tmpl = "<div style='display:block;'><div style='float:left'><a href=\"" + Utils.AbsoluteWebRoot + "Views/TrainingView.aspx?id={0}\"  title=\"{1}\">{2}</a></div><div style='float:right'><span style='color: #FF6600'>{3}</span>天&nbsp;{4}</div></div><div style='clear:both;height=0px;'/>";
        //Utils.AbsoluteWebRoot
        int liCount = 0;
        foreach (Training item in Training.Trainings)
        {
            if (item.Fields.Contains(fd) && item.IsPublished)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                string tchName = item.Teacher;
                AuthorProfile ap = AuthorProfile.GetProfile(item.Teacher);
                if (ap != null)
                {
                    tchName = ap.DisplayName;
                }
                li.InnerHtml = string.Format(tmpl, item.Id.ToString(), item.Title, jup(item.Title),item.Days.ToString(),tchName);
                li.Style.Add(HtmlTextWriterStyle.Width, "99%");
                list.Controls.Add(li);
                liCount++;
                if (liCount>=10)
                {
                    break;
                }
            }
            
        }
    }

    protected string jup(object s)
    {
        string _s = "";
        if (s.ToString().Trim().Length > 23) { _s = s.ToString().Substring(0, 21) + "..."; }
        else { _s = s.ToString(); }
        return _s;
    }
}
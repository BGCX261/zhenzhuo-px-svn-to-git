using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
public partial class admin_Pages_Curricula : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    #region Private members

    private static string _id;
    private static Curricula _curricula;
    private static string _urlReferrer;
   public Curricula CurrentCurricula { get { return _curricula; } }

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            txtTitle.Focus();
            BindTags();
            BindCategories();
            BindFields();
            _id = HttpContext.Current.Request.QueryString["id"];
            _urlReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
            if (_id!=null && _id!= string.Empty )
            {
                _curricula = Curricula.GetCurricula(new Guid(_id));

            }
            if (_curricula!=null)
            {
                BindCurricula();
            }
        }
        
        
    }

    private void BindCurricula()
    {
        txtTitle.Text = _curricula.Title;
        txtObj.Text = _curricula.ObjectDes;
        txtContent.Text = _curricula.Content;
        txtPoints.Text = _curricula.Points.ToString();
        txtScores.Text = _curricula.Scores.ToString();
        foreach (ListItem item in cblCategories.Items)
        {
            if (_curricula.Categories.Contains(Category.GetCategory(new Guid(item.Value))))
            {
                item.Selected = true;
            }
        }

        foreach (ListItem item in cblField.Items)
        {
            if (_curricula.Fields.Contains(Field.GetField(new Guid(item.Value))))
            {
                item.Selected = true;
            }
        }
        string[] tags = new string[_curricula.Tags.Count];
        for (int i = 0; i < _curricula.Tags.Count; i++)
        {
            tags[i] = _curricula.Tags[i];
        }
        txtTags.Text = string.Join(",", tags);
    }

    private void BindFields()
    {
        foreach (Field fld in Field.Fields)
        {
            cblField.Items.Add(new ListItem(Server.HtmlEncode(fld.FieldName), fld.Id.ToString()));
        }
    }

    private void BindCategories()
    {
        foreach (Category cat in Category.Categories)
        {
            cblCategories.Items.Add(new ListItem(Server.HtmlEncode(cat.Title), cat.Id.ToString()));
        }
    }

    private void BindTags()
    {
        System.Collections.Generic.List<string> col = new System.Collections.Generic.List<string>();
        foreach (Curricula cls in Curricula.Curriculas)
        {
            foreach (string tag in cls.Tags)
            {
                if (!col.Contains(tag))
                    col.Add(tag);
            }
        }
        foreach (Training cls in Training.Trainings)
        {
            foreach (string tag in cls.Tags)
            {
                if (!col.Contains(tag))
                    col.Add(tag);
            }

        }
        col.Sort(delegate(string s1, string s2) { return String.Compare(s1, s2); });

        foreach (string tag in col)
        {
            HtmlAnchor a = new HtmlAnchor();
            a.HRef = "javascript:void(0)";
            a.Attributes.Add("onclick", "AddTag(this)");
            a.InnerText = tag;
            phTags.Controls.Add(a);
        }
    }
    public bool IsActionNew
    {
        get
        {
            return (_curricula == null);
        }
    }
    #region ICallbackEventHandler 成员

    private string _Callback;

    public string GetCallbackResult()
    {
        return _Callback;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        _Callback = Utils.RemoveIllegalCharacters(eventArgument.Trim());
    }

    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (_curricula == null)
        {
            _curricula = new Curricula();
            _curricula.Author =  Page.User.Identity.Name;
            _curricula.DateCreated = DateTime.Now.AddHours(-TrainSettings.Instance.Timezone);
            
        }
        
        _curricula.Title = txtTitle.Text;
        _curricula.ObjectDes = txtObj.Text;
        _curricula.Content = txtContent.Text;
        _curricula.Tags.Clear();
        _curricula.Categories.Clear();
        _curricula.Fields.Clear();
        _curricula.Points = Convert.ToInt32(txtPoints.Text);
        _curricula.Scores = Convert.ToInt32(txtScores.Text);
        foreach (ListItem item in cblCategories.Items)
        {
            if (item.Selected)
                _curricula.Categories.Add(Category.GetCategory(new Guid(item.Value)));
        }

        foreach (ListItem item in cblField.Items)
        {
            if (item.Selected)
                _curricula.Fields.Add(Field.GetField(new Guid(item.Value)));
        }

        if (txtTags.Text.Trim().Length > 0)
        {
            string[] tags = txtTags.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tag in tags)
            {
                if (string.IsNullOrEmpty(_curricula.Tags.Find(delegate(string t) { return t.Equals(tag.Trim(), StringComparison.OrdinalIgnoreCase); })))
                {
                    _curricula.Tags.Add(tag.Trim());
                }
            }
        }
        _curricula.Save();
        Reload();
       
    }

    protected void Reload()
    {
        
        ClientScript.RegisterClientScriptBlock(GetType(), "ClientScript",
            "<SCRIPT LANGUAGE='JavaScript'>parent.closeEditor(true);</SCRIPT>");
    }
}
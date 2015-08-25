using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;
using System.Web.UI.HtmlControls;
using System.Web.Security;
public partial class admin_Pages_Training : System.Web.UI.Page, System.Web.UI.ICallbackEventHandler
{
    #region Private members

    private static string _id;
    private static Training _training;
    private static string _urlReferrer;
   public Training CurrentTraining { get { return _training; } }

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        txtTitle.Focus();
        
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindTags();
            BindCategories();
            BindFields();
            BindTeacher();
            _id = HttpContext.Current.Request.QueryString["id"];
            _urlReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
            if (_id!=null && _id!= string.Empty )
            {
                _training = Training.GetTraining(new Guid(_id));

            }
            if (_training!=null)
            {
                BindTraining();
            }
        }
        
    }

    private void BindTraining()
    {
        txtTitle.Text = _training.Title;
        ddlObj.SelectedValue = _training.Teacher;
        txtDays.Text = _training.Days.ToString();
        txtContent.Text = _training.Content;
        
        
        foreach (ListItem item in cblCategories.Items)
        {
            if (_training.Categories.Contains(Category.GetCategory(new Guid(item.Value))))
            {
                item.Selected = true;
            }
        }

        foreach (ListItem item in cblField.Items)
        {
            if (_training.Fields.Contains(Field.GetField(new Guid(item.Value))))
            {
                item.Selected = true;
            }
        }
        string[] tags = new string[_training.Tags.Count];
        for (int i = 0; i < _training.Tags.Count; i++)
        {
            tags[i] = _training.Tags[i];
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
        foreach (Training cls in Training.Trainings)
        {
            foreach (string tag in cls.Tags)
            {
                if (!col.Contains(tag))
                    col.Add(tag);
            }
            
        }
        foreach (Curricula cls in Curricula.Curriculas)
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

    private void BindTeacher()
    {
        ddlObj.Items.Clear();
        ListItem dli = new ListItem("教师团", "教师团");
        ddlObj.Items.Add(dli);
        if (Page.User.IsInRole("administrators"))
        {
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
                
                if (ap.IsTeacher || ap.IsOrgan)
                {
                    ListItem li = new ListItem(ap.DisplayName, user.UserName);
                    ddlObj.Items.Add(li);
                    ddlObj.SelectedValue = User.Identity.Name;
                }
               
            }
        }
        else
        {
            ListItem li = new ListItem(AuthorProfile.GetProfile( Page.User.Identity.Name).DisplayName, Page.User.Identity.Name);
            ddlObj.Items.Add(li);
            ddlObj.SelectedValue = User.Identity.Name;
        }
    }
    public bool IsActionNew
    {
        get
        {
            return (_training == null);
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
        if (_training == null)
        {
            _training = new Training();
            _training.Author =  Page.User.Identity.Name;
            _training.DateCreated = DateTime.Now.AddHours(-TrainSettings.Instance.Timezone);
            
        }
        
        _training.Title = txtTitle.Text;
        _training.Teacher = ddlObj.SelectedValue;
        _training.Days = Convert.ToInt32(txtDays.Text);
        _training.Content = txtContent.Text;
        _training.Tags.Clear();
        _training.Categories.Clear();
        _training.Fields.Clear();
        
        foreach (ListItem item in cblCategories.Items)
        {
            if (item.Selected)
                _training.Categories.Add(Category.GetCategory(new Guid(item.Value)));
        }

        foreach (ListItem item in cblField.Items)
        {
            if (item.Selected)
                _training.Fields.Add(Field.GetField(new Guid(item.Value)));
        }

        if (txtTags.Text.Trim().Length > 0)
        {
            string[] tags = txtTags.Text.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tag in tags)
            {
                if (string.IsNullOrEmpty(_training.Tags.Find(delegate(string t) { return t.Equals(tag.Trim(), StringComparison.OrdinalIgnoreCase); })))
                {
                    _training.Tags.Add(tag.Trim());
                }
            }
        }
        _training.Save();
        Reload();
       
    }

    protected void Reload()
    {
        ClientScript.RegisterClientScriptBlock(GetType(), "ClientScript",
            "<SCRIPT LANGUAGE='JavaScript'>parent.closeEditor(true);</SCRIPT>");
    }
}
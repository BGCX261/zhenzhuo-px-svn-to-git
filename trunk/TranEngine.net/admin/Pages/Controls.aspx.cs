using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using TrainEngine.Core;

public partial class admin_Pages_Controls : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
      BindSettings();

    btnSave.Click += new EventHandler(btnSave_Click);
    btnSave.Text = Resources.labels.save + " " + Resources.labels.settings;
    Page.Title = Resources.labels.controls;
  }

  void btnSave_Click(object sender, EventArgs e)
  {
    TrainSettings.Instance.NumberOfRecentPosts = int.Parse(txtNumberOfPosts.Text, CultureInfo.InvariantCulture);
    TrainSettings.Instance.DisplayCommentsOnRecentPosts = cbDisplayComments.Checked;
    TrainSettings.Instance.DisplayRatingsOnRecentPosts = cbDisplayRating.Checked;

    TrainSettings.Instance.NumberOfRecentComments = int.Parse(txtNumberOfComments.Text, CultureInfo.InvariantCulture);

    TrainSettings.Instance.SearchButtonText = txtSearchButtonText.Text;
    TrainSettings.Instance.SearchCommentLabelText = txtCommentLabelText.Text;
    TrainSettings.Instance.SearchDefaultText = txtDefaultSearchText.Text;
    TrainSettings.Instance.EnableCommentSearch = cbEnableCommentSearch.Checked;

    TrainSettings.Instance.ContactFormMessage = txtFormMessage.Text;
    TrainSettings.Instance.ContactThankMessage = txtThankMessage.Text;
    TrainSettings.Instance.EnableContactAttachments = cbEnableAttachments.Checked;

    TrainSettings.Instance.Save();
  }

  private void BindSettings()
  {
    txtNumberOfPosts.Text = TrainSettings.Instance.NumberOfRecentPosts.ToString();
    cbDisplayComments.Checked = TrainSettings.Instance.DisplayCommentsOnRecentPosts;
    cbDisplayRating.Checked = TrainSettings.Instance.DisplayRatingsOnRecentPosts;

    txtNumberOfComments.Text = TrainSettings.Instance.NumberOfRecentComments.ToString();

    txtSearchButtonText.Text = TrainSettings.Instance.SearchButtonText;
    txtCommentLabelText.Text = TrainSettings.Instance.SearchCommentLabelText;
    txtDefaultSearchText.Text = TrainSettings.Instance.SearchDefaultText;
    cbEnableCommentSearch.Checked = TrainSettings.Instance.EnableCommentSearch;

    txtThankMessage.Text = TrainSettings.Instance.ContactThankMessage;
    txtFormMessage.Text = TrainSettings.Instance.ContactFormMessage;
    cbEnableAttachments.Checked = TrainSettings.Instance.EnableContactAttachments;
  }
}

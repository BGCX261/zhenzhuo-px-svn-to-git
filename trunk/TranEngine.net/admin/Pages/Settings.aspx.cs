#region Using

using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using TrainEngine.Core;

#endregion

public partial class admin_Pages_configuration : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			BindThemes();
			BindCultures();
			BindSettings();
		}

		Page.MaintainScrollPositionOnPostBack = true;
		Page.Title = Resources.labels.settings;

		btnSave.Click += new EventHandler(btnSave_Click);
		btnSaveTop.Click += new EventHandler(btnSave_Click);
		btnTestSmtp.Click += new EventHandler(btnTestSmtp_Click);

		btnSaveTop.Text = Resources.labels.saveSettings;
		btnSave.Text = btnSaveTop.Text;
		valDescChar.ErrorMessage = "Please specify a number";
	}

	private void btnTestSmtp_Click(object sender, EventArgs e)
	{
		try
		{
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(txtEmail.Text, txtName.Text);
			mail.To.Add(mail.From);
			mail.Subject = "Test mail from " + txtName.Text;
			mail.Body = "Success";
			SmtpClient smtp = new SmtpClient(txtSmtpServer.Text);
            // don't send credentials if a server doesn't require it,
            // linux smtp servers don't like that 
            if (!string.IsNullOrEmpty(txtSmtpUsername.Text)) {
                smtp.Credentials = new System.Net.NetworkCredential(txtSmtpUsername.Text, txtSmtpPassword.Text);
            }
			smtp.EnableSsl = cbEnableSsl.Checked;
			smtp.Port = int.Parse(txtSmtpServerPort.Text, CultureInfo.InvariantCulture);
			smtp.Send(mail);
			lbSmtpStatus.Text = "Test successfull";
			lbSmtpStatus.Style.Add(HtmlTextWriterStyle.Color, "green");
		}
		catch (Exception ex)
		{
			lbSmtpStatus.Text = "Could not connect - " + ex.Message;
			lbSmtpStatus.Style.Add(HtmlTextWriterStyle.Color, "red");
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
        bool enabledHttpCompressionSettingChanged = TrainSettings.Instance.EnableHttpCompression != cbEnableCompression.Checked;

        //-----------------------------------------------------------------------
        // Set Basic settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.Name = txtName.Text;
        TrainSettings.Instance.Description = txtDescription.Text;
        TrainSettings.Instance.PostsPerPage = int.Parse(txtPostsPerPage.Text);
        TrainSettings.Instance.Theme = ddlTheme.SelectedValue;
        TrainSettings.Instance.MobileTheme = ddlMobileTheme.SelectedValue;
        TrainSettings.Instance.UseTrainNameInPageTitles = cbUseTrainNameInPageTitles.Checked;
        TrainSettings.Instance.EnableRelatedPosts = cbShowRelatedPosts.Checked;
        TrainSettings.Instance.EnableRating = cbEnableRating.Checked;
        TrainSettings.Instance.ShowDescriptionInPostList = cbShowDescriptionInPostList.Checked;
        TrainSettings.Instance.DescriptionCharacters = int.Parse(txtDescriptionCharacters.Text);
        TrainSettings.Instance.ShowDescriptionInPostListForPostsByTagOrCategory = cbShowDescriptionInPostListForPostsByTagOrCategory.Checked;
        TrainSettings.Instance.DescriptionCharactersForPostsByTagOrCategory = int.Parse(txtDescriptionCharactersForPostsByTagOrCategory.Text);
        TrainSettings.Instance.TimeStampPostLinks = cbTimeStampPostLinks.Checked;
        TrainSettings.Instance.ShowPostNavigation = cbShowPostNavigation.Checked;
        TrainSettings.Instance.Culture = ddlCulture.SelectedValue;
        TrainSettings.Instance.Timezone = double.Parse(txtTimeZone.Text, CultureInfo.InvariantCulture);

        //-----------------------------------------------------------------------
        // Set Email settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.Email = txtEmail.Text;
        TrainSettings.Instance.SmtpServer = txtSmtpServer.Text;
        TrainSettings.Instance.SmtpServerPort = int.Parse(txtSmtpServerPort.Text);
        TrainSettings.Instance.SmtpUserName = txtSmtpUsername.Text;
        TrainSettings.Instance.SmtpPassword = txtSmtpPassword.Text;
        TrainSettings.Instance.SendMailOnComment = cbComments.Checked;
        TrainSettings.Instance.EnableSsl = cbEnableSsl.Checked;
        TrainSettings.Instance.EmailSubjectPrefix = txtEmailSubjectPrefix.Text;

        TrainSettings.Instance.EnableEnclosures = cbEnableEnclosures.Checked;

        //-----------------------------------------------------------------------
        // Set Advanced settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.EnableHttpCompression = cbEnableCompression.Checked;
        TrainSettings.Instance.RemoveWhitespaceInStyleSheets = cbRemoveWhitespaceInStyleSheets.Checked;
        TrainSettings.Instance.CompressWebResource = cbCompressWebResource.Checked;
        TrainSettings.Instance.EnableOpenSearch = cbEnableOpenSearch.Checked;
        TrainSettings.Instance.RequireSSLMetaWeblogAPI = cbRequireSslForMetaWeblogApi.Checked;
        TrainSettings.Instance.HandleWwwSubdomain = rblWwwSubdomain.SelectedItem.Value;
        TrainSettings.Instance.EnableTrackBackSend = cbEnableTrackBackSend.Checked;
        TrainSettings.Instance.EnableTrackBackReceive = cbEnableTrackBackReceive.Checked;
        TrainSettings.Instance.EnablePingBackSend = cbEnablePingBackSend.Checked;
        TrainSettings.Instance.EnablePingBackReceive = cbEnablePingBackReceive.Checked;
        TrainSettings.Instance.EnableErrorLogging = cbEnableErrorLogging.Checked;

        //-----------------------------------------------------------------------
        // Set Syndication settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.SyndicationFormat = ddlSyndicationFormat.SelectedValue;
        TrainSettings.Instance.PostsPerFeed = int.Parse(txtPostsPerFeed.Text, CultureInfo.InvariantCulture);
        TrainSettings.Instance.AuthorName = txtDublinCoreCreator.Text;
        TrainSettings.Instance.Language = txtDublinCoreLanguage.Text;

        float latitude;
        if (Single.TryParse(txtGeocodingLatitude.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out latitude)) {
            TrainSettings.Instance.GeocodingLatitude = latitude;
        } else {
            TrainSettings.Instance.GeocodingLatitude = Single.MinValue;
        }
        float longitude;
        if (Single.TryParse(txtGeocodingLongitude.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out longitude)) {
            TrainSettings.Instance.GeocodingLongitude = longitude;
        } else {
            TrainSettings.Instance.GeocodingLongitude = Single.MinValue;
        }

        TrainSettings.Instance.Endorsement = txtTrainChannelBLink.Text;

        if (txtAlternateFeedUrl.Text.Trim().Length > 0 && !txtAlternateFeedUrl.Text.Contains("://"))
            txtAlternateFeedUrl.Text = "http://" + txtAlternateFeedUrl.Text;

        TrainSettings.Instance.AlternateFeedUrl = txtAlternateFeedUrl.Text;

        //-----------------------------------------------------------------------
        // HTML header section
        //-----------------------------------------------------------------------
        TrainSettings.Instance.HtmlHeader = txtHtmlHeader.Text;

        //-----------------------------------------------------------------------
        // Visitor tracking settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.TrackingScript = txtTrackingScript.Text;

        //-----------------------------------------------------------------------
        //  Persist settings
        //-----------------------------------------------------------------------
        TrainSettings.Instance.Save();

        if (enabledHttpCompressionSettingChanged)
        { 
            // To avoid errors in IIS7 when toggling between compression and no-compression, re-start the app.
            string ConfigPath = HttpContext.Current.Request.PhysicalApplicationPath + "Web.Config";
            File.SetLastWriteTimeUtc(ConfigPath, DateTime.UtcNow);
        }

        Response.Redirect(Request.RawUrl, true);
        
	}

	private void BindSettings()
	{
		//-----------------------------------------------------------------------
		// Bind Basic settings
		//-----------------------------------------------------------------------
		txtName.Text = TrainSettings.Instance.Name;
		txtDescription.Text = TrainSettings.Instance.Description;
		txtPostsPerPage.Text = TrainSettings.Instance.PostsPerPage.ToString();
		cbShowRelatedPosts.Checked = TrainSettings.Instance.EnableRelatedPosts;
		ddlTheme.SelectedValue = TrainSettings.Instance.Theme;
		ddlMobileTheme.SelectedValue = TrainSettings.Instance.MobileTheme;
		cbUseTrainNameInPageTitles.Checked = TrainSettings.Instance.UseTrainNameInPageTitles;
		cbEnableRating.Checked = TrainSettings.Instance.EnableRating;
		cbShowDescriptionInPostList.Checked = TrainSettings.Instance.ShowDescriptionInPostList;
		txtDescriptionCharacters.Text = TrainSettings.Instance.DescriptionCharacters.ToString();
        cbShowDescriptionInPostListForPostsByTagOrCategory.Checked = TrainSettings.Instance.ShowDescriptionInPostListForPostsByTagOrCategory;
        txtDescriptionCharactersForPostsByTagOrCategory.Text = TrainSettings.Instance.DescriptionCharactersForPostsByTagOrCategory.ToString();
        cbTimeStampPostLinks.Checked = TrainSettings.Instance.TimeStampPostLinks;
		ddlCulture.SelectedValue = TrainSettings.Instance.Culture;
		txtTimeZone.Text = TrainSettings.Instance.Timezone.ToString();
		cbShowPostNavigation.Checked = TrainSettings.Instance.ShowPostNavigation;

		//-----------------------------------------------------------------------
		// Bind Email settings
		//-----------------------------------------------------------------------
		txtEmail.Text = TrainSettings.Instance.Email;
		txtSmtpServer.Text = TrainSettings.Instance.SmtpServer;
		txtSmtpServerPort.Text = TrainSettings.Instance.SmtpServerPort.ToString();
		txtSmtpUsername.Text = TrainSettings.Instance.SmtpUserName;
		txtSmtpPassword.Text = TrainSettings.Instance.SmtpPassword;
		cbComments.Checked = TrainSettings.Instance.SendMailOnComment;
		cbEnableSsl.Checked = TrainSettings.Instance.EnableSsl;
		txtEmailSubjectPrefix.Text = TrainSettings.Instance.EmailSubjectPrefix;

		cbEnableEnclosures.Checked = TrainSettings.Instance.EnableEnclosures;

		//-----------------------------------------------------------------------
		// Bind Advanced settings
		//-----------------------------------------------------------------------
		cbEnableCompression.Checked = TrainSettings.Instance.EnableHttpCompression;
		cbRemoveWhitespaceInStyleSheets.Checked = TrainSettings.Instance.RemoveWhitespaceInStyleSheets;
		cbCompressWebResource.Checked = TrainSettings.Instance.CompressWebResource;
		cbEnableOpenSearch.Checked = TrainSettings.Instance.EnableOpenSearch;
		cbRequireSslForMetaWeblogApi.Checked = TrainSettings.Instance.RequireSSLMetaWeblogAPI;
		rblWwwSubdomain.SelectedValue = TrainSettings.Instance.HandleWwwSubdomain;
		cbEnablePingBackSend.Checked = TrainSettings.Instance.EnablePingBackSend;
		cbEnablePingBackReceive.Checked = TrainSettings.Instance.EnablePingBackReceive;
		cbEnableTrackBackSend.Checked = TrainSettings.Instance.EnableTrackBackSend;
		cbEnableTrackBackReceive.Checked = TrainSettings.Instance.EnableTrackBackReceive;
        cbEnableErrorLogging.Checked = TrainSettings.Instance.EnableErrorLogging;

		//-----------------------------------------------------------------------
		// Bind Syndication settings
		//-----------------------------------------------------------------------
		ddlSyndicationFormat.SelectedValue = TrainSettings.Instance.SyndicationFormat;
		txtPostsPerFeed.Text = TrainSettings.Instance.PostsPerFeed.ToString();
		txtDublinCoreCreator.Text = TrainSettings.Instance.AuthorName;
		txtDublinCoreLanguage.Text = TrainSettings.Instance.Language;

		txtGeocodingLatitude.Text = TrainSettings.Instance.GeocodingLatitude != Single.MinValue ? TrainSettings.Instance.GeocodingLatitude.ToString(CultureInfo.InvariantCulture) : String.Empty;
		txtGeocodingLongitude.Text = TrainSettings.Instance.GeocodingLongitude != Single.MinValue ? TrainSettings.Instance.GeocodingLongitude.ToString(CultureInfo.InvariantCulture) : String.Empty;

		txtTrainChannelBLink.Text = TrainSettings.Instance.Endorsement;
		txtAlternateFeedUrl.Text = TrainSettings.Instance.AlternateFeedUrl;

		//-----------------------------------------------------------------------
		// HTML header section
		//-----------------------------------------------------------------------
		txtHtmlHeader.Text = TrainSettings.Instance.HtmlHeader;

		//-----------------------------------------------------------------------
		// Visitor tracking settings
		//-----------------------------------------------------------------------
		txtTrackingScript.Text = TrainSettings.Instance.TrackingScript;
	}

	private void BindThemes()
	{
		string path = Server.MapPath(Utils.RelativeWebRoot + "themes/");
		foreach (string dic in Directory.GetDirectories(path))
		{
			int index = dic.LastIndexOf(Path.DirectorySeparatorChar) + 1;
			ddlTheme.Items.Add(dic.Substring(index));
			ddlMobileTheme.Items.Add(dic.Substring(index));
		}
	}

	private void BindCultures()
	{
		if (File.Exists(Path.Combine(HttpRuntime.AppDomainAppPath, "PrecompiledApp.config")))
		{

			string precompiledDir = HttpRuntime.BinDirectory;
			string[] translations = Directory.GetFiles(precompiledDir, "App_GlobalResources.resources.dll", SearchOption.AllDirectories);
			foreach (string translation in translations)
			{
				string resourceDir = Path.GetDirectoryName(translation).Remove(0, precompiledDir.Length);
				if (!String.IsNullOrEmpty(resourceDir))
				{

					System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag(resourceDir);
					ddlCulture.Items.Add(new ListItem(info.NativeName, resourceDir));
				}
			}
		}
		else
		{

			string path = Server.MapPath(Utils.RelativeWebRoot + "App_GlobalResources/");
			foreach (string file in Directory.GetFiles(path, "labels.*.resx"))
			{

				int index = file.LastIndexOf(Path.DirectorySeparatorChar) + 1;
				string filename = file.Substring(index);
				filename = filename.Replace("labels.", string.Empty).Replace(".resx", string.Empty);
				System.Globalization.CultureInfo info = System.Globalization.CultureInfo.GetCultureInfoByIetfLanguageTag(filename);
				ddlCulture.Items.Add(new ListItem(info.NativeName, filename));

			}
		}
	}

}
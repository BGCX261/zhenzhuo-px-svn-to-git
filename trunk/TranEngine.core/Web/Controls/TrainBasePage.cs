﻿#region Using

using System;
using System.Globalization;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using TrainEngine.Core.Classes;

#endregion

namespace TrainEngine.Core.Web.Controls
{
	/// <summary>
	/// All pages in the custom themes as well as pre-defined pages in the root
	/// must inherit from this class.
	/// </summary>
	/// <remarks>
	/// The class is responsible for assigning the theme to all
	/// derived pages as well as adding RSS, RSD, tracking script
	/// and a whole lot more.
	/// </remarks>
	public abstract class TrainBasePage : System.Web.UI.Page
	{

		private string _Theme = TrainSettings.Instance.Theme;
		/// <summary>
		/// Assignes the selected theme to the pages.
		/// </summary>
		protected override void OnPreInit(EventArgs e)
		{
			MasterPageFile = Utils.RelativeWebRoot + "themes/" + _Theme + "/site.master";

			base.OnPreInit(e);

            //if (!Page.IsPostBack && !string.IsNullOrEmpty(Request.QueryString["deletepost"]))
            //{
            //    if (Page.User.Identity.IsAuthenticated)
            //    {
            //        Training training = Training.GetTraining(new Guid(Request.QueryString["deletetraining"]));
            //        if (Page.User.IsInRole(TrainSettings.Instance.AdministratorRole) || Page.User.Identity.Name == training.Author)
            //        {
            //            training.Delete();
            //            training.Save();
            //            Response.Redirect(Utils.RelativeWebRoot);
            //        }
            //    }
            //}
		}

		/// <summary>
		/// Adds links and javascript to the HTML header tag.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!Page.IsCallback)
			{
				// Links
				AddGenericLink("contents", "Archive", Utils.RelativeWebRoot + "archive.aspx");
				AddGenericLink("start", TrainSettings.Instance.Name, Utils.RelativeWebRoot);
				AddGenericLink("application/rdf+xml", "meta", "SIOC", Utils.AbsoluteWebRoot + "sioc.axd");
				AddGenericLink("application/apml+xml", "meta", "APML", Utils.AbsoluteWebRoot + "apml.axd");
				AddGenericLink("application/rdf+xml", "meta", "FOAF", Utils.AbsoluteWebRoot + "foaf.axd");

				if (string.IsNullOrEmpty(TrainSettings.Instance.AlternateFeedUrl))
				{
					AddGenericLink("application/rss+xml", "alternate", TrainSettings.Instance.Name + " (RSS)", Utils.FeedUrl + "?format=rss");
					AddGenericLink("application/atom+xml", "alternate", TrainSettings.Instance.Name + " (ATOM)", Utils.FeedUrl + "?format=atom");
				}
				else
				{
					AddGenericLink("application/rss+xml", "alternate", TrainSettings.Instance.Name, Utils.FeedUrl);
				}				
				
				AddGenericLink("application/rsd+xml", "edituri", "RSD", Utils.AbsoluteWebRoot + "rsd.axd");

				AddMetaContentType();
				AddDefaultLanguages();

                AddLocalizationKeys();
				AddJavaScriptInclude(Utils.RelativeWebRoot + "train.js", true, true);				

				if (TrainSettings.Instance.EnableOpenSearch)
					AddGenericLink("application/opensearchdescription+xml", "search", TrainSettings.Instance.Name, Utils.AbsoluteWebRoot + "opensearch.axd");

				if (!string.IsNullOrEmpty(TrainSettings.Instance.HtmlHeader))
					AddCustomCodeToHead();

				if (!string.IsNullOrEmpty(TrainSettings.Instance.TrackingScript))
					AddTrackingScript();
			}

			if (User.IsInRole(TrainSettings.Instance.AdministratorRole))
			{
				AddJavaScriptInclude(Utils.RelativeWebRoot + "admin/widget.js", true, true);
			}

			if (TrainSettings.Instance.RemoveWhitespaceInStyleSheets)
				CompressCss();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Page.PreRenderComplete"></see> event after 
		/// the <see cref="M:System.Web.UI.Page.OnPreRenderComplete(System.EventArgs)"></see> event and before the page is rendered.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnPreRenderComplete(EventArgs e)
		{
			base.OnPreRenderComplete(e);
			if (TrainSettings.Instance.UseTrainNameInPageTitles)
			{
				Page.Title = (TrainSettings.Instance.Name + " | " + Page.Title);
			}
		}

		/// <summary>
		/// Adds the localization keys to JavaScript for use globally.
		/// </summary>
		protected virtual void AddLocalizationKeys()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("function registerVariables(){");
			sb.AppendFormat("TrainEngine.webRoot='{0}';", Utils.RelativeWebRoot);
			sb.AppendFormat("TrainEngine.i18n.hasRated='{0}';", Utils.Translate("youAlreadyRated").Replace("'", "\\'"));			
			sb.AppendFormat("TrainEngine.i18n.savingTheComment='{0}';", Utils.Translate("savingTheComment").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.comments='{0}';", Utils.Translate("comments").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.commentWasSaved='{0}';", Utils.Translate("commentWasSaved").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.commentWaitingModeration='{0}';", Utils.Translate("commentWaitingModeration").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.cancel='{0}';", Utils.Translate("cancel").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.filter='{0}';", Utils.Translate("filter").Replace("'", "\\'"));
			sb.AppendFormat("TrainEngine.i18n.apmlDescription='{0}';", Utils.Translate("filterByApmlDescription").Replace("'", "\\'"));
            sb.AppendFormat("TrainEngine.i18n.beTheFirstToRate='{0}';", Utils.Translate("beTheFirstToRate").Replace("'", "\\'"));
            sb.AppendFormat("TrainEngine.i18n.currentlyRated='{0}';", Utils.Translate("currentlyRated").Replace("'", "\\'"));
            sb.AppendFormat("TrainEngine.i18n.ratingHasBeenRegistered='{0}';", Utils.Translate("ratingHasBeenRegistered").Replace("'", "\\'"));
            sb.AppendFormat("TrainEngine.i18n.rateThisXStars='{0}';", Utils.Translate("rateThisXStars").Replace("'", "\\'"));
            
			sb.Append("};");

            ClientScript.RegisterStartupScript(this.GetType(), "registerVariables", sb.ToString(), true);
		}

		/// <summary>
		/// Finds all stylesheets in the header and changes the 
		/// path so it points to css.axd which removes the whitespace.
		/// </summary>
		protected virtual void CompressCss()
		{
			if (Request.QueryString["theme"] != null)
				return;

			foreach (Control control in Page.Header.Controls)
			{
				HtmlControl c = control as HtmlControl;
				if (c != null && c.Attributes["type"] != null && c.Attributes["type"].Equals("text/css", StringComparison.OrdinalIgnoreCase))
				{
					if (!c.Attributes["href"].StartsWith("http://"))
					{
						string url = Utils.RelativeWebRoot + "themes/" + TrainSettings.Instance.Theme + "/css.axd?name=" + c.Attributes["href"];
						c.Attributes["href"] = url.Replace(".css", TrainSettings.Instance.Version() + ".css");
						c.EnableViewState = false;
					}
				}
			}
		}

		/// <summary>
		/// Adds the content-type meta tag to the header.
		/// </summary>
		protected virtual void AddMetaContentType()
		{
			HtmlMeta meta = new HtmlMeta();
			meta.HttpEquiv = "content-type";
			meta.Content = Response.ContentType + "; charset=" + Response.ContentEncoding.HeaderName;
			Page.Header.Controls.Add(meta);
		}

		/// <summary>
		/// Adds the default stylesheet language
		/// </summary>
		protected virtual void AddDefaultLanguages()
		{
			Response.AppendHeader("Content-Style-Type", "text/css");
			Response.AppendHeader("Content-Script-Type", "text/javascript");
		}

		/// <summary>
		/// Add a meta tag to the page's header.
		/// </summary>
		protected virtual void AddMetaTag(string name, string value)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
				return;

			HtmlMeta meta = new HtmlMeta();
			meta.Name = name;
			meta.Content = value;
			Page.Header.Controls.Add(meta);
		}

		/// <summary>
		/// Adds the generic link to the header.
		/// </summary>
		public virtual void AddGenericLink(string relation, string title, string href)
		{
			HtmlLink link = new HtmlLink();
			link.Attributes["rel"] = relation;
			link.Attributes["title"] = title;
			link.Attributes["href"] = href;
			Page.Header.Controls.Add(link);
		}

		/// <summary>
		/// Adds the generic link to the header.
		/// </summary>
		public virtual void AddGenericLink(string type, string relation, string title, string href)
		{
			HtmlLink link = new HtmlLink();
			link.Attributes["type"] = type;
			link.Attributes["rel"] = relation;
			link.Attributes["title"] = title;
			link.Attributes["href"] = href;
			Page.Header.Controls.Add(link);
		}

		/// <summary>
		/// Adds a JavaScript reference to the HTML head tag.
		/// </summary>
		public virtual void AddJavaScriptInclude(string url, bool placeInBottom, bool addDeferAttribute)
		{
			if (placeInBottom)
			{
                string script = "<script type=\"text/javascript\"" + (addDeferAttribute ? " defer=\"defer\"" : string.Empty) + " src=\"" + ResolveScriptUrl(url) + "\"></script>";
				ClientScript.RegisterStartupScript(GetType(), url.GetHashCode().ToString(), script);
			}
			else
			{
				HtmlGenericControl script = new HtmlGenericControl("script");
				script.Attributes["type"] = "text/javascript";
				script.Attributes["src"] = ResolveScriptUrl(url);
				if (addDeferAttribute)
				{
					script.Attributes["defer"] = "defer";
				}

				Page.Header.Controls.Add(script);
			}
		}

		/// <summary>
		/// Adds a Stylesheet reference to the HTML head tag.
		/// </summary>
		/// <param name="url">The relative URL.</param>
		public virtual void AddStylesheetInclude(string url)
		{
			HtmlLink link = new HtmlLink();
			link.Attributes["type"] = "text/css";
			link.Attributes["href"] = url;
			link.Attributes["rel"] = "stylesheet";
			Page.Header.Controls.Add(link);
		}

		/// <summary>
		/// Resolves the script URL.
		/// </summary>
		/// <param name="url">The URL.</param>
		/// <returns></returns>
		public virtual string ResolveScriptUrl(string url)
		{
			return Utils.RelativeWebRoot + "js.axd?path=" + HttpUtility.UrlEncode(url) + "&amp;v=" + TrainSettings.Instance.Version();
		}

		/// <summary>
		/// Adds a JavaScript to the bottom of the page at runtime.    
		/// </summary>
		/// <remarks>
		/// You must add the script tags to the TrainSettings.Instance.TrackingScript.
		/// </remarks>
		protected virtual void AddTrackingScript()
		{
			ClientScript.RegisterStartupScript(this.GetType(), "tracking", "\n" + TrainSettings.Instance.TrackingScript, false);
		}

		/// <summary>
		/// Adds code to the HTML head section.
		/// </summary>
		protected virtual void AddCustomCodeToHead()
		{
			string code = string.Format(CultureInfo.InvariantCulture, "{0}<!-- Start custom code -->{0}{1}{0}<!-- End custom code -->{0}", Environment.NewLine, TrainSettings.Instance.HtmlHeader);
			LiteralControl control = new LiteralControl(code);
			Page.Header.Controls.Add(control);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.TemplateControl.Error"></see> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
		protected override void OnError(EventArgs e)
		{
			HttpContext ctx = HttpContext.Current;
			Exception exception = ctx.Server.GetLastError();

			if (exception != null && exception.Message.Contains("callback"))
			{
				// This is a robot spam attack so we send it a 404 status to make it go away.
				ctx.Response.StatusCode = 404;
				ctx.Server.ClearError();
				Comment.OnSpamAttack();
			}

			base.OnError(e);
		}

		/// <summary>
		/// Initializes the <see cref="T:System.Web.UI.HtmlTextWriter"></see> object and calls on the child
		/// controls of the <see cref="T:System.Web.UI.Page"></see> to render.
		/// </summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"></see> that receives the page content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			//if (TrainSettings.Instance.RemoveWhitespaceInPages)
			//{
			//  using (HtmlTextWriter htmlwriter = new HtmlTextWriter(new System.IO.StringWriter()))
			//  {
			//    base.Render(new RewriteFormHtmlTextWriter(htmlwriter));
			//    string html = htmlwriter.InnerWriter.ToString();
			//    html = Utils.RemoveHtmlWhitespace(html);
			//    writer.Write(html);
			//  }
			//}
			//else
			//{
				base.Render(new RewriteFormHtmlTextWriter(writer));
			//}
		}

	}
}
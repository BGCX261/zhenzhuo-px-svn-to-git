#region Using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Security;
using System.Web.UI.WebControls;
using TrainEngine.Core;
using Page = System.Web.UI.Page;
using TrainEngine.Core.Classes;
using System.Web;
using System.IO;

#endregion

public partial class admin_profiles : Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        lbTitle.Text = "用户档案";
        if (!Page.IsPostBack)
        {
            ClearFormControls();
            SetDDLUser();
            SetPanelVisible();
            SetFields();
            SetProfile(ddlUserList.SelectedValue);

            dropdown.Visible = cbIsPublic.Enabled = Page.User.IsInRole(TrainSettings.Instance.AdministratorRole);
        }

        lbSaveProfile.Text = Resources.labels.saveProfile;
        Page.Title = Resources.labels.profile;

    }
    #endregion

    #region 各控件设置
    public void SetPanelVisible()
    {
        string uName = ddlUserList.SelectedValue;
        pnlTch.Visible = Roles.IsUserInRole(uName,"teachers");
        pnlFax.Visible = Roles.IsUserInRole(uName, "organs");
        pnlCampany.Visible = Roles.IsUserInRole(uName, "organs") || Roles.IsUserInRole(uName, "students");
        pnlAddress.Visible = Roles.IsUserInRole(uName, "organs");
        pnlAbout.Visible = Roles.IsUserInRole(uName, "organs") || Roles.IsUserInRole(uName, "teachers");
        pnlAbout2.Visible = Roles.IsUserInRole(uName, "teachers");
        if (Roles.IsUserInRole(uName, "Organs"))
        {
            lbDisplayName.InnerText = "联系人";
            lbAboutMe.Text = "公司简介";
            lbPhotoUrl.Text = "公司Logo";
        }
        else if (Roles.IsUserInRole(uName, "Teachers"))
        {
            lbAboutMe.Text = "讲师简介";
            lbDisplayName.InnerText = "讲师姓名";
            lbPhotoUrl.Text = "讲师照片";
        }
    }
	private void SetProfile(string name)
	{
		AuthorProfile pc = AuthorProfile.GetProfile(name);
		if (pc != null)
		{
           
			cbIsPublic.Checked = pc.IsPrivate;
            if (pc.IsTeacher && !pc.IsAdmin)
            {
                pnlTchGold.Visible = true;
                cbIsGoldTch.Checked = pc.IsGoldTch;
            }
            else
            {
                pnlTchGold.Visible = false;
            }
            cbIsVIP.Checked = pc.IsVip;
			tbDisplayName.Text = pc.DisplayName;
            txtNoMess.Text = pc.NoMess;
            //tbPhotoUrl.Text = pc.PhotoURL;
			tbPhoneMain.Text = pc.PhoneMain;
			tbPhoneMobile.Text = pc.PhoneMobile;
			tbPhoneFax.Text = pc.PhoneFax;
			
			tbCityTown.Text = pc.CityTown;
            tbPay.Text = pc.Pay;
            //set fields
            if (Roles.IsUserInRole(ddlUserList.SelectedValue, "Teachers"))
            {
                string fieldsString = pc.Fields;//形如:fieldId1|fieldId2...
                if (!string.IsNullOrEmpty(fieldsString))
                {
                    string[] sa = fieldsString.Split('|');
                    foreach (string item in sa)
                    {
                        cblFields.Items.FindByValue(item).Selected = true;
                    }
                }
            }
            
			tbCompany.Text = pc.Company;
            tbAddress.Text = pc.Address;

			tbAboutMe.Text = pc.AboutMe;

            tbAbout1.Text = pc.Description1;
            tbAbout2.Text = pc.Description2;
            lbUpPicId.Text = pc.PhotoURL;
            SetImageUrl(pc.PhotoURL);
		}
		else
		{
			// Clear any data in the form controls remaining from the last profile selected.
			ClearFormControls();
		}

		// Sync the dropdownlist user with the selected profile user.  This is
		// particularily needed on the initial page load (!IsPostBack).
		ListItem SelectedUser = FindItemInListControlByValue(ddlUserList, name);
		if (SelectedUser != null)
			SelectedUser.Selected = true;

		// Store the selected profile name so changes are saved to this
		// profile and not another profile the user may later select and
		// forget to click the lbChangeUserProfile button for.
		ViewState["selectedProfile"] = name;
	}

	/// <summary>
	/// Returns the ListItem that has a value matching the Value passed in
	/// via a OrdinalIgnoreCase search.
	/// </summary>
	private ListItem FindItemInListControlByValue(ListControl control, string Value)
	{
		foreach (ListItem li in control.Items)
		{
			if (string.Equals(Value, li.Value, StringComparison.OrdinalIgnoreCase))
				return li;
		}
		return null;
	}

	private void ClearFormControls()
	{
		cbIsPublic.Checked = false;
        cbIsGoldTch.Checked = cbIsVIP.Checked = false;
		tbDisplayName.Text = string.Empty;
        txtNoMess.Text = string.Empty;
        txtNoMess.Text = string.Empty;
		tbPhoneMain.Text = string.Empty;
		tbPhoneMobile.Text = string.Empty;
		tbPhoneFax.Text = string.Empty;
		
		tbCityTown.Text = string.Empty;
        tbPay.Text = string.Empty;
		tbCompany.Text = string.Empty;
        tbAddress.Text = string.Empty;

		tbAboutMe.Text = string.Empty;
        tbAbout1.Text = string.Empty;
        tbAbout2.Text = string.Empty;
        lbUpPicId.Text = string.Empty;

        foreach (ListItem item in cblFields.Items)
        {
            item.Selected = false;
        }
		
	}

	private void SetDDLUser()
	{
        int count = 0;
		foreach (MembershipUser user in Membership.GetAllUsers())
		{
			ListItem li = new ListItem(user.UserName, user.UserName);
            if (Request.Params["p"]!=null)
            {
                AuthorProfile ap = AuthorProfile.GetProfile(user.UserName);
                if (ap==null || (!ap.IsPrivate && ap.NoMess == string.Empty))
                {
                    ddlUserList.Items.Add(li);
                    count++;
                }
            }
            else
            {
                ddlUserList.Items.Add(li);
                count++;
            }
		}
        if (count == 0)
        {
            Response.Redirect(Utils.RelativeWebRoot + "admin/pages/default.aspx");
        }
        ddlUserList.SelectedValue = User.Identity.Name;
	}

    private void SetFields()
    {
        foreach (Field fld in Field.Fields)
        {
            ListItem li = new ListItem(fld.FieldName, fld.Id.ToString());
            cblFields.Items.Add(li);
        }
    }

    
    #endregion

    #region 控件事件
    protected void lbSaveProfile_Click(object sender, EventArgs e)
	{
		string userProfileToSave = ViewState["selectedProfile"] as string;
		AuthorProfile pc = AuthorProfile.GetProfile(userProfileToSave);
		if (pc == null)
			pc = new AuthorProfile(userProfileToSave);

		pc.IsPrivate = cbIsPublic.Checked;
        pc.IsVip = cbIsVIP.Checked;
        pc.IsGoldTch = cbIsGoldTch.Checked;
		pc.DisplayName = tbDisplayName.Text;
		

        //pc.PhotoURL = tbPhotoUrl.Text;
		pc.PhoneMain = tbPhoneMain.Text;
		pc.PhoneMobile = tbPhoneMobile.Text;
		pc.PhoneFax = tbPhoneFax.Text;
        //pc.EmailAddress = tbEmailAddress.Text;
		pc.CityTown = tbCityTown.Text;
        string fields = string.Empty;
        foreach (ListItem item in cblFields.Items)
        {
            if (item.Selected == true)
            {
                fields += item.Value + "|";
            }
        }
        pc.Fields = fields.TrimEnd('|');
        pc.Pay = tbPay.Text;
		pc.Company = tbCompany.Text;
        pc.Address = tbAddress.Text;

		pc.AboutMe = tbAboutMe.Text;
        pc.Description1 = tbAbout1.Text;
        pc.Description2 = tbAbout2.Text;

        pc.PhotoURL = lbUpPicId.Text;
        pc.NoMess = txtNoMess.Text;
		pc.Save();
	}
    protected void btnNo_Click(object sender, EventArgs e)
    {
        string userProfileToSave = ViewState["selectedProfile"] as string;
        AuthorProfile pc = AuthorProfile.GetProfile(userProfileToSave);
        if (pc == null)
            pc = new AuthorProfile(userProfileToSave);
        pc.IsPrivate = false;
        pc.NoMess = txtNoMess.Text;
        pc.Save();

    }
	protected void lbChangeUserProfile_Click(object sender, EventArgs e)
	{
        ClearFormControls();
        SetPanelVisible();
		SetProfile(ddlUserList.SelectedValue);
	}
    #endregion

    #region 上传图片相关
    private void SetImageUrl(string ResId)
	{
        if (ResId == null || ResId == string.Empty)
        {
            imgPic.ImageUrl = Utils.RelativeWebRoot + "pics/no_avatar.png";
            return;
        }
        Guid id;
        try
        {
            id = new Guid(ResId);
        }
        catch
        {
            return;
        }
        Res rs = Res.GetRes(new Guid(ResId));
        if (rs != null)
        {
            imgPic.ImageUrl = rs.GetResTempFilePath();
        }
        else
        {
            imgPic.ImageUrl = Utils.RelativeWebRoot + "pics/no_avatar.png";
        }
	}
    protected void btnUppic_Click(object sender, EventArgs e)
    {
        HttpPostedFile pf = fudPhoto.PostedFile;
        int intDocLen = pf.ContentLength;
        string contentType = pf.ContentType;
        if (!contentType.ToLower().StartsWith("image"))
        {
            lblMess.Text = "请选择图片类型的文件!";
            lblMess.Visible = true;
            return;
        }
        else if (pf.InputStream.Length> 512000 )
        {
            lblMess.Text = "选择图片文件大小超过500K!";
            lblMess.Visible = true;
            return;
        }
        else
        {
            lblMess.Text = "";
            lblMess.Visible = false;
        }
        if (lbUpPicId.Text != string.Empty)
        {
            Res rsd = Res.GetRes(new Guid(lbUpPicId.Text));
            rsd.Delete();
            rsd.Save();
        }
        byte[] Docbuffer = new byte[intDocLen];

        Stream objStream;
        objStream = pf.InputStream;
        objStream.Read(Docbuffer, 0, intDocLen);

        Res res = new Res();
        res.FileName = Path.GetFileName(pf.FileName);
        res.ResType = pf.ContentType;
        res.Description = "Profile";
        res.Author = Page.User.Identity.Name;
        res.Points = 0;
        res.Save();

        res.CurrentPostFileBuffer = Docbuffer;
        res.BlobUpdate();
        
        res.CurrentPostFileBuffer = null;
        string userProfileToSave = ViewState["selectedProfile"] as string;
        AuthorProfile pc = AuthorProfile.GetProfile(userProfileToSave);
        if (pc == null)
            pc = new AuthorProfile(userProfileToSave);

        pc.PhotoURL = lbUpPicId.Text = res.Id.ToString();
        pc.Save();

        SetImageUrl(pc.PhotoURL);
    }
    #endregion
    
}
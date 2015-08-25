<%@ Page Language="C#" MasterPageFile="~/admin/admin1.master" AutoEventWireup="true" ValidateRequest="False" CodeFile="Profiles.aspx.cs" Inherits="admin_profiles" Title="Modify Profiles" %>
<%@ Register Src="~/admin/htmlEditor.ascx" TagPrefix="Train" TagName="TextEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">

    <div class="settings" id="dropdown" runat="server"> 
        <h1>
            <asp:Label ID="lbTitle" runat="server" Text=""></asp:Label> 
        </h1>
        <asp:Panel ID="pnlAdmin" runat="server" Visible='<%# User.IsInRole("Administrator") %>'>
            <asp:DropDownList ID="ddlUserList" runat="server"></asp:DropDownList>
            <asp:LinkButton ID="lbChangeUserProfile" runat="server" 
                OnClick="lbChangeUserProfile_Click"><%= Resources.labels.switchUserProfile %></asp:LinkButton>
            <div style="margin-bottom:3px">
            <label for="<%=cbIsPublic.ClientID %>">审核是否通过</label>               
            <asp:CheckBox ID="cbIsPublic" runat="server" Enabled="false"/> 
            
                &nbsp;  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="审核不通过,请输入原因" ControlToValidate="txtNoMess" 
                    ValidationGroup="NO"></asp:RequiredFieldValidator>
      </div>
      <div style="margin-bottom:3px">
      <label for="<%=cbIsVIP.ClientID %>">VIP用户</label>               
            <asp:CheckBox ID="cbIsVIP" runat="server" /> 
      </div>
      <div style="margin-bottom:3px">
      <asp:Panel ID="pnlTchGold" runat="server" Visible='<%# User.IsInRole("Administrator") %>'>
      <label for="<%=cbIsGoldTch.ClientID %>">金牌显示</label>               
            <asp:CheckBox ID="cbIsGoldTch" runat="server" />
      </asp:Panel> 
      </div>
      <div style="margin-bottom:3px">
              <label for="<%=txtNoMess.ClientID %>">不通过原因</label>
              <asp:TextBox ID="txtNoMess" runat="server"></asp:TextBox>
              (如审核不通过,请输入原因)<asp:Button ID="btnNo" runat="server" Text="审核不通过" 
                  onclick="btnNo_Click" ValidationGroup="NO" />
            </div>
        </asp:Panel>
            
    </div>
    <div class="settings" style="padding: 0 10px">
        <asp:Image ID="imgPic" runat="server" Height="80px" Width="80px" 
            ImageUrl="~/pics/no_avatar.png" ToolTip="未设置图像" ImageAlign="Top" /><br />
            
        
      
       
      <div style="margin-bottom:3px">
      <label for="<%=tbDisplayName.ClientID %>" id="lbDisplayName" runat="server">显示名称</label>
      <asp:TextBox ID="tbDisplayName" runat="server"></asp:TextBox>
      </div>

      <div style="margin-bottom:3px">
      <label for="<%=tbPhoneMain.ClientID %>">固定电话</label>
      <asp:TextBox ID="tbPhoneMain" runat="server"></asp:TextBox>
      </div>
      <div style="margin-bottom:3px">
      <label for="<%=tbPhoneMobile.ClientID %>">手机号</label>   
      <asp:TextBox ID="tbPhoneMobile" runat="server"></asp:TextBox>
      </div>
      <asp:Panel ID="pnlFax" runat="server" >
      <div style="margin-bottom:3px">
      <label for="<%=tbPhoneFax.ClientID %>">传真号</label>
      <asp:TextBox ID="tbPhoneFax" runat="server"></asp:TextBox>
      </div>
      </asp:Panel>          
      <div style="margin-bottom:3px">
      <label for="<%=tbCityTown.ClientID %>">所在地区</label>
      <asp:TextBox ID="tbCityTown" runat="server"></asp:TextBox>
      </div>
      
      <asp:Panel ID="pnlTch" runat="server" >
      <div style="margin-bottom:3px">
      <label for="<%=tbPay.ClientID %>">对外课酬</label>
      <asp:TextBox ID="tbPay" runat="server"></asp:TextBox>
          <span style="color: #808080">元/天</span>
      </div>
      
      <div style="margin-bottom:3px">
      <label for="<%=cblFields.ClientID %>">培训领域</label>
          <asp:CheckBoxList ID="cblFields" runat="server" RepeatColumns="3" 
              CssClass="nowidth1">
          </asp:CheckBoxList>
      </div>
      </asp:Panel>
      <asp:Panel ID="pnlCampany" runat="server" >
      <div style="margin-bottom:3px">
      <label for="<%=tbCompany.ClientID %>">公司名称</label>
      <asp:TextBox runat="server" id="tbCompany" />
      </div>
      </asp:Panel>
      <asp:Panel ID="pnlAddress" runat="server" >
      <div style="margin-bottom:3px">
      <label for="<%=tbAddress.ClientID %>">地址</label>
      <asp:TextBox runat="server" id="tbAddress" />
      </div>
      </asp:Panel>
      
      <asp:Panel ID="pnlAbout" runat="server" >
          <div style="margin-bottom:3px">
          <asp:Label ID="lbAboutMe" runat="server"></asp:Label>
              <asp:Label ID="lbUpPicId" runat="server" Visible="False"></asp:Label>
              <br />
          <Train:TextEditor runat="server" id="tbAboutMe" TextMode="MultiLine"  />
          </div>      
          <div style="margin-bottom:3px">
          <asp:Label ID="lbAbout1" runat="server">曾服务客户</asp:Label><br />
          <%--<Train:TextEditor runat="server" id="tbAbout1" TextMode="MultiLine"  />--%>
          <script type="text/javascript">
              tinyMCE.init({
                  // General options
                  mode: "exact",
                  elements: "<%=tbAbout1.ClientID %>",
                  theme: "advanced",
                  plugins: "inlinepopups,fullscreen,contextmenu,emotions,table,advlink",
                  convert_urls: false,
                  language: "zh-cn",
                  // Theme options
                  theme_advanced_buttons1: "fullscreen,|,cut,copy,paste,|,undo,redo,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,outdent,indent,|,link,unlink,sub,sup,removeformat,cleanup,charmap,emotions,|,formatselect,fontselect,fontsizeselect",
                  theme_advanced_buttons2: "",
                  theme_advanced_toolbar_location: "top",
                  theme_advanced_toolbar_align: "left",
                  theme_advanced_statusbar_location: "bottom",
                  theme_advanced_resizing: true,

                  tab_focus: ":prev,:next"
              });
           </script>
          <asp:TextBox  ID="tbAbout1" runat="server" Height="200px" TextMode="MultiLine" Width="100%"></asp:TextBox>
          </div>
          <asp:Panel ID="pnlAbout2" runat="server" Visible='<%# User.IsInRole("Teachers") %>'>
                  <div style="margin-bottom:3px">
                  <asp:Label ID="lbAbout2" runat="server">主讲课程</asp:Label><br />
                  <%--<Train:TextEditor runat="server" id="tbAbout2" TextMode="MultiLine"  />--%>
                  <script type="text/javascript">
                      tinyMCE.init({
                          // General options
                          mode: "exact",
                          elements: "<%=tbAbout2.ClientID %>",
                          theme: "advanced",
                          plugins: "inlinepopups,fullscreen,contextmenu,emotions,table,advlink",
                          convert_urls: false,
                          language: "zh-cn",
                          // Theme options
                          theme_advanced_buttons1: "fullscreen,|,cut,copy,paste,|,undo,redo,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,outdent,indent,|,link,unlink,sub,sup,removeformat,cleanup,charmap,emotions,|,formatselect,fontselect,fontsizeselect",
                          theme_advanced_buttons2: "",
                          theme_advanced_toolbar_location: "top",
                          theme_advanced_toolbar_align: "left",
                          theme_advanced_statusbar_location: "bottom",
                          theme_advanced_resizing: true,

                          tab_focus: ":prev,:next"
                      });
                    </script>
                  <asp:TextBox ID="tbAbout2" runat="server" Height="200px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                  </div>
              </asp:Panel>
          <div style="margin-bottom:3px">
            <asp:Label ID="lbPhotoUrl"  runat="server"></asp:Label>
            <asp:FileUpload ID="fudPhoto" runat="server" Width="400px" />
             &nbsp;<asp:Button ID="btnUppic" runat="server" Text="上传" 
                  onclick="btnUppic_Click" />
              <asp:Label ID="lblMess" runat="server" ForeColor="Red" Visible="False"></asp:Label>
          </div>
      </asp:Panel>
      
    </div>
    <p style="text-align:right;">
        <asp:button ID="lbSaveProfile" runat="server" OnClick="lbSaveProfile_Click" />
    </p>
    
</asp:Content>

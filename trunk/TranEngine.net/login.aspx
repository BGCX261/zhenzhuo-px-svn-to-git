<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div style="text-align:center;height: 250px;margin-top: 50px;width: 100%;">
<div style="float:left;width:50%">
<table cellspacing="2" cellpadding="4" width="75%">
                    <tr>
                        <td colspan="2" height="40">
                            欢迎使用
                        </td>
                    </tr>
                    <tr>
                        <td width="4">
                        </td>
                        <td align="left">
                            <asp:Label ID="lbRef1" runat="server">1. 如有可能，请选择Microsoft IE 6.0或以上版本的浏览器以及1024*768的屏幕分辨率，以获得最佳效果</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="4">
                        </td>
                        <td align="left">
                            <asp:Label ID="lbRef2" runat="server">2. 本系统使用了Cookie来标识用户，请确认您的浏览器设置，如果您禁用了Cookie，可能将无法登陆；本系统保证没有使用任何有害和非安全的Cookie来暴露</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="4">
                        </td>
                        <td align="left">
                            <asp:Label ID="lbRef3" runat="server">3.本系统使用JavaScript脚本程序和弹出框，请关掉上网助手和防毒软件的屏蔽功能，否则可能无法正常使用！ </asp:Label>
                        </td>
                    </tr>
                </table>
</div>
<div style="float:left;margin-top: 50px">
<asp:Label id="lbErr" runat="server" Text="Err" Visible="false"></asp:Label>
<asp:Login ID="Login1" runat="server" class="loginbox" BorderPadding="25">
    <TextBoxStyle Font-Size="12pt" width="150" />   
</asp:Login>

  <asp:changepassword runat="server" id="changepassword1" visible="false" />
  <br /><br />
  <asp:loginstatus runat="server" id="lsLogout" visible="false" />
</div>
</div>
</asp:Content>
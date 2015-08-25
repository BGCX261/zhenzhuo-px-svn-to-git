<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RZView.aspx.cs" Inherits="Views_RZView" %>
<%@ Register Src="..\User controls\RZ\GridRzSf.ascx" TagName="GridRzsf" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../<%=pagestring %>'><%=rvc.RzType%></a> > <%=rvc.Title %> </div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<div class="ContentTitle">
        <%=rvc.Title %>
    </div>    
  	
  	<div class="ContentMain" style="display:block;">
        <%=rvc.Content %>
    </div>
    
  </div>
</div>
<div id="DivRight">
<div style="margin:5px 0 0 0" class="Div_heard">体系认证
    <div class="list" >
      <uc1:GridRzsf id="GridRzsf1" RzType="体系认证"  runat="server"></uc1:GridRzsf>
      </div>
  </div>
  <div style="margin:5px 0 0 0" class="Div_heard">产品认证
    <div class="list" >
      <uc1:GridRzsf id="GridRzsf2" RzType="产品认证" runat="server"></uc1:GridRzsf>
      </div>
  </div>
    <div style="margin:5px 0 0 0" class="Div_heard">体系认证收费
    <div class="list" >
      <uc1:GridRzsf id="GridRzList3" RzType="体系认证收费" pType="认证收费" runat="server"></uc1:GridRzsf>
      </div>
  </div>
  <div style="margin:5px 0 0 0" class="Div_heard">产品认证收费
  <div class="list" >
      <uc1:GridRzsf id="GridRzList4" RzType="产品认证收费" pType="认证收费" runat="server"></uc1:GridRzsf>
      </div>
  </div>
</div>
<div style="clear:both"></div>


</asp:Content>


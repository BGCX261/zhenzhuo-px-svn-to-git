<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rztx.aspx.cs" Inherits="Rztx" %>
<%@ Register Src=".\User controls\RZ\GridRzList.ascx" TagName="GridRzList" TagPrefix="uc1" %>
<%@ Register src=".\User controls\GridGoldLeft.ascx" tagname="DGridGoldLeft" tagprefix="uc2" %>
<%@ Register Src=".\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DGridHotTeachers" TagPrefix="uc5" %>
<%@ Register Src=".\User controls\RZ\GridRzSf.ascx" TagName="GridRzsf" TagPrefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content" style="padding-top:5px;">
  	<uc1:GridRzList id="DGridOrgansList" RzType="体系认证" runat="server"></uc1:GridRzList>
  </div>
</div>

<div id="DivRight">
<div  class="Div_heard">体系认证收费
    <div class="list" >
      <uc6:GridRzsf id="GridRzList1" RzType="体系认证收费" pType="认证收费" runat="server"></uc6:GridRzsf>
      </div>
  </div>
  <div id="right_top" class="Div_heard">金牌公开课
      <uc2:DGridGoldLeft id="DGridGoldLeft" runat="server" pSource = "right"></uc2:DGridGoldLeft>
  </div>
  <div style="margin:5px 0 0 0" class="Div_heard">精品内训课程
    <div class="list" >
    <uc3:DataGrid id="DataGrid3" runat="server" isTopShow="false"></uc3:DataGrid>
    </div>
  </div>
  <div id="right_tch" class="Div_heard">人气讲师
    <uc5:DGridHotTeachers id="DGridHotTeachers" runat="server"></uc5:DGridHotTeachers> 
  </div>
  
</div>
<div style="clear:both"></div>
</asp:Content>

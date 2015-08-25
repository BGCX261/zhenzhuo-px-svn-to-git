<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Organs.aspx.cs" Inherits="Organs" title="培训机构"%>
<%@ Import Namespace="TrainEngine.Core.Classes" %>
<%@ Register Src=".\User controls\Teacher\GridOrgansList.ascx" TagName="DGridOrgansList" TagPrefix="uc1" %>
<%@ Register src=".\User controls\GridGoldLeft.ascx" tagname="DGridGoldLeft" tagprefix="uc2" %>
<%@ Register Src=".\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DGridHotTeachers" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content" style="padding-top:5px;">
  	<uc1:DGridOrgansList id="DGridOrgansList" runat="server"></uc1:DGridOrgansList>
  </div>
</div>

<div id="DivRight">
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
  <div id="right_pic1_ggk"><img name="" src="images/pic20.jpg" width="250" height="165" alt="" /></div>
</div>
<div style="clear:both"></div>
</asp:Content>

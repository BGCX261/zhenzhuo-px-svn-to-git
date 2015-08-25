<%@ Page  Language="C#"  AutoEventWireup="true" CodeFile="CurriculaView.aspx.cs" Inherits="Views_CurriculaView" %>
<%@ Register src=".\..\User controls\GridGold.ascx" tagname="DGridGold" tagprefix="uc2" %>
<%@ Register src=".\..\User controls\GridSellWell.ascx" tagname="DGridSellWell" tagprefix="uc3" %>
<%@ Register src=".\..\User controls\GridOpenInfo.ascx" tagname="DGridOpenInfo" tagprefix="uc1" %>
<%@ Register src=".\..\User controls\GridCorrelation.ascx" tagname="DGridCorrelation" tagprefix="uc4" %>
<%@ Register src=".\..\User controls\GridGoldLeft.ascx" tagname="DGridGoldLeft" tagprefix="uc5" %>
<%@ Register Src=".\..\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc6" %>
<%@ Register Src=".\..\User controls\Teacher\GridHotTeachers.ascx" TagName="DGridHotTeachers" TagPrefix="uc7" %>

<%@ Import Namespace="TrainEngine.Core.Classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../Curricula.aspx'>公开课</a> > <a href='../CurriculaList.aspx?FieldID=<%=FieldID %>'><%=Field.GetField(new Guid(FieldID)).FieldName %></a> > <%=CName %> </div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<div class="ContentTitle">
        <asp:Label ID="lbTitle" runat="server"></asp:Label>
    </div>    
  	<div class="ContentIndex">开课信息</div>
    <div>
        <uc1:DGridOpenInfo id="DGridOpenInfo" runat="server"></uc1:DGridOpenInfo>
    </div>
    <div class="ContentIndex">招生对象</div>
  	<div class="ContentMain"><%=zsdx%></div>
    <div class="ContentIndex">课程介绍</div>
    <div class="ContentMain"><%=kcxq%></div>
    <div class="ContentIndex">相关公开课</div>
    <div>
        <uc4:DGridCorrelation id="DGridCorrelation" runat="server"></uc4:DGridCorrelation>
    </div>
    <div class="ContentIndex">上一个公开课：<a href='CurriculaView.aspx?id=<%=qid %>'><%=qkc%></a></div>
    <div class="ContentIndex">下一个公开课：<a href='CurriculaView.aspx?id=<%=hid %>'><%=hkc%></a></div>
    <div class="ContentIndex">在线报名咨询</div>

  </div>
</div>
<div id="DivRight">
  <div id="right_top" class="Div_heard">金牌公开课
      <uc5:DGridGoldLeft id="DGridGoldLeft" runat="server" pSource = "right"></uc5:DGridGoldLeft>
  </div>
  <div style="margin:5px 0 0 0" class="Div_heard">精品内训课程
  <div class="list" >
    <uc6:DataGrid id="DataGrid" runat="server" isTopShow="false"></uc6:DataGrid>
  </div>
  </div>
  <div id="right_tch" class="Div_heard">人气讲师
    <uc7:DGridHotTeachers id="DGridHotTeachers" runat="server"></uc7:DGridHotTeachers> 
  </div>
</div>
<div style="clear:both"></div>


</asp:Content>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurriculaList.aspx.cs" Inherits="CurriculaList" Title="公开课列表" %>
<%@ Register src=".\User controls\GridOpenInfo.ascx" tagname="DGridOpenInfo" tagprefix="uc1" %>
<%@ Register src=".\User controls\GridCorrelation.ascx" tagname="DGridCorrelation" tagprefix="uc4" %>
<%@ Register src=".\User controls\ListField.ascx" tagname="DListField" tagprefix="uc5" %>
<%@ Register src=".\User controls\ListCategories.ascx" tagname="DListCategories" tagprefix="uc6" %>
<%@ Register src=".\User controls\ListDate.ascx" tagname="DListDate" tagprefix="uc8" %>
<%@ Register src=".\User controls\ListCity.ascx" tagname="DListCity" tagprefix="uc7" %>
<%@ Import Namespace="TrainEngine.Core.Classes" %>
<%@ Register Src=".\User controls\GridCurriculaList.ascx" TagName="DGridListCurricula" TagPrefix="uc9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="Div_Position" id="DivPosition" > 您的位置：<a href='Default.aspx'>首页</a> > <a href='Curricula.aspx'>公开课</a> > <%=strPosition %> </div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<uc9:DGridListCurricula id="DGridListCurricula" runat="server"></uc9:DGridListCurricula>

  </div>
</div>

<div id="DivRight">
  <div id="right_date_ggk" class="Div_heard">培训日历
    <uc8:DListDate id="DListDate" runat="server"></uc8:DListDate>
  </div>
  <div id="right_dq_ggk" class="Div_heard">地区公开课
    <uc7:DListCity id="DListCity" runat="server"></uc7:DListCity>
  </div>
  <div id="right_ly_ggk" class="Div_heard">热门培训专题
    <div>
        <uc5:DListField id="DListField" runat="server"></uc5:DListField>            
    </div>
  </div>
  <div id="right_pic1_ggk"><img name="" src="images/pic20.jpg" width="250" height="165" alt="" /></div>
  <div id="right_hy_ggk" class="Div_heard">行业体系
    <div>
        <uc6:DListCategories id="DListCategories" runat="server"></uc6:DListCategories>            
    </div>
  </div>
  <div id="right_pic_ggk"><img name="" src="images/pic22.jpg" width="250" height="200" alt="" /></div>
</div>


</asp:Content>

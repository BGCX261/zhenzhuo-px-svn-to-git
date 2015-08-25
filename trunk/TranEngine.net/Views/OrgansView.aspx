<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgansView.aspx.cs" Inherits="Views_OrgansView" %>
<%@ Import Namespace="TrainEngine.Core.Classes" %>
<%@ Register src=".\..\User controls\Teacher\GridOrgansCurricula.ascx" tagname="DGridOrgansCurricula" tagprefix="uc4" %>
<%@ Register src=".\..\User controls\Teacher\GridOrgansTraining.ascx" tagname="DGridOrgansTraining" tagprefix="uc3" %>
<%@ Register Src=".\..\User controls\Teacher\GridOrgansRes.ascx" TagName="DGridOrgansRes" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../Organs.aspx'>培训机构</a> > <%=strTitle%></div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<div class="ContentTitle">
        <asp:Label ID="lbTitle" runat="server"></asp:Label>
    </div>    
  	<div class="ContentIndex">所在城市：<%=strCity%></div>
    <div class="ContentIndex">公司介绍：</div>
  	<div class="ContentMain"><img src='<%=SetImageUrl(ap.PhotoURL) %>' width="80px" height="80px" alt="" style="border: 1px solid #C0C0C0" /><%=strGSJS%></div>
    <div class="ContentIndex">服务过的客户</div>
    <div class="ContentMain"><%=strFWGKH%></div>
  </div>
</div>

<div id="DivRight">
    <div id ="right_top" class="Div_heard">
		近期公开课
        <div class="list" id="cxgkk">
            <uc4:DGridOrgansCurricula id="DGridOrgansCurricula" runat="server"></uc4:DGridOrgansCurricula>
	    </div>
    </div>
    <div id="right_top" class="Div_heard">
		近期内训课程
        <div class="list">
            <uc3:DGridOrgansTraining id="DGridOrgansTraining" runat="server"></uc3:DGridOrgansTraining>
	    </div>
    </div>
    <div id="middle_center" ><img src="images/pic1.png" width="250" height="160" /></div>
	<div id="right_org" class="Div_heard">相关资料
        <div class="list">
            <uc2:DGridOrgansRes id="DGridOrgansRes" runat="server"></uc2:DGridOrgansRes> 
        </div>
	</div>

    <div id="middle_center" ><img src="images/pic1.png" width="250" height="160" /></div>
</div>
<div style="clear:both"></div>


</asp:Content>

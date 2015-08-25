<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherView.aspx.cs" Inherits="Views_TeacherView" %>
<%@ Register Src=".\..\User controls\Teacher\GridTeacherTraining.ascx" TagName="DataGrid" TagPrefix="uc1" %>
<%@ Register Src=".\..\User controls\Teacher\GridTeacherRes.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\..\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../Teachers.aspx'>培训讲师</a> > <%=tch.DisplayName%> </div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<div class="ContentTitle">
        <%=tch.DisplayName%>
        <div class="div_info">所在城市：<span><%=tch.CityTown%></span>　培训领域：<span><%=GetFieldsString(tch.Fields)%></span></div>
    </div>    
  	
  	<div class="ContentMain" style="display:list-item;"><img src='<%=SetImageUrl(tch.PhotoURL) %>' width="80px" height="80px" alt="" style="border: 1px solid #C0C0C0" /> <%=tch.AboutMe%></div>
    <div class="ContentIndex">服务过的客户</div>
    <div class="ContentMain"><%=tch.Description1%></div>
    <div class="ContentIndex">主讲课程</div>
    <div class="ContentMain"><%=tch.Description2%></div>
  </div>
</div>
<div id="DivRight">
    <div id ="right_top" class="Div_heard">
		相关内训课程
        <div class="list" id="cxgkk">
        <uc1:DataGrid id="DataGrid1" runat="server"></uc1:DataGrid> 
	    </div>
    </div>
    <div id="right_tch" class="Div_heard">人气讲师
          <uc3:DataGrid id="DataGrid5" runat="server"></uc3:DataGrid> 
		</div>
    <div id="middle_center" ><img src="images/pic1.png" width="250" height="160" /></div>
	<div id="right_org" class="Div_heard">相关资料
        <div class="list">
        <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid> 
        </div>
	</div>
</div>
<div style="clear:both"></div>


</asp:Content>

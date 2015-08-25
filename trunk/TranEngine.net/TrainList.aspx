<%@ Page  Language="C#" AutoEventWireup="true" CodeFile="TrainList.aspx.cs"
    Inherits="Views_TrainList" %>

<%@ Register Src=".\User controls\Training\GridField.ascx" TagName="DataGrid" TagPrefix="uc1" %>
<%@ Register Src=".\User controls\Training\GridCategory.ascx" TagName="DataGrid"
    TagPrefix="uc2" %>
<%@ Register Src=".\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc4" %>
<%@ Register Src=".\User controls\Training\GridTrainList.ascx" TagName="DataGrid" TagPrefix="uc5" %>
<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">
<div id="Training">
<div class="Div_Position" id="DivPosition" > 您的位置：<a href='Default.aspx'>首页</a> > <a href='Training.aspx'>内训课程</a> > <%=strPosition %> </div>

<div id="DivLeftMiddle">
<div class="Div_Content">
<uc5:DataGrid id="DataGrid5" runat="server"></uc5:DataGrid>
</div>
</div>

<div id="DivRight">
<%if (isFields)
  {%>
      <div style="margin:5px 0 0 0" class="Div_heard">内训专题
    <div class="list" id="Div1">
         <uc1:DataGrid id="DataGrid1" runat="server"></uc1:DataGrid>
	</div>
  </div>
 <% }
  else
  { %>

  <div style="margin:5px 0 0 0" class="Div_heard">行业体系
    <div class="list" id="Div3">
         <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid>
	</div>
  </div>
  <%} %>
  <div style="margin:5px 0 0 0" class="Div_heard">精品内训课程
<div class="list" >
<uc3:DataGrid id="DataGrid3" runat="server" isTopShow="false"></uc3:DataGrid>
</div>
</div>

  <div style="margin:5px 0 0 0" class="Div_heard">热门内训
		    <div class="list" id="cxgkk">
		     <uc4:DataGrid id="DataGrid4" runat="server"></uc4:DataGrid> 		    
		    </div>
	  	</div>
</div>
</div>
</asp:content>

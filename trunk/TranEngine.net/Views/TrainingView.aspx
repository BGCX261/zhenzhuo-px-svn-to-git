<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingView.aspx.cs" Inherits="Views_TrainingView" %>
<%@ Import Namespace="TrainEngine.Core.Classes" %>
<%@ Register Src=".\..\User controls\Training\GridField.ascx" TagName="DataGrid" TagPrefix="uc1" %>
<%@ Register Src=".\..\User controls\Training\GridCategory.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\..\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\..\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../Training.aspx'>内训课程</a> <%if (paramName != string.Empty)
                                                                                                                           {%>> <a href='../TrainList.aspx?<%=paramString %>'><%=paramName%></a><%} %> > <%=cTraining.Title%></div>
    <asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content">
	<div class="ContentTitle">
        <%=cTraining.Title%>
        <div class="div_info">课程类型：<span>内训课程</span>　培训天数：<span><%=cTraining.Days%>天</span>　授课专家：<span><%=TeacherString %></span></div>
      
    </div>    
  	<%--<div class="ContentMain"><%=zsdx%></div>--%>
    <div class="ContentIndex">课程介绍</div>
    <div class="ContentMain"><%=cTraining.Content%></div>
    <asp:Panel id="pnltch" runat="server">
    <div class="ContentIndex">讲师介绍</div>
    <div class="ContentMain"><%=TeacherDes%></div>
    </asp:Panel>
    <div class="ContentIndex">相关内训课程</div>
    <div class="list" style="text-align:left">上一个内训课程：<%if (cTraining.Previous != null)
                                                        {%><a href='TrainingView.aspx?id=<%=cTraining.Previous.Id %>'><%=cTraining.Previous.Title%></a><% }
                                                        else { %>无 <%} %></div>
    <div class="list"style="text-align:left">下一个内训课程：<%if (cTraining.Next != null)
                                                        {%><a href='TrainingView.aspx?id=<%=cTraining.Next.Id  %>'><%=cTraining.Next.Title%></a><% }
                                                        else { %>无 <%} %></div>
    <div class="ContentIndex">在线报名咨询</div>

  </div>
</div>
<div id="DivRight">
    <div style="margin:5px 0 0 0" class="Div_heard">内训专题
    <div class="list" id="Div2">
         <uc1:DataGrid id="DataGrid1" runat="server"></uc1:DataGrid>
	</div>
  </div>
    <div style="margin:5px 0 0 0" class="Div_heard">行业体系
    <div class="list" id="Div3">
         <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid>
	</div>
  </div>
    <div style="margin:5px 0 0 0" class="Div_heard">精品内训课程
<div class="list" >
<uc3:DataGrid id="DataGrid3" runat="server" isTopShow="false"></uc3:DataGrid>
</div>
</div>
    <div style="margin:5px 0 0 0" class="Div_heard">热门内训
		    <div class="list" id="Div1">
		     <uc4:DataGrid id="DataGrid4" runat="server"></uc4:DataGrid> 		    
		    </div>
	  	</div>
	
</div>
<div style="clear:both"></div>
</asp:Content>
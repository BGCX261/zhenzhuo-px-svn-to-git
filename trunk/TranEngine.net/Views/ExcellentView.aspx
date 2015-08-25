<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcellentView.aspx.cs" Inherits="Views_ExcellentView" %>
<%@ Register src="..\User controls\GridHots.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register Src="..\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src="..\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<div class="Div_Position" id="DivPosition" > 您的位置：<a href='../Default.aspx'>首页</a> > <a href='../Excellent.aspx'>精彩展示</a> > <%=ex.Title%> </div>
<asp:Label ID="lbID" runat="server" Text="Label" Visible =" false"></asp:Label>
<div id="DivLeftMiddle">
  <div class="Div_Content" >
  <br />
	<table border="0" cellpadding="0" cellspacing="0" width="100%" 
    style="border-color: #FFFFFF; border-width: 0px 5px 0px 5px; border-style: none solid none solid; ">
    <tr>
    <td style="text-align:left; width=160px">
        <div style="border: 1px solid #CCCCCC;float: left;height: 120px;padding: 4px;width: 150px; margin-left:5px;">
        <a id="a1"  target="_blank" href="<%=SetImageUrl(ex.MastPic) %>" title='<%=ex.Title%>'>
            <img src='<%=SetImageUrl(ex.MastPic) %>' width="150px" height="120px" alt='<%=ex.Title%>'  />
        </a>
        </div>
    </td>
    <td class="blklist" valign="top" align="left" width="70%">
        <p><div style=' display: block;' ><strong>培训主题:<a id="aPages"  href="Javascript:void(0)" title='<%=ex.Title%>'><%=StripString(ex.Title, 50, false)%></a></strong></div></p>
        <p><div style=' display: block;'><strong>所在城市:</strong><%=ex.CityTown%></div></p>
        <p><div style=' display: block;'><strong>培训时间:</strong><%=ex.TrainingDate%></div></p>
    </td>
    </tr>
    <tr>
    <td colspan="2" class="blklist" align="left">
        <br />
        <strong style="font-size:14px">   相关图片</strong>
        <div class='hr'></div>
    </td>
    </tr>    
    <td colspan="2" class="blklist" align="left">
           <ul id="picList" class="ul2" runat="server"/>
    </td>
    </tr>     
</table>
  </div>
</div>
<div id="DivRight">
<div class="Div_heard" style="margin:5px 0 0 0">热门公开课
 <uc1:DataGrid id="hots" runat="server"></uc1:DataGrid>
</div>
<div class="Div_heard" style="margin:5px 0 0 0">热门内训课程
<div class="list" id="cxgkk" >
		     <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid> 		    
		    </div>
</div>
<div id="right_tch" class="Div_heard">人气讲师
          <uc3:DataGrid id="DataGrid5" runat="server"></uc3:DataGrid> 
		</div>
</div>
<div style="clear:both"></div>
</asp:Content>

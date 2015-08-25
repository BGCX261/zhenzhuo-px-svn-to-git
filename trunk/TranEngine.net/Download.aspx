<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download"
    Title="资料下载" %>

<%@ Register Src=".\User controls\GridHots.ascx" TagName="DataGrid" TagPrefix="uc1" %>
<%@ Register Src=".\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\User controls\DownLoad\GridResList.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\DownLoad\GrideResNews.ascx" TagName="DataGrid"
    TagPrefix="uc4" %>
<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">
<div id="Training">
<div id="DivLeftMiddle">
<div class="Div_Content">
    <uc3:DataGrid id="DataGrid1" runat="server" ></uc3:DataGrid>
</div>
</div>

<div id="DivRight">
<div id="right_tch" class="Div_heard">最新资料
<div class="list">
<uc4:DataGrid id="DataGrid3" runat="server" ></uc4:DataGrid>
</div>
</div>
<div class="Div_heard" style="margin:5px 0 0 0">热门公开课
 <uc1:DataGrid id="hots" runat="server" pSource = "right"></uc1:DataGrid>
</div>
<div class="Div_heard" style="margin:5px 0 0 0">热门内训课程
    <div class="list" id="cxgkk" >
	    <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid> 		    
    </div>
</div>


</div>
</asp:content>

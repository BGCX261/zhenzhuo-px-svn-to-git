<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Excellent.aspx.cs" Inherits="Excellent" title="精彩展示"%>
<%@ Register src=".\User controls\GridHots.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register Src=".\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Excellent\GridExcellentList.ascx" TagName="DataGrid" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div id="DivLeftMiddle">
    <div class="Div_Content" style="padding-top:5px;">
    	<uc4:DataGrid id="DataGrid1" runat="server"></uc4:DataGrid> 
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

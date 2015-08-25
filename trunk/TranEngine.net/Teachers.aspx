<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Teachers.aspx.cs" Inherits="Teachers" title="培训讲师"%>
<%@ Register src=".\User controls\GridHots.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register Src=".\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\User controls\Teacher\GridGoldTeachers.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Teacher\GridTeachers.ascx" TagName="DataGrid" TagPrefix="uc4" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc5" %>
<%@ Import Namespace="TrainEngine.Core.Classes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<div id="DivLeftMiddle">
    <div id="Div_middle_top" class="Div_heard">金牌讲师			
		<uc3:DataGrid id="DataGrid3" runat="server"></uc3:DataGrid> 	
	 </div>
     <div id="Div_middle_top" class="Div_heard">推荐讲师			
		<uc4:DataGrid id="DataGrid1" runat="server"></uc4:DataGrid> 	
	 </div>
</div>
<div id="DivRight">
<div id="right_tch" class="Div_heard">人气讲师
          <uc5:DataGrid id="DataGrid5" runat="server"></uc5:DataGrid> 
		</div>
<div class="Div_heard" style="margin:5px 0 0 0">热门公开课
 <uc1:DataGrid id="hots" runat="server"></uc1:DataGrid>
</div>
<div class="Div_heard" style="margin:5px 0 0 0">热门内训课程
<div class="list" id="cxgkk" >
		     <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid> 		    
		    </div>
</div>
</div>
<div style="clear:both"></div>
</asp:Content>

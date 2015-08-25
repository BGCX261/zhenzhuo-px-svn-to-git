<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Training.aspx.cs" Inherits="Training"    Title="企业内训" %>
<%@ Register Src=".\User controls\Training\GridField.ascx" TagName="DataGrid" TagPrefix="uc1" %>
<%@ Register Src=".\User controls\Training\GridCategory.ascx" TagName="DataGrid" TagPrefix="uc2" %>
<%@ Register Src=".\User controls\Training\GridGold.ascx" TagName="DataGrid" TagPrefix="uc3" %>
<%@ Register Src=".\User controls\Training\GridHots.ascx" TagName="DataGrid" TagPrefix="uc4" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc5" %>
<%@ Register src=".\User controls\Teacher\GridOrgans.ascx" tagname="DGridOrgans" tagprefix="uc6" %>

<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">
<div id="Training">
<div id="DivLeft">
<div class="PicAD"><img src="images/pic21.gif" width="280" height="160" alt="" /></div>
<div id = "train_left_zt" class="Div_heard">内训专题
    <div class="list" id="Div1">
         <uc1:DataGrid id="DataGrid1" runat="server"></uc1:DataGrid>
	</div>
  </div>
  <div id = "train_left_hy" class="Div_heard">行业体系
    <div class="list" id="Div3">
         <uc2:DataGrid id="DataGrid2" runat="server"></uc2:DataGrid>
	</div>
  </div>
</div>
<div id="DivMiddle">
<div id="train_middle_top" class="Div_heard">精品内训课程
<div class="list" >
<uc3:DataGrid id="DataGrid3" runat="server"></uc3:DataGrid>
</div>
</div>
<link rel="stylesheet" type="text/css" href="tab/tabview.css" />
    <link rel="stylesheet" type="text/css" href="tab/fonts-min.css" />
    <script src="tab/dom-event.js"></script>
    <script src="tab/element-min.js"></script>
    <script src="tab/connection-min.js"></script>
    <script src="tab/tabview-min.js"></script>
    
<asp:Literal id="ltContainer" runat="server"></asp:Literal>

</div>
<div id="DivRight">
		<div id ="right_top" class="Div_heard">热门内训
		    <div class="list" id="cxgkk">
		     <uc4:DataGrid id="DataGrid4" runat="server"></uc4:DataGrid> 		    
		    </div>
	  	</div>
		<div id="right_tch" class="Div_heard">人气讲师
          <uc5:DataGrid id="DataGrid5" runat="server"></uc5:DataGrid> 
		</div>
		<div id="right_org" class="Div_heard">培训机构
        <div class="list">
            <uc6:DGridOrgans id="DGridNewOrangs" runat="server"></uc6:DGridOrgans>
        </div>
		</div>
	</div>
</div>
<div style="clear:both"></div>
</asp:content>

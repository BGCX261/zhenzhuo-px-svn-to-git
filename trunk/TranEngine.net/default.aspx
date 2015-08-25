<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_Default" Title="首页"%>
<%@ Register src=".\User controls\GridHots.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register src=".\User controls\GridGold.ascx" tagname="DGridGold" tagprefix="uc2" %>
<%@ Register src=".\User controls\GridSellWell.ascx" tagname="DGridSellWell" tagprefix="uc3" %>
<%@ Register src=".\User controls\GridNew.ascx" tagname="DGridNew" tagprefix="uc4" %>
<%@ Register src=".\User controls\Teacher\GridOrgans.ascx" tagname="DGridOrgans" tagprefix="uc5" %>
<%@ Register Src=".\User controls\Teacher\GridHotTeachers.ascx" TagName="DataGrid" TagPrefix="uc6" %>
<%@ Register Src=".\User controls\Excellent\GridExcellentGold.ascx" TagName="DataGrid" TagPrefix="uc7" %>
<%@ Register Src=".\User controls\RZ\GridRZMainList.ascx" TagName="DataGrid" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<%--<form id="form1" runat="server">--%>
<div id="DivLeft">
    <div id="left_search1" class="Div_heard">论证询价	
      <div>
      <label>论证行业</label>
		    <input name="searchFor" type="text" size="10" />
		    <input name="goButton" type="submit" value="搜索"  />	
	  </div>  
    </div>
    <div id="left_search" class="Div_heard" >课程搜索	
        <div>
              <table width="240" border="0">
                <tr>
                  <td height="22"><label>课程名称</label></td>
                  <td colspan="2" alian="left"  width="170"><asp:TextBox ID="txtSearch" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                  <td height="22"><label>课程类别</label></td>
                  <td width="100">
                    <asp:DropDownList ID="DropDownList1" runat="server"  >
                        <asp:ListItem Selected="True">公开课程</asp:ListItem>
                        <asp:ListItem>内训课程</asp:ListItem>
                    </asp:DropDownList>                  </td>
                  <td alian="left" width="65"><asp:Button ID="btnSearch" runat="server" Text="搜索" onclick="btnSearch_Click" /></td>
                </tr>
              </table>
		    
        </div>  
        <div>
            
            
                
        </div>  
    </div>

	<div class="Div_heard" id="left_hot">热门公开课
        <uc1:DataGrid id="hots" runat="server"></uc1:DataGrid>
	</div>
		<div class="Div_heard" id="left_ext">精彩展示
		  <uc7:DataGrid id="DataGrid1" runat="server"></uc7:DataGrid>
		</div>
	</div>
<div id="DivMiddle">
		<div id="middle_center" ><img src="images/pic1.png" width="415" height="160" /></div>
		<div id="middle_new" class="Div_heard"><span class="STYLE6">最新公开课</span>			
		  <div>
		    <uc4:DGridNew id="DGridNew" runat="server"></uc4:DGridNew>            
	      </div>
	  </div>
		<div id="middle_gold" class="Div_heard"><strong>金牌公开课</strong>
		    <uc2:DGridGold id="DGridGold" runat="server"></uc2:DGridGold>
		</div>
		<div id="middle_rz" class="Div_heard">认证
            <div class="list">
            <uc8:DataGrid id="DGridGold1" runat="server"></uc8:DataGrid>
            </div>
		</div>
	</div>
<div id="DivRight">
		<div id ="right_top" class="Div_heard">
		 畅销公开课
		    <div class="list" id="cxgkk">
		      		    <uc3:DGridSellWell id="SellWell" runat="server"></uc3:DGridSellWell>

		    </div>
	  	</div>
		<div id="right_tch" class="Div_heard">人气讲师
          <uc6:DataGrid id="DataGrid5" runat="server"></uc6:DataGrid> 
		</div>
		<div id="right_org" class="Div_heard">培训机构
        <div class="list">
		  <uc5:DGridOrgans id="DGridNewOrangs" runat="server"></uc5:DGridOrgans>            
          </div>
		</div>
	</div>
<%--</form>--%>

</asp:Content>

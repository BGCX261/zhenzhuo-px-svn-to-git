<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Curricula.aspx.cs" Inherits="Curricula" Title="公开课"%>
<%@ Register src=".\User controls\GridNew.ascx" tagname="DGridNew" tagprefix="uc4" %>
<%@ Register src=".\User controls\GridHots.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register src=".\User controls\GridGoldLeft.ascx" tagname="DGridGoldLeft" tagprefix="uc2" %>
<%@ Register src=".\User controls\GridField.ascx" tagname="DGridField" tagprefix="uc3" %>
<%@ Register src=".\User controls\ListField.ascx" tagname="DListField" tagprefix="uc5" %>
<%@ Register src=".\User controls\ListCategories.ascx" tagname="DListCategories" tagprefix="uc6" %>
<%@ Register src=".\User controls\ListDate.ascx" tagname="DListDate" tagprefix="uc8" %>
<%@ Register src=".\User controls\ListCity.ascx" tagname="DListCity" tagprefix="uc7" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

<link rel="stylesheet" type="text/css" href="tab/tabview.css" />
<link rel="stylesheet" type="text/css" href="tab/fonts-min.css" />
<script src="tab/dom-event.js"></script>
<script src="tab/element-min.js"></script>
<script src="tab/connection-min.js"></script>
<script src="tab/tabview-min.js"></script>
<script>
(function () {
            var tabView = new YAHOO.widget.TabView('test');
        })();
</script>
<script>
    (function () {
        var tabView = new YAHOO.widget.TabView('test1');
    })();
</script>
<script>
    (function () {
        var tabView = new YAHOO.widget.TabView('test2');
    })();
</script>
<div id="Training">
<div id="DivLeft">
  <div id="PicAD"><img src="images/pic21.gif" width="285" height="160" alt="" /></div>
  <div id = "left_gold_gkk" class="Div_heard">金牌公开课
    <uc2:DGridGoldLeft id="DGridGoldLeft" runat="server" pSource = "left"></uc2:DGridGoldLeft>
  </div>
  <div id="left_hot_gkk" class="Div_heard">热门公开课
    <uc1:DataGrid id="hots" runat="server" pSource = "left"></uc1:DataGrid>
  </div>

  <div id="left_sellwell_gkk" class="Div_heard">畅销公开课
    <div>
    </div>
  </div>
</div>

<div id="DivMiddle">
  <div id="Middle_New_gkk" class="Div_heard"><span class="STYLE6">最新公开课</span>			
		  <div>
		    <uc4:DGridNew id="DGridNew" runat="server"></uc4:DGridNew>            
	      </div>
  </div>
  <div  class="yui-skin-sam" style="text-align:left;" id="middle_rz">
    <div id="test" class="yui-navset" >
        <ul class="yui-nav" >
            <li class="selected"><a href="#tab6" style="line-height:100%"><em>生产管理</em></a></li>
            <li><a href="#tab7" style="line-height:100%"><em>客户管理</em></a></li>
            <li><a href="#tab8" style="line-height:100%"><em>财税管理</em></a></li>
            <li><a href="#tab9" style="line-height:100%"><em>人力资源</em></a></li>
            <li><a href="#tab10" style="line-height:100%"><em>项目管理</em></a></li>
        </ul>
        <div class="yui-content" style="background-color:White; border-right: 1px solid #C5DDF1; border-left: 1px solid #C5DDF1;border-bottom: 1px solid #C5DDF1;">
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField1" runat="server" FieldID="45ccb46e-19ad-469d-a32f-19b71fa87584"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField2" runat="server" FieldID="10bf3942-5b85-46b0-8523-2964981b5cb0"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField3" runat="server" FieldID="235914cd-4da4-4087-a988-2cd88c450db6"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField4" runat="server" FieldID="ede6aeee-9dbb-40b1-b68a-304c63618a99"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField5" runat="server" FieldID="549c969c-e306-4363-9d15-6776572c9340"></uc3:DGridField>
            </div>
        </div>      
    </div>
  </div>
  
  <div  class="yui-skin-sam" style="text-align:left;" id="middle_rz">
    <div id="test1" class="yui-navset" >
        <ul class="yui-nav" >
            <li class="selected"><a href="#tab6" style="line-height:100%"><em>团队管理</em></a></li>
            <li><a href="#tab7" style="line-height:100%"><em>经营管理</em></a></li>
            <li><a href="#tab8" style="line-height:100%"><em>采购管理</em></a></li>
            <li><a href="#tab9" style="line-height:100%"><em>个人技能</em></a></li>
            <li><a href="#tab10" style="line-height:100%"><em>品牌管理</em></a></li>
        </ul>
        <div class="yui-content" style="background-color:White; border-right: 1px solid #C5DDF1; border-left: 1px solid #C5DDF1;border-bottom: 1px solid #C5DDF1;">
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField6" runat="server" FieldID="fe59cb38-77b4-4dc6-9744-68014ecd13dd"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField7" runat="server" FieldID="72e78810-a679-49ef-a0e8-72ee2a1fd6e5"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField8" runat="server" FieldID="c6b72d17-29fc-4011-ac4b-8cf5d411631b"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField9" runat="server" FieldID="afead1ba-ecfb-42c3-a463-c7b202a34bba"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField10" runat="server" FieldID="df828b84-3c40-466f-a123-d0bd087e22c9"></uc3:DGridField>
            </div>
        </div>      
    </div>
  </div>

  <div  class="yui-skin-sam" style="text-align:left;" id="middle_rz">
    <div id="test2" class="yui-navset" >
        <ul class="yui-nav" >
            <li class="selected"><a href="#tab1" style="line-height:100%"><em>市场营销</em></a></li>
            <li><a href="#tab2" style="line-height:100%"><em>仓储物流</em></a></li>
            <li><a href="#tab3" style="line-height:100%"><em>质量管理</em></a></li>
            <li><a href="#tab4" style="line-height:100%"><em>企业战略</em></a></li>
        </ul>
        <div class="yui-content" style="background-color:White; border-right: 1px solid #C5DDF1; border-left: 1px solid #C5DDF1;border-bottom: 1px solid #C5DDF1;">
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField11" runat="server" FieldID="83c55398-e1af-4bbb-b249-de70323dc5b3"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField12" runat="server" FieldID="dc801d1a-1b80-4ee5-84d3-f3a8cfb85430"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField13" runat="server" FieldID="cf2dd359-d0d3-4f51-a7b9-fa1770f36272"></uc3:DGridField>
            </div>
            <div class ="Middle_ul_gkk">
                <uc3:DGridField id="DGridField14" runat="server" FieldID="1dd605da-e80e-443e-a0b6-fbd8218e9efb"></uc3:DGridField>
            </div>
        </div>      
    </div>
  </div>

</div>

<div id="DivRight">
  <div id="right_ly_ggk" class="Div_heard">热门培训专题
    <div>
        <uc5:DListField id="DListField" runat="server"></uc5:DListField>            
    </div>
  </div>
  <div id="right_pic_ggk"><img name="" src="images/pic22.jpg" width="250" height="200" alt="" /></div>
  <div id="right_date_ggk" class="Div_heard">培训日历
    <uc8:DListDate id="DListDate" runat="server"></uc8:DListDate>
  </div>
  <div id="right_dq_ggk" class="Div_heard">地区公开课
    <uc7:DListCity id="DListCity" runat="server"></uc7:DListCity>
  </div>
  <div id="right_pic2_ggk"><img name="" src="images/pic20.jpg" width="250" height="55" alt="" /></div>
  <div id="right_hy_ggk" class="Div_heard">行业体系
    <div>
        <uc6:DListCategories id="DListCategories" runat="server"></uc6:DListCategories>            
    </div>
  </div>
  <div id="right_pic1_ggk"><img name="" src="images/pic20.jpg" width="250" height="165" alt="" /></div>
</div>
</div>
<div style="clear:both;"></div>
</asp:Content>

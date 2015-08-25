<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regist.aspx.cs" Inherits="regist" %>
<%@ Import Namespace="TrainEngine.Core" %>
<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">

<div class="divReg">

<ul >

<li class="li1">培训学员注册</li>

<li class="li2">若您是学员，您可以</li>

<li class="li3">采购课程、积分兑奖......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>reg/regstd.aspx' title="学员免费注册"><img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/btn1.gif' /></a></li>

</ul>



<ul >

<li class="li1">培训机构注册</li>

<li class="li2">若您是机构，您可以</li>

<li class="li3">发布课程、宣传公司......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>reg/regorg.aspx' title="机构免费注册"><img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/btn1.gif' /></a></li>

</ul>



<ul >

<li class="li1">培训讲师注册</li>

<li class="li2">若您是讲师，您可以</li>

<li class="li3">展示履历、上传资料......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>reg/regtch.aspx' title="讲师免费注册"><img src='<%=Utils.AbsoluteWebRoot %>themes/Defualt/pic/btn1.gif' /></a></li>

</ul>





</div>

<div style="clear:both;"></div>

</asp:content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regok.aspx.cs" Inherits="reg_regok" %>
<%@ Import Namespace="TrainEngine.Core" %>
<asp:content id="Content1" contentplaceholderid="cphBody" runat="Server">

<div class="divReg" style="text-align:center;">

<%if(Request["uType"]=="std") {%>
<ul style="float:none;">

<li class="li1">培训学员注册成功!</li>

<li class="li2">自动通过审核</li>

<li class="li3">您可以采购课程、积分兑奖......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>' title="回到首页">回到首页</a></li>

</ul>

<%} 
if(Request["uType"]=="org") {
%>

<ul style="float:none;">

<li class="li1">培训机构注册成功!</li>

<li class="li2">请等待管理员审核</li>

<li class="li3">通过审核后,您可以发布课程、宣传公司......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>' title="回到首页">回到首页</a></li>

</ul>

<%} 
if(Request["uType"]=="tch") {
%>

<ul style="float:none;">

<li class="li1">培训讲师注册</li>

<li class="li2">请等待管理员审核</li>

<li class="li3">通过审核后,您可以展示履历、上传资料......</li>

<li class="li4"><a href='<%=Utils.AbsoluteWebRoot %>' title="回到首页">回到首页</a></li>

</ul>

<%} %>



</div>

<div style="clear:both;"></div>
</asp:content>
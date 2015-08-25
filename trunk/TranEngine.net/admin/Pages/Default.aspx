<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Pages_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
    <div class="content-box-outer">
		<div class="content-widgets-box-full">
            <div id="stats" style="width:20%;">
                <div class="dashboardStats">
                    <div class="rounded">
                        <h2>综合信息</h2>
                        <ul id="ulInfoList" runat="server">
                            <%if (Page.User.IsInRole("administrators") || Page.User.IsInRole("organs"))
                              {%>
                            <li>
                                <%=CurriculaCount%> 公开课<a class="viewAction endline" href="./Curricula/default.aspx">查看</a><br />
                                <%=NoCurriculaCount%> 未审核
                            </li>
                            <%} %>
                            <%if (Page.User.IsInRole("administrators") || Page.User.IsInRole("organs") || Page.User.IsInRole("teachers"))
                             {%>
                                <li>
                                    <%=TrainingCount%> 内训课程<a class="viewAction endline" href="./Training/default.aspx">查看</a><br />
                                    <%=NoTrainingCount%> 未审核
                                </li>
                                <li>
                                    <%=ExcellentCount%> 培训现场 <a class="viewAction endline" href="./Excellent/default.aspx">查看</a><br />
                                    <%=NoExcellentCount%> 未审核
                                </li>
                            <%} %>
                            <li>
                                <%=ResCount%> 上传资料<a class="viewAction endline" href="./ResUpload/default.aspx">查看</a><br />
                                <%=NoResCount%> 未审核
                            </li>
                            <%if (Page.User.IsInRole("administrators"))
                              {%>
                            <li>
                                <%=NoUserCount%> 未审核用户<a class="viewAction endline" href="./Profiles.aspx?p=no">查看</a><br />
                            </li>
                            <%} %>
                            
                        </ul>
                    </div>
                </div>
            </div>
            <div id="widgets" style="width:80%;">
                <div class="dashboardWidget">
                    <div class="rounded">
                        <h2>欢迎您访问 </h2>
                        <asp:Literal ID="ltWellcom" runat="server"></asp:Literal>
                        <%--<font color='green'>ls</font>,欢迎您回来!　您现有积分:<font color='#3265C3'>15</font>个　会员类别:<font color='green'>培训讲师</font>　会员级别:<font color='green'>普通会员</font>　帐户状态:<font color='red'>审核不通过,请联系管理员</font>--%>
                    </div>
                </div>
                <div class="dashboardWidget rounded">
                    <div class="rounded">
                    <h2>欢迎您回来，您可以立即获得以下服务：</h2>
                    <asp:Literal ID="ltServices" runat="server"></asp:Literal>
                      <%--<ul id="ulServices" runat="server">
	                    <li>1．维护您的个人简介；</li>
	                    <li>2．免费发布课程</li>
                        <li>3．上传培训现场图片</li>
	                    <li>4．上传您的管理培训资源</li>
	                    <li>5．获取下载积分下载更多管理培训资源</li>
	                    <li>7．申请VIP尊贵会员,享受品牌专业推广和更多授课机会</li>
	                    <li>9．您所注册的个人资料和发布的信息要求填写合法、完整、真实、有效。否则将删除！谢谢</li>
	                    <li>管理员QQ: 123455678  </li>
	                    <li>管理员MSN: xxxxxxx@163.com　　xxxxxxx@hotmail.com... </li>
	                    <li>合作专线:020-61968122-606  xxx先生</li>
	                    <li>如果您对合作推广细节有疑问，请与我们联系:xxxxxxxx</li>
                       </ul>--%>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
</asp:Content>


<%@ Page Title="公开课管理" Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs"
    Inherits="admin_Pages_Curricula" ValidateRequest="False" %>

<%@ Register Src="../../htmlEditor.ascx" TagPrefix="Blog" TagName="TextEditor" %>
<%@ Import Namespace="Resources" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%=labels.widget %>
        <%=labels.editor %></title>
    <style type="text/css">
        body
        {
            font: 12px verdana;
            margin: 0;
            overflow: hidden;
        }
        #title
        {
            background: #F1F1F1;
            border-bottom: 1px solid silver;
            padding: 10px;
            font-weight: bold;
        }
        label
        {
            margin: 2px;
        }
        .field
        {
            padding: 2px;
        }
        #phEdit
        {
            padding: 10px;
            height: 450px;
            overflow: auto;
            overflow-x: hidden;
        }
        #bottom
        {
            background: #F1F1F1;
            border-top: 1px solid silver;
            padding: 10px;
            text-align: center;
        }
        a
        {
            text-decoration: none;
            color: #5C80B1;
        }
        
        a:hover
        {
            text-decoration: underline;
        }
    </style>
    <link rel="stylesheet" href="../../style.css" type="text/css" />
</head>
<body onkeypress="ESCclose(event)" scroll="no">
    <script type="text/javascript">
        function checkTextContent(ClientID) {
            if (document.getElementById(ClientID).value == '') { Alert('请输入课程介绍!'); return false; }
        }
        function ESCclose(evt) {
            if (!evt) evt = window.event;

            if (evt && evt.keyCode == 27)
                window.parent.closeEditor();
        }

        document.body.onkeypress = ESCclose;

        function ESCclose(evt) {
            if (!evt)
                evt = window.event;

            if (evt.keyCode == 27)
                document.getElementById('tagselector').style.display = 'none';
        }

        function AddTag(element) {
            var input = document.getElementById('<%=txtTags.ClientID %>');
            input.value += element.innerHTML + ', ';
        }
        function ToggleTagSelector() {
            var element = document.getElementById('tagselector');
            if (element.style.display == "none")
                element.style.display = "block";
            else
                element.style.display = "none";
        }
        
    </script>
    <form id="form1" runat="server">
    <div id="title">
        <div style="font-size: 14px">
            <% if (!IsActionNew)
               { %>
            修改课程:
            <%=CurrentCurricula.Title%>
            <span style="padding-top: 5px; font-weight: normal;">
                <%=labels.author %>:
                <%=CurrentCurricula.Author%>
            </span>
            <%}
               else
               {%>
            新增课程
            <% }%>
        </div>
    </div>
    <div runat="server" id="phEdit">
        <div class="field">
            <label for="<%=txtTitle.ClientID %>">
                课程标题</label>
            <asp:TextBox runat="server" ID="txtTitle" Width="400px" />&nbsp;<asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                ErrorMessage="请输入课程标题" />
        </div>
        <div class="field">
            <label for="<%=txtObj.ClientID %>">
                招生对象</label>
            <asp:TextBox runat="server" ID="txtObj" Width="400px" />
            <label for="<%=txtPoints.ClientID %>">
                积分</label>
            <asp:TextBox ID="txtPoints" runat="server" Width="45px"></asp:TextBox>
            &nbsp;
            <label for="<%=txtScores.ClientID %>">
                培训币</label>
            <asp:TextBox ID="txtScores" runat="server" Width="45px"></asp:TextBox>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPoints"
                Display="Dynamic" ErrorMessage="请输入积分" SetFocusOnError="True"></asp:RequiredFieldValidator>
            <a id="aPages0" runat="server" href="javascript:void(ToggleVisibility());" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtScores"
                Display="Dynamic" ErrorMessage="请输入培训币" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="field">
            <label for="<%=txtContent.ClientID %>">
                课程介绍
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtObj"
                Display="Dynamic" ErrorMessage="请输入招生对象" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </label>
            <br />
            <Blog:TextEditor runat="server" ID="txtContent" TabIndex="4" />
            <%--<asp:TextBox runat="server" ID="txtContent" TabIndex="4" Height="200px" 
          TextMode="MultiLine" Width="100%"></asp:TextBox>--%>
        </div>
        <table id="entrySettings" Width="100%">
            <tr>
                <td class="label">
                    <%=Resources.labels.keywords %>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtTags" Width="400" />
                    <a href="javascript:void(ToggleTagSelector())">选择关键字</a> <span>请用逗号(,)分割</span>
                </td>
            </tr>
            <tr>
                <td class="label">
                    分类
                </td>
                <td>
                        <asp:CheckBoxList runat="server" ID="cblCategories" CssClass="cblCategories"
                            RepeatLayout="Flow" RepeatDirection="Horizontal" Width="800px" />
                    
                </td>
            </tr>
            <tr >
                <td class="label">
                    领域
                </td>
                <td style="border-color: #C0C0C0; border-top-width: 1px; border-top-style: solid">
                        <asp:CheckBoxList runat="server" ID="cblField" CssClass="cblCategories"
                            RepeatLayout="Flow" EnableTheming="True" 
                            RepeatDirection="Horizontal" Width="800px" />
                    
                </td>
            </tr>
        </table>
    </div>
    <div id="bottom">
        <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" />
        <input onclick="parent.closeEditor()" type="button" value="<%=labels.cancel %>" /></div>
    <div id="tagselector" style="display: none;">
        <div style="color: Black; float: left; padding-left: 10;">
            <label style="margin-left: 10;">
                常用关键字</label></div>
        <a href="javascript:void(ToggleTagSelector())" style="color: Black; float: right">Close</a>
        <div style="clear: both">
        </div>
        <asp:PlaceHolder runat="server" ID="phTags" />
        <div style="clear: both">
        </div>
    </div>
    </form>
</body>
</html>

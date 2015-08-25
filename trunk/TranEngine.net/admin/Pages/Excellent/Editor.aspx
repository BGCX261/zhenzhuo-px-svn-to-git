<%@ Page Title="公开课管理" Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs"
    Inherits="admin_Pages_Excellent" ValidateRequest="False" %>

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
            height: 355px;
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
        function ESCclose(evt) {
            if (!evt) evt = window.event;

            if (evt && evt.keyCode == 27)
                window.parent.closeEditor();
        }

        document.body.onkeypress = ESCclose;
        function LookUpStock() {
            CallServer("checkDelete", "");
        }

        function ReceiveServerData(rValue) {
            if (rValue == 'false') {
                alert('最后一条图片展示数据,不能删除!');
                return false;
            }
        }

        function CloseEditer() {
            CallServer("closeEditor", "");
            parent.closeEditor();
        }
    </script>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="title">
        <div style="font-size: 14px">
            <% if (!IsActionNew)
               { %>修改培训现场:
            <%=CurrentExcellent.Title%>
            <%}
               else
               {%>新增培训现场
            <% }%>
        </div>
    </div>
    <div runat="server" id="phEdit">
        <div class="field">
            <label for="<%=txtTitle.ClientID %>">
                内训标题</label>
            <asp:TextBox runat="server" ID="txtTitle" Width="400px" />&nbsp;<asp:RequiredFieldValidator
                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                ErrorMessage="请输入课程标题" ValidationGroup="edit" />
        </div>
        <div class="field">
            <label for="<%=ddlObj.ClientID %>">
                授课地点</label>
            <asp:TextBox ID="txtCityTown" runat="server" Width="100px"></asp:TextBox>
            &nbsp;<label for="<%=ddlObj.ClientID %>">授课教师</label>
            <asp:DropDownList ID="ddlObj" runat="server" Height="22px" Width="100px" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCityTown"
                Display="Dynamic" ErrorMessage="请输入授课地点" SetFocusOnError="True" ValidationGroup="edit"></asp:RequiredFieldValidator>
            <br />
            <label for="<%=ddlObj.ClientID %>">
                培训日期</label>
            <asp:TextBox ID="txtTrainingDate" runat="server" Width="100px"></asp:TextBox>
            (格式:yyyy-MM-dd)<br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTrainingDate"
                Display="Dynamic" ErrorMessage="请输入培训日期" SetFocusOnError="True" ValidationGroup="edit"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                ControlToValidate="txtTrainingDate" Display="Dynamic" ErrorMessage="请按格式输入日期"
                ValidationExpression="\d{4}-\d{1,2}-\d{1,2}" ValidationGroup="edit"></asp:RegularExpressionValidator>
        </div>
        <div class="field">
            <label for="<%=FileUpload.ClientID %>">
                选择图片
            </label>
            <asp:FileUpload ID="FileUpload" runat="server" Width="340px" />
            &nbsp;<asp:Button ID="btnAdd" runat="server" Text="添加" Width="50px" OnClick="btnAdd_Click"
                ValidationGroup="addFile" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileUpload"
                Display="Dynamic" ErrorMessage="请选择图片文件" ValidationGroup="addFile" />
        </div>
        <table id="entrySettings">
            <tr>
                <td class="label" colspan="2">
                    <asp:Label ID="lblMess" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridComments" BorderColor="#F8F8F8" BorderStyle="Solid" BorderWidth="1px"
                        RowStyle-BorderWidth="0" RowStyle-BorderStyle="None" GridLines="None" DataKeyNames="Id"
                        runat="server" Width="580px" AlternatingRowStyle-BackColor="#f8f8f8" AlternatingRowStyle-BorderColor="#f8f8f8"
                        HeaderStyle-BackColor="#F1F1F1" CellPadding="3" AutoGenerateColumns="False" ShowFooter="True"
                        AllowSorting="True" OnRowDataBound="gridComments_RowDataBound" BackColor="White"
                        EnableModelValidation="True" OnRowDeleting="gridComments_RowDeleting" OnRowCreated="gridComments_RowCreated">
                        <AlternatingRowStyle BackColor="#F8F8F8" BorderColor="#F8F8F8"></AlternatingRowStyle>
                        <Columns>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnD" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="删除" OnClientClick="LookUpStock()"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="文件名称" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "FileName").ToString()%>'
                                        OnClientClick='<%#GetEditHtml(DataBinder.Eval(Container.DataItem, "FileName").ToString(),"showRes.aspx",1000, 580)%>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="300px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Literal ID="ltAuthor" Text='<%#DataBinder.Eval(Container.DataItem, "ResType") + "" %>'
                                        runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="180px"></HeaderStyle>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="RadioButtonMarkup" runat="server"></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#F1F1F1"></HeaderStyle>
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="20" />
                        <PagerStyle HorizontalAlign="Center" />
                        <RowStyle BorderWidth="0px" BorderStyle="None"></RowStyle>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <div id="bottom">
        <asp:Button runat="server" ID="btnSave" Text="保存" OnClick="btnSave_Click" ValidationGroup="edit" />
        <%--<asp:Button runat="server" ID="btnCancel" Text="取消" OnClick="btnCancel_Click" />--%>
        <input onclick="CloseEditer()" type="button" value="<%=labels.cancel %>" />
    </div>
    </form>
</body>
</html>

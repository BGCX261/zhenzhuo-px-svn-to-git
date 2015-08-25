<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataGrid.ascx.cs" Inherits="admin_Pages_ResUpload_DataGrid" %>
<%@ Import Namespace="Resources" %>
<%@ Import Namespace="TrainEngine.Core" %>
<script language="javascript">
    function editComment(id, page, w, h) {
        window.scrollTo(0, 0);
        var width = document.documentElement.clientWidth + document.documentElement.scrollLeft;
        var height = document.documentElement.clientHeight + document.documentElement.scrollTop;
        var layer = document.createElement('div');

        layer.style.zIndex = 2;
        layer.id = 'layer';
        layer.style.position = 'absolute';
        layer.style.top = '0px';
        layer.style.left = '0px';
        layer.style.height = document.documentElement.scrollHeight + 'px';
        layer.style.width = width + 'px';
        layer.style.backgroundColor = 'black';
        layer.style.opacity = '.6';
        layer.style.filter += ("progid:DXImageTransform.Microsoft.Alpha(opacity=60)");
        document.body.style.position = 'static';
        document.body.appendChild(layer);

        var size = { 'height': h, 'width': w };
        var iframe = document.createElement('iframe');

        iframe.name = 'Comment Editor';
        iframe.id = 'CommentEditor';
        //        iframe.src = 'Editor.aspx?id=' + id;
        iframe.src = page + '?id=' + id;
        iframe.style.height = size.height + 'px';
        iframe.style.width = size.width + 'px';
        iframe.style.position = 'fixed';
        iframe.style.zIndex = 3;
        iframe.style.backgroundColor = 'white';
        iframe.style.border = '2px solid silver';
        iframe.frameborder = '0';

        iframe.style.top = ((height + document.documentElement.scrollTop) / 2) - (size.height / 2) + 'px';
        iframe.style.left = (width / 2) - (size.width / 2) + 'px';

        document.body.appendChild(iframe);
    }

    function closeEditor(reload) {
        if (reload) {
            location.href = location.href;
        }
        else {
            var v = document.getElementById('CommentEditor');
            var l = document.getElementById('layer');
            document.body.removeChild(document.getElementById('CommentEditor'));
            document.body.removeChild(document.getElementById('layer'));
            document.body.style.position = '';

        }
    }
    function ToggleVisibility(id) {
        var element = document.getElementById(id);
        if (element.style.display == "none")
            element.style.display = "block";
        else
            element.style.display = "none";
    }
    function ConfirmDelete() {

        return confirm('<%=AreYouSureDelete()%>');
    }
</script>
<style type="text/css">
        
        .field
        {
            padding: 2px;
        }
        #phEdit
        {
            padding: 10px;
            
            overflow: auto;
            overflow-x: hidden;
        }
        
    </style>
<br />
<div runat="server" id="phEdit">
    <div class="field">
        <span>
            资料简介
        </span>
        <asp:TextBox ID="txtDesription" runat="server" Width="410px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesription"
            Display="Dynamic" ErrorMessage="请输入资料简介" ValidationGroup="addFile" />
    </div>
    <div class="field">
        <span>
            下载积分
        </span>
        <asp:TextBox ID="txtPoints" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPoints"
            Display="Dynamic" ErrorMessage="请输入资料下载所需积分" ValidationGroup="addFile" 
            SetFocusOnError="True" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtPoints" ErrorMessage="积分应为正整数" SetFocusOnError="True" 
            ValidationExpression="\d{1,}" ValidationGroup="addFile"></asp:RegularExpressionValidator>
    </div>
    <div class="field">
        <span >
            选择文件
        </span>
        <asp:FileUpload ID="FileUpload" runat="server" Width="355px" />
        &nbsp;<asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" ValidationGroup="addFile" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileUpload"
            Display="Dynamic" ErrorMessage="请选择图片文件" ValidationGroup="addFile" />
        <br />
        <asp:Label ID="lblMess" runat="server" ForeColor="Red" Visible="False"></asp:Label>
    </div>
    <hr />
    <div style="border: 1px solid #f3f3f3;">
        <asp:GridView ID="gridComments" BorderColor="#F8F8F8" BorderStyle="Solid" BorderWidth="1px"
            RowStyle-BorderWidth="0" RowStyle-BorderStyle="None" GridLines="None" DataKeyNames="Id"
            runat="server" Width="100%" AlternatingRowStyle-BackColor="#f8f8f8" AlternatingRowStyle-BorderColor="#f8f8f8"
            HeaderStyle-BackColor="#F1F1F1" CellPadding="3" AutoGenerateColumns="False" AllowPaging="True"
            OnPageIndexChanging="gridView_PageIndexChanging" ShowFooter="True" AllowSorting="True"
            OnRowDataBound="gridComments_RowDataBound" BackColor="White" EnableModelValidation="True">
            <AlternatingRowStyle BackColor="#F8F8F8" BorderColor="#F8F8F8"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" />
                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" Enabled='<%#HasNoChildrenAndApped((Guid)DataBinder.Eval(Container.DataItem, "Id"))%>'
                            runat="server" ToolTip='<%#HasNoChildrenAndApped((Guid)DataBinder.Eval(Container.DataItem, "Id"))?"未发布":""%>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文件名" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href="<%#Utils.AbsoluteWebRoot%>GetRes.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Id") %>" target="_blank"><%#DataBinder.Eval(Container.DataItem, "FileName").ToString()%></a>
                        <%--<asp:LinkButton ID="lnk" runat="server" Text='' PostBackUrl="~/GetRes.aspx" CommandName="id" CommandArgument='' />--%>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="简介" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Literal ID="ltTeacher" Text='<%#DataBinder.Eval(Container.DataItem, "Description") + "" %>'
                            runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="80px"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="下载积分" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Literal ID="ltType" Text='<%#DataBinder.Eval(Container.DataItem, "Points") + "" %>'
                            runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="180px"></HeaderStyle>
                    <ItemStyle Wrap="False" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="上传人" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Literal ID="ltAuthor" Text='<%#DataBinder.Eval(Container.DataItem, "Author") + "" %>'
                            runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="80px"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发布情况" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="app" runat="server" Text='<%# GetAppInfo((Guid)DataBinder.Eval(Container.DataItem, "Id")) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#F1F1F1"></HeaderStyle>
            <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="20" />
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle BorderWidth="0px" BorderStyle="None"></RowStyle>
        </asp:GridView>
    </div>
</div>
<div style="text-align: center; padding-top: 10px">
    <asp:Button ID="btnSelect" runat="server" Text="全选" OnClick="btnSelect_Click" />
    <asp:Button ID="btnClear" runat="server" Text="清除全选" OnClick="btnClear_Click" />
    <asp:Button ID="btnAction" runat="server" Text="<%$ Resources:labels, approve %>"
        OnClick="btnAction_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>"
        OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete();" />
</div>
<div id="ErrorMsg" runat="server" style="color: Red; display: block;">
</div>
<div id="InfoMsg" runat="server" style="color: Green; display: block;">
</div>

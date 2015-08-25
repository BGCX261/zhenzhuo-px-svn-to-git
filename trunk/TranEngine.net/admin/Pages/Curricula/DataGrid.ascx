<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DataGrid.ascx.cs" Inherits="admin_Pages_Curricula_DataGrid" %>
<%@ Import Namespace="Resources" %>
<script language="javascript">
    function editComment(id,page,w,h) {
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
//    function GetEditor(flag) {
//        k;
//        WebForm_DoCallback('__Page', flag, ApplyEditor, 'Editor', null, false);

//    }
//    function ApplyEditor(arg, context) {
//        closeEditor(arg)
//    }
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
                    <asp:CheckBox ID="chkSelect" 
                        runat="server"  /><%--Enabled='<%#HasNoChildrenAndApped((Guid)DataBinder.Eval(Container.DataItem, "Id"))%>'--%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="标题" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'
                        OnClientClick='<%#GetEditHtml(DataBinder.Eval(Container.DataItem, "Id").ToString(),"Editor.aspx",1000, 560)%>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作者" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Literal ID="ltModerator" Text='<%#DataBinder.Eval(Container.DataItem, "Author") + "" %>'
                        runat="server" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="80px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程安排">
            <ItemTemplate>
                  <div id="divPages" runat="server" visible="true" enableviewstate="False" style="margin-bottom: 10px">
                    <a id="aPages" runat="server" href="javascript:void(ToggleVisibility());" />　<a id="aNew" runat="server" href="#">新建</a>
                    <ul id="ulPages" runat="server" style="display:none;list-style-type:circle" />
                  </div>
            </ItemTemplate>
                <HeaderStyle Width="350px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="app" runat="server" 
                        Text='<%# GetAppInfo((Guid)DataBinder.Eval(Container.DataItem, "Id")) %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="编辑" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEditComment" runat="server" Text='编辑'　
                    Visible='<%#HasNoChildrenAndApped((Guid)DataBinder.Eval(Container.DataItem, "Id"))%>'
                        OnClientClick='<%#GetEditHtml(DataBinder.Eval(Container.DataItem, "Id").ToString(),"Editor.aspx",1000, 560)%>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="50px"></HeaderStyle>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#F1F1F1"></HeaderStyle>
        <PagerSettings Mode="NumericFirstLast" Position="Bottom" PageButtonCount="20" />
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle BorderWidth="0px" BorderStyle="None"></RowStyle>
    </asp:GridView>
</div>
<div style="text-align: center; padding-top: 10px">
    <asp:Button ID="btnSelect" runat="server" Text="全选" OnClick="btnSelect_Click" />
    <asp:Button ID="btnClear" runat="server" Text="清除全选" OnClick="btnClear_Click" />
    <asp:Button ID="btnAction" runat="server" Text="<%$ Resources:labels, approve %>"
        OnClick="btnAction_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:labels, delete %>"
        OnClick="btnDelete_Click" OnClientClick="return ConfirmDelete();" />
        <asp:Button ID="btnSetGold" runat="server" Text="金牌课程设置" onclick="btnSetGold_Click"
         />
</div>
<div id="ErrorMsg" runat="server" style="color: Red; display: block;">
</div>
<div id="InfoMsg" runat="server" style="color: Green; display: block;">
</div>

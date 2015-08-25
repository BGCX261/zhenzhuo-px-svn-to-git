<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridResList.ascx.cs" Inherits="User_controls_DownLoad_GridResList" %>
<train:PageGridView ID="GridList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    GridLines="None" PageSize="15" Width="100%" ShowHeader="False" DataKeyNames="Id"
    OnRowDataBound="GridList_RowDataBound" OnPageIndexChanging="GridList_PageIndexChanging">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color: #FFFFFF;
                    border-width: 0px 5px 0px 5px; border-style: none solid none solid;">
                    <tr>
                        <td class="blklist" style="">
                            <h1>
                                <span>下载积分:<%#DataBinder.Eval(Container.DataItem, "Points").ToString()%>
                                    &nbsp;添加日期:<%#DataBinder.Eval(Container.DataItem, "DateCreated").ToString()%></span><asp:LinkButton
                                        ID="lbRes" runat="server" OnClick="lbRes_Click"></asp:LinkButton>
                            </h1>
                        </td>
                    </tr>
                    <tr>
                        <td class="blklist">
                            <asp:Label ID="ltDes" runat="server"></asp:Label>
                            <div class='hr'>
                            </div>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="true" />
        </asp:TemplateField>
    </Columns>
</train:PageGridView>

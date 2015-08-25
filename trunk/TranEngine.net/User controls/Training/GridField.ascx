<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridField.ascx.cs" Inherits="User_controls_Training_GridField" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <asp:GridView ID="GridFields" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridFields_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            ·<a id="aPages" runat="server" href="Javascript:void(0)"><%#jup(DataBinder.Eval(Container.DataItem, "FieldName").ToString())%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbNum" runat="server" Text='00' ForeColor="#FF6600"></asp:Label><span>
                                课程&nbsp;</span>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
        <td>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridFields_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            ·<a id="aPages" runat="server" href="Javascript:void(0)"><%#jup(DataBinder.Eval(Container.DataItem, "FieldName").ToString())%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbNum" runat="server" Text='00' ForeColor="#FF6600"></asp:Label><span>
                                课程&nbsp;</span>
                        </ItemTemplate>
                        <HeaderStyle Width="100px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

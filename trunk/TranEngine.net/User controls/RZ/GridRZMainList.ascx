<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridRZMainList.ascx.cs" Inherits="User_controls_RZ_GridRZMainList" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td width="50%" valign="top">
            <asp:GridView ID="GridList1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%" 
                OnRowDataBound="GridList_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            ·<a id="aPages" runat="server" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>' href="Javascript:void(0)"><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </td>
        <td  width="50%" valign="top">
            <asp:GridView ID="GridList2" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%" 
                OnRowDataBound="GridList_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            ·<a id="aPages" runat="server" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>' href="Javascript:void(0)"><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
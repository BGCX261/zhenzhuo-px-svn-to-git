<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridSellWell.ascx.cs" Inherits="User_controls_GridSellWell" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="100%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Width="80%" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <label><%#DataBinder.Eval(Container.DataItem, "Author").ToString()%></label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>





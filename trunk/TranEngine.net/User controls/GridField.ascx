<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridField.ascx.cs" Inherits="User_controls_GridField" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="100%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:BoundField DataField="Id" Visible="False" />
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Width="75%" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" Width="10%" Wrap="False" />
        </asp:TemplateField>        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblCityTown" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Width="5%" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lbCast" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
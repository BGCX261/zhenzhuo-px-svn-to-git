<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GrideResNews.ascx.cs"
    Inherits="User_controls_DownLoad_GrideResNews" %>
<asp:GridView ID="GridList" runat="server" AutoGenerateColumns="False"
    GridLines="None" PageSize="3" Width="100%" ShowHeader="False" 
    DataKeyNames="Id" onrowdatabound="GridFields_RowDataBound" 
     >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                ·<asp:LinkButton ID="lbRes" runat="server" OnClick="lbRes_Click"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="true" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>

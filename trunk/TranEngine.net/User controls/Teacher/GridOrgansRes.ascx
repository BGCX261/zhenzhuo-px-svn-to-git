<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridOrgansRes.ascx.cs" Inherits="User_controls_Teacher_GridOrgansRes" %>
<%@ Import Namespace="TrainEngine.Core" %>
<asp:GridView ID="GridFields" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
    GridLines="None" ShowHeader="False" Width="100%" DataKeyNames="Id" 
    onrowdatabound="GridFields_RowDataBound" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                ·<asp:LinkButton ID="lbRes" runat="server" onclick="lbRes_Click" ></asp:LinkButton>
            </ItemTemplate>
            
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle Wrap="False" />
</asp:GridView>

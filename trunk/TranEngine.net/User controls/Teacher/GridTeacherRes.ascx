<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridTeacherRes.ascx.cs"
    Inherits="User_controls_Teacher_GridTeacherRes" %>
<%@ Import Namespace="TrainEngine.Core" %>
<asp:GridView ID="GridFields" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
    GridLines="None" ShowHeader="False" Width="100%" DataKeyNames="Id" OnRowDataBound="GridFields_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                ·<asp:LinkButton ID="lbRes" runat="server" OnClick="lbRes_Click"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle Wrap="False" />
</asp:GridView>

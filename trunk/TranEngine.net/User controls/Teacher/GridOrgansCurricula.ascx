<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridOrgansCurricula.ascx.cs" Inherits="User_controls_Teacher_GridOrgansCurricula" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="100%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
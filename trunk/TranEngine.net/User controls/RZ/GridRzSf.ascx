<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridRzSf.ascx.cs" Inherits="User_controls_RZ_GridRzSf" %>
<%@ Import Namespace="TrainEngine.Core" %>
<asp:GridView ID="GridList" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
    GridLines="None" ShowHeader="False" Width="100%"  OnRowDataBound="GridFields_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                ·<a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle Wrap="False" />
</asp:GridView>
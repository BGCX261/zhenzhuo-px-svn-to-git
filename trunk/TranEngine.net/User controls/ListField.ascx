<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListField.ascx.cs" Inherits="User_controls_ListField" %>
<link rel="stylesheet" href="../themes/Defualt/Style.css" type="text/css" />

<div class="DivControlsList3C">
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="33%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "FieldName").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
</div>

<div class="DivControlsList3C">
<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="33%" onrowdatabound="GridView2_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages1" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "FieldName").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
</div>

<div class="DivControlsList3C">
<asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="33%" onrowdatabound="GridView3_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                    ·<a id="aPages2" runat="server" href="Javascript:void(0)" ><%#jup(DataBinder.Eval(Container.DataItem, "FieldName").ToString())%></a>
            </ItemTemplate>
            <ItemStyle Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
</div>
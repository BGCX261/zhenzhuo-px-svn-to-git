<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridNew.ascx.cs" Inherits="User_controls_GridNew" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" GridLines="None" 
    Width="100%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="14pt" Font-Strikeout="False" >
    <Columns>
        <asp:BoundField DataField="Id" Visible="False" />
        <asp:TemplateField>
            <ItemTemplate>
                    <a id="aPages" runat="server" href="Javascript:void(0)" ><span class="NewA"><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></span></a>
            </ItemTemplate>
            <ItemStyle Width="100%" Wrap="False" />
        </asp:TemplateField>        
    </Columns>
</asp:GridView>
<asp:Label ID="lblNew" runat="server" Text="" Font-Size="9pt" 
    ForeColor="Red"><%=strNew %></asp:Label>

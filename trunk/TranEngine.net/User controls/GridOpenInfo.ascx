<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridOpenInfo.ascx.cs" Inherits="User_controls_GridOpenInfo" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" GridLines="None" 
    Width="100%" onrowdatabound="GridView1_RowDataBound" Font-Names="宋体" 
    Font-Size="9pt" >
    <Columns>
        <asp:BoundField DataField="Id" Visible="False" />
        <asp:TemplateField HeaderText="开课日期">
            <ItemTemplate>
                <asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>      
        <asp:TemplateField HeaderText="培训天数">
            <ItemTemplate>
                <asp:Label ID="lblTotalDays" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>     
        <asp:TemplateField HeaderText="上课地区">
            <ItemTemplate>
                <asp:Label ID="lblCityTown" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="培训费用">
            <ItemTemplate>
                <asp:Label ID="lbCast" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="当前状态">
            <ItemTemplate>
                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="获取积分">
            <ItemTemplate>
                <asp:Label ID="lblPoint" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="获取培训币">
            <ItemTemplate>
                <asp:Label ID="lblScore" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <RowStyle HorizontalAlign="Left" />
</asp:GridView>
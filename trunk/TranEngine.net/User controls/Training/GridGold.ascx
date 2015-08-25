<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridGold.ascx.cs" Inherits="User_controls_Training_GridGold" %>
<asp:GridView ID="GridFields" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%" OnRowDataBound="GridFields_RowDataBound" >
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            ·<a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'><%#jup(DataBinder.Eval(Container.DataItem, "Title").ToString())%></a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a id="aTch" runat="server" href="Javascript:void(0)" style="color: #666666; font-weight: bold"><%#GetTeacherDisplayName(DataBinder.Eval(Container.DataItem, "Teacher").ToString())%></a>&nbsp;
                        </ItemTemplate>
                        <HeaderStyle Width="100px" Wrap="False" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" Wrap="False" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle Wrap="False" />
            </asp:GridView>
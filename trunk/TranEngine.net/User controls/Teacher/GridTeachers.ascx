<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridTeachers.ascx.cs" Inherits="User_controls_Teacher_GridTeachers" %>
<asp:GridView ID="GridTeachers" runat="server" AutoGenerateColumns="False" 
    EnableModelValidation="True" ShowHeader="False" Width="100%"      
    DataKeyNames="Id" Font-Size="12px" 
    onrowdatabound="GridTeachers_RowDataBound" GridLines="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            [<a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "FieldName").ToString()%>' ><%#DataBinder.Eval(Container.DataItem, "FieldName").ToString()%></a>]
                        </ItemTemplate>
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="90px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                          <div class="tch"><asp:PlaceHolder ID="ltTchs"  runat="server"></asp:PlaceHolder></div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left"  />
                    </asp:TemplateField>
                    <asp:TemplateField >
                        <ItemTemplate>
                          <div class="more"><a id="aMore" runat="server"  href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "FieldName").ToString()%>' >更多>></a></div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="center" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle Wrap="False" />
            </asp:GridView>

<style type="text/css" >
.more a { color: #333333; text-decoration: none; }
.more a:hover { color: #05a;text-decoration: none; }  
.tch a { color: #808080; text-decoration: none; }
.tch a:hover { color: #05a;text-decoration: none; } 
</style>
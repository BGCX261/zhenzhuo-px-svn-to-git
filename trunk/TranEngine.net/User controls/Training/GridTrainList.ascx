<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridTrainList.ascx.cs"
    Inherits="User_controls_Training_GridTrainList" %>
<asp:Label ID="lbFId" runat="server" Text="" Visible="False"></asp:Label>
<asp:Label ID="lbCId" runat="server" Text="" Visible="False"></asp:Label>
<train:PageGridView ID="GridList"  runat="server" AllowPaging="True" 
    AutoGenerateColumns="False"  GridLines="None" PageSize="15" Width="100%" 
    ShowHeader="False" DataKeyNames="Id" 
    onrowdatabound="GridList_RowDataBound" onpageindexchanging="GridList_PageIndexChanging" 
   >
<Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" 
                                style="border-color: #FFFFFF; border-width: 0px 5px 0px 5px; border-style: none solid none solid;">
                                <tr>
                                    <td class="blklist" style="">
                                    <h1>
                                    <span>添加日期:<%#DataBinder.Eval(Container.DataItem, "DateCreated").ToString()%></span><a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'><%#StripString(DataBinder.Eval(Container.DataItem, "Title").ToString(),60,false)%></a></h1>
                                   </td>
                                </tr>
                                <tr>
                                <td class="blklist">
                                <asp:Label ID="ltDes" runat="server"></asp:Label>
                                <div class='hr'></div>
                                </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" />
                    </asp:TemplateField>
                    
                </Columns>
                

</train:PageGridView>

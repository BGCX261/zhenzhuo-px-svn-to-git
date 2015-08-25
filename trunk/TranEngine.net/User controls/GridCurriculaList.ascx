<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridCurriculaList.ascx.cs" Inherits="User_controls_GridCurriculaList" %>
<asp:Label ID="lbFId" runat="server" Text="" Visible="False"></asp:Label>
<asp:Label ID="lbCId" runat="server" Text="" Visible="False"></asp:Label>
<train:PageGridView ID="GridList"  runat="server" AllowPaging="True" 
    AutoGenerateColumns="False"  GridLines="None" PageSize="15" Width="100%" 
    ShowHeader="False" DataKeyNames="Id" 
    onrowdatabound="GridList_RowDataBound" 
    onpageindexchanging="GridList_PageIndexChanging" >
<Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" 
                                style="border-color: #FFFFFF; border-width: 0px 5px 0px 5px; border-style: none solid none solid;">
                                <tr>
                                    <td class="blklist" style="">
                                    <h1>
                                    <a id="aPages" runat="server" href="Javascript:void(0)" title='<%#GetTitle(DataBinder.Eval(Container.DataItem, "Id").ToString())%>'><%#StripString(GetTitle(DataBinder.Eval(Container.DataItem, "Id").ToString()), 60, false)%></a></h1>
                                   </td>
                                </tr>
                                <tr>
                                <td class="blklist">
                                <div style ="border: 1px solid #DDDDDD; ">
                                    <div style='float:left ;' ><strong>开课时间:</strong><asp:Label ID="lbStartDate" runat="server"></asp:Label></div>
                                    <div style='float:left;margin: 0  0  0 20px;'><strong>培训天数:</strong><asp:Label ID="lbDays" runat="server"></asp:Label>天</div>
                                    <div style='float:left;margin: 0  0  0 20px;'><strong>开课地区:</strong><asp:Label ID="lbCity" runat="server"></asp:Label></div>
                                    <div style='float:left;margin: 0  0  0 20px;'><strong>培训费用:</strong>￥<asp:Label ID="lbCast" runat="server"></asp:Label></div>
                                    <div style="clear:both">
                                    </div>
                                    <div>课程内容:<asp:Label ID="ltDes" runat="server"></asp:Label></div>
                                </div>
                                
                                <div class='hr'></div>
                                </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" />
                    </asp:TemplateField>
                    
                </Columns>
                

</train:PageGridView>

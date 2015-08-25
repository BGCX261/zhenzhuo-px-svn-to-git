<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridExcellentList.ascx.cs" Inherits="User_controls_Excellent_GridExcellentList" %>
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
                <td style="text-align:left;width:170px;">
                    <div style="border: 1px solid #CCCCCC;float: left;height: 120px;padding: 4px;width: 150px;">
                    <a id="a1" runat="server" target="_blank" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'>
                        <img src='<%#SetImageUrl(DataBinder.Eval(Container.DataItem, "MastPic")) %>' width="150px" height="120px" alt=""  />
                    </a>
                    </div>
                </td>
                <td class="blklist" valign="top">
                    <p><div style=' display: block;' ><strong>培训主题:<a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'><%#StripString(DataBinder.Eval(Container.DataItem, "Title").ToString(), 50, false)%></a></strong></div></p>
                    <p><div style=' display: block;'><strong>所在城市:</strong><asp:Label ID="ltAddress" runat="server"></asp:Label></div></p>
                    <p><div style=' display: block;'><strong>培训时间:</strong><asp:Label ID="ltDays" runat="server"></asp:Label></div></p>
                </td>
                </tr>
                <tr>
                <td colspan="2" class="blklist">
                    <div class='hr'></div>
                </td>
                </tr>         
            </table>
          
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" Wrap="true" />
    </asp:TemplateField>                    
</Columns>
                

</train:PageGridView>
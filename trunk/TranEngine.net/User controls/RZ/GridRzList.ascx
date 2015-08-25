<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridRzList.ascx.cs" Inherits="User_controls_RZ_GridRzList" %>
<train:PageGridView ID="GridList"  runat="server" AllowPaging="True" 
    AutoGenerateColumns="False"  GridLines="None" PageSize="15" Width="100%" 
    ShowHeader="False" 
    onrowdatabound="GridList_RowDataBound" onpageindexchanging="GridList_PageIndexChanging" 
    >
<Columns>
    <asp:TemplateField>
        <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" 
                style="border-color: #FFFFFF; border-width: 0px 5px 0px 5px; border-style: none solid none solid;">
                <tr>
                <td class="blklist" valign="top">
                <div style ="border: 1px solid #DDDDDD; height:80px;">
                    <div style='float:left ;width:75%' ><strong><a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "Title").ToString()%>'><%#StripString(DataBinder.Eval(Container.DataItem, "Title").ToString(), 24, false)%></a></strong></div>
                    <div style="clear:both">
                    </div>
                    <div><strong>简介:</strong><asp:Label ID="ltDes" runat="server"></asp:Label></div>
                </div>
                
                </td>
                </tr>  
                <tr>
                <td  class="blklist">
                    <div class='hr'></div>
                </td>
                </tr>           
            </table>

        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" Wrap="true" />
    </asp:TemplateField>                    
</Columns>
                

</train:PageGridView>
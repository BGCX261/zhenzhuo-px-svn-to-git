<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridTeacherList.ascx.cs" Inherits="User_controls_Teacher_GridTeacherList" %>

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
                <td style="text-align:left;width:90px">
                    <a id="a1" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "DisplayName").ToString()%>'>
                        <img src='<%#SetImageUrl(DataBinder.Eval(Container.DataItem, "PhotoURL")) %>' width="80px" height="80px" alt="" style="border: 1px solid #C0C0C0" />
                    </a>
                </td>
                <td class="blklist" valign="top">
                <div style ="border: 1px solid #DDDDDD; height:80px">
                    <div style='float:left ;width:75%' ><strong>讲师名称:<a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "DisplayName").ToString()%>'><%#StripString(DataBinder.Eval(Container.DataItem, "DisplayName").ToString(), 30, false)%></a></strong></div>
                    <div style='float:left;margin: 0  0  0 20px;'><strong>所在城市:</strong><asp:Label ID="ltAddress" runat="server"></asp:Label></div>
                    <div style="clear:both">
                    </div>
                    <div><strong>讲师简介:</strong><asp:Label ID="ltDes" runat="server"></asp:Label></div>
                 </div>               
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

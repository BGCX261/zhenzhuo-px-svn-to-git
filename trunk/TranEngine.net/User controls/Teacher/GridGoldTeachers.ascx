<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridGoldTeachers.ascx.cs" Inherits="User_controls_Teacher_GridGoldTeachers" %>
<asp:GridView ID="GridTeachers" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                GridLines="None" ShowHeader="False" Width="100%"      
    DataKeyNames="UserName" Font-Size="12px" 
    onrowdatabound="GridTeachers_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" 
                                style="border-color: #FFFFFF; border-width: 0px 5px 0px 5px; border-style: none solid none solid;">
                <tr>
                    <td style="text-align:left;width:90px">
                    <a id="aPages" runat="server" href="Javascript:void(0)" title='<%#DataBinder.Eval(Container.DataItem, "DisplayName").ToString()%>'>
                        <img src='<%#SetImageUrl(DataBinder.Eval(Container.DataItem, "PhotoURL")) %>' width="80px" height="80px" alt="" style="border: 1px solid #C0C0C0" />
                    </a>
                    </td>
                    <td style="text-align:left;vertical-align:text-top;">
                    [<a id="aTch" runat="server" href="Javascript:void(0)" style=" font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "DisplayName").ToString()%></a>]&nbsp;
                        <%#StripString(DataBinder.Eval(Container.DataItem, "AboutMe").ToString(), 130)%>
                    </td>
                </tr>
                <tr>
                    <td class="blklist" colspan="2">                    
                    <div class='hr'></div>
                    </td>
                </tr>
               </table>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

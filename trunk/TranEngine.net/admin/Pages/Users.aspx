﻿<%@ Page Language="C#" MasterPageFile="~/admin/admin1.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="admin_newuser" Title="Create new user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">

    <div class="settings">
        <h1><%=Resources.labels.createNewUser %></h1>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" LoginCreatedUser="false">
            <TextBoxStyle Width="200" />
            <TitleTextStyle Font-Bold="true" Height="25" />
            
            <WizardSteps>
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" />
            </WizardSteps>
        </asp:CreateUserWizard>
        <asp:Label runat="server" ID="lblError" Text="Username is already taken" style="color:Red" visible="false" />
    </div>

    <div class="settings">
        <asp:GridView runat="server" ID="gridUsers" 
            AutoGenerateColumns="False"       
            BorderColor="#F8F8F8" 
            BorderStyle="Solid" 
            BorderWidth="1px" 
            RowStyle-BorderWidth="0"
            RowStyle-BorderStyle="None"
            gridlines="Horizontal"
            width="100%"
            AlternatingRowStyle-BackColor="#f8f8f8"
            AlternatingRowStyle-BorderColor="#f8f8f8" 
            HeaderStyle-BackColor="#F1F1F1"
            cellpadding="3"
            DataKeyNames="username" 
            OnLoad="gridUsers_Load" 
            AutoGenerateEditButton="True" 
            AutoGenerateDeleteButton="True" EnableModelValidation="True" AlternatingRowStyle-Wrap="False">
<AlternatingRowStyle BackColor="#F8F8F8" BorderColor="#F8F8F8" Wrap="False"></AlternatingRowStyle>
            <Columns>                
                <asp:TemplateField HeaderText="<%$ Resources:labels, username %>" HeaderStyle-Wrap="False">
                  <ItemTemplate>
                    <asp:Label ID="labelUsername" runat="server" Text='<%# Server.HtmlEncode(Eval("username").ToString()) %>' ></asp:Label>
                  </ItemTemplate>

<HeaderStyle Wrap="False"></HeaderStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="<%$ Resources:labels, password %>" FooterStyle-Wrap="False">
                  <ItemTemplate>
                    **********
                  </ItemTemplate>

<FooterStyle Wrap="False"></FooterStyle>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="<%$ Resources:labels, email %>">
                  <ItemTemplate>
                    <%# Server.HtmlEncode(Eval("email").ToString()) %>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:TextBox runat="server" ID="txtEmail" Text='<%# Eval("email") %>' />
                  </EditItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Roles" ItemStyle-Wrap="false" 
                    ItemStyle-Width="60%" >
<ItemStyle Wrap="False" ></ItemStyle>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle Wrap="False" />
            <EmptyDataRowStyle Wrap="False" />
            <FooterStyle Wrap="False" />
            <HeaderStyle HorizontalAlign="Left" Wrap="False" />

<RowStyle BorderWidth="0px" BorderStyle="None" Wrap="False"></RowStyle>
        </asp:GridView>
    </div>
</asp:Content>

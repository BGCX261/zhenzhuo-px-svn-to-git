﻿<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin1.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Pages_Curricula_Default" %>
<%@ Register src="DataGrid.ascx" tagname="DataGrid" tagprefix="uc1" %>
<%@ Register src="Menu.ascx" tagname="TabMenu" tagprefix="menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
<div class="settings">
        <menu:TabMenu ID="TabMenu" runat="server" /><br />
        <uc1:DataGrid ID="DataGridComments" runat="server" />
    </div>
</asp:Content>


﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tinyMCE.ascx.cs" Inherits="admin_tinyMCE" %>
<%@ Import Namespace="TrainEngine.Core" %>

<script type="text/javascript" src="<%=Utils.RelativeWebRoot %>editors/tiny_mce3/tiny_mce.js"></script>
<script type="text/javascript">
	tinyMCE.init({
		// General options
		mode: "exact",
		elements : "<%=txtContent.ClientID %>",
		theme: "advanced",
		plugins : "inlinepopups,fullscreen,contextmenu,emotions,table,advlink",
		convert_urls: false,
		language: "zh-cn",
	  // Theme options
		theme_advanced_buttons1: "fullscreen,|,cut,copy,paste,|,undo,redo,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,bullist,numlist,outdent,indent,|,link,unlink,sub,sup,removeformat,cleanup,charmap,emotions,|,formatselect,fontselect,fontsizeselect",
		theme_advanced_buttons2: "",
		theme_advanced_toolbar_location: "top",
		theme_advanced_toolbar_align: "left",
		theme_advanced_statusbar_location: "bottom",
		theme_advanced_resizing: true,
		
		tab_focus : ":prev,:next"
	});
</script>

<asp:TextBox runat="Server" ID="txtContent" CssClass="post" Width="100%" 
    Height="200px" TextMode="MultiLine" />
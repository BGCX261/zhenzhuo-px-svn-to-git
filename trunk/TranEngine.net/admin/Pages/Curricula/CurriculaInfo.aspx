<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurriculaInfo.aspx.cs" Inherits="admin_Pages_Curricula_ＣurriculaInfo" %>
<%@ Import Namespace="Resources" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../style.css" type="text/css" />
    <style type="text/css">
        body
        {
            font: 12px verdana;
            margin: 0;
            overflow: hidden;
        }
        #title
        {
            background: #F1F1F1;
            border-bottom: 1px solid silver;
            padding: 10px;
            font-weight: bold;
        }
        label
        {
            margin: 2px;
        }
        .field
        {
            padding: 2px;
        }
        #phEdit
        {
            padding: 10px;
            height: 160px;
            overflow: auto;
            overflow-x: hidden;
        }
        #bottom
        {
            background: #F1F1F1;
            border-top: 1px solid silver;
            padding: 10px;
            text-align: center;
        }
        a
        {
            text-decoration: none;
            color: #5C80B1;
        }
        
        a:hover
        {
            text-decoration: underline;
        }
    </style>
</head>
<body onkeypress="ESCclose(event)" scroll="no">
    <script type="text/javascript">
        function ESCclose(evt) {
            if (!evt) evt = window.event;

            if (evt && evt.keyCode == 27)
                window.parent.closeEditor();
        }
    </script>
    <form id="form1" runat="server">
    <div id="title">
      <div style="font-size:14px">
          <%=CurrentCurricula.Title%>- 
          新增课程安排
          
      </div>
    </div>
    <div runat="server" ID="phEdit">
  
  
  <div class="field">
  <label for="<%=txtStart.ClientID %>">课程日期</label>
  <asp:TextBox runat="server" ID="txtStart" Width="200px" />&nbsp;至&nbsp;<asp:TextBox runat="server" ID="txtEnd" Width="200px" />&nbsp;
      <br />
      (格式:yyyy-MM-dd)<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStart" Display="Dynamic" ErrorMessage="请输入开课日期" />&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEnd" Display="Dynamic" ErrorMessage="请输入结束日期" />
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
          ControlToValidate="txtStart" Display="Dynamic" ErrorMessage="请按格式输入开始日期" 
          ValidationExpression="\d{2,4}-\d{1,2}-\d{1,2}"></asp:RegularExpressionValidator>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
          ControlToValidate="txtEnd" Display="Dynamic" ErrorMessage="请按格式输入结束日期" 
          ValidationExpression="\d{4}-\d{1,2}-\d{1,2}"></asp:RegularExpressionValidator>
  </div>
  <div class="field">
  <label for="<%=txtCity.ClientID %>">上课地区</label>
    <asp:TextBox ID="txtCity" runat="server" Width="200px"></asp:TextBox>
    &nbsp;
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
          ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="请输入上课地区" 
          SetFocusOnError="True"></asp:RequiredFieldValidator>
</div>
<div class="field">
<label for="<%=txtCast.ClientID %>">培训费用</label>
    <asp:TextBox ID="txtCast" runat="server" Width="200px"></asp:TextBox>&nbsp;
      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
          ControlToValidate="txtCast" Display="Dynamic" ErrorMessage="请输入培训费用" 
          SetFocusOnError="True"></asp:RequiredFieldValidator>
      
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ControlToValidate="txtCast" Display="Dynamic" ErrorMessage="费用请输入数字" 
        ValidationExpression="\d{1,}"></asp:RegularExpressionValidator>
      
</div>
  </div>
    <div id="bottom">
    <asp:Button runat="server" ID="btnSave"  Text="保存" onclick="btnSave_Click" />
        <input onclick="parent.closeEditor()" type="button" 
            value="<%=labels.cancel %>" /></div>  
    </form>
</body>
</html>

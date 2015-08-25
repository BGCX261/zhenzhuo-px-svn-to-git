<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetRes.aspx.cs" Inherits="GetRes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:Label ID="lbMess" runat="server" Text="正在缓存文件,请等待..."></asp:Label>
    </div>
    <div><a href="javascript:void(0);" onclick="parent.closeEditor();"></a></div>
    </form>
</body>
</html>

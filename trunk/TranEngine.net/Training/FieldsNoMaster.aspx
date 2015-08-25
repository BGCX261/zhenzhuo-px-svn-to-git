<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FieldsNoMaster.aspx.cs" Inherits="Training_FieldsNoMaster" %>
<%@ Import Namespace="TrainEngine.Core" %>
<html>
<body>
    <table border="0" cellpadding="0" cellspacing="0"  width="100%">
        <tr>
            <td>
            <ul id="list" runat="server">
            </ul>
            </td>
        </tr>
        <tr>
         <td align="right">
            <a href='<%=Utils.AbsoluteWebRoot %>TrainList.aspx?fid=<%=fieldId%>' title="">更多...</a>
            </td>
        </tr>
    </table>
    </body>
</html>

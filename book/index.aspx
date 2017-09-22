<%@ Page Language="VB" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>申请账户</title>
    <style type="text/css">
        .auto-style10 {
            width: 40%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="auto-style10">
        <tr>
            <td colspan="2"><h1 style="text-align: center">图书角</h1></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: center">
                    <asp:Button ID="apply_button" runat="server" Text="我是用户" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
            <td style="text-align: center">
                    <asp:Button ID="apply_button0" runat="server" Text="我是管理员" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                    &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>

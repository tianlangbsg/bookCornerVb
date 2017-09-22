﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_type_up.aspx.vb" Inherits="lbbj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 138px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td class="auto-style1">请输入类别名：
                    </td>
                    <td>
                        <asp:TextBox ID="boxname" runat="server" Height="36px" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">请输入6位颜色值：
                    </td>
                    <td>
                        <asp:TextBox ID="boxcolor" runat="server" Height="36px" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Button ID="btnin" runat="server" Text="保存" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" ForeColor="White" Height="40px" Width="80px" />
                    </td>
                    <td>
                        <asp:Button ID="btndel" runat="server" Text="取消" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" ForeColor="White" Height="40px" Width="80px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

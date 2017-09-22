<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_edit_enable.aspx.vb" Inherits="user_edit_enable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style2 {
            width: 11%;
        }

        .auto-style3 {
            width: 24%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="return_button" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="26px" Width="56px" />
        <asp:Label ID="Label1" runat="server" Text="Label"><h1 style="text-align: center">修改用户信息</h1></asp:Label>

        <table style="width: 100%">
            <tr>
                <td class="auto-style3"></td>
                <td style="width: 15%">员工号：</td>
                <td class="auto-style2">
                    <asp:TextBox ID="DNo" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td style="width: 20%"></td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td>用户名：</td>
                <td class="auto-style2">
                    <asp:TextBox ID="DName" runat="server" Width="130px"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="auto-style3"></td>
                <td>
                    <asp:Button ID="write" runat="server" Text="修改" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" class="auto-style2">
                    <asp:Button ID="loseEff" runat="server" Text="生效" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>


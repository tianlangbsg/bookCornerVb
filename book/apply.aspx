<%@ Page Language="VB" AutoEventWireup="false" CodeFile="apply.aspx.vb" Inherits="apply" %>

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
        <div>
            <asp:Button ID="return_log_in" runat="server" Text="返回登录页" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        </div>
        <table align="center" class="auto-style10">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">申请审核</h1>
                </td>
            </tr>
            <tr>
                <td>员工号： &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:TextBox ID="user_code_textbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>姓名：</td>
                <td>
                    <asp:TextBox ID="user_name_textbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="apply_button" runat="server" Text="申请" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

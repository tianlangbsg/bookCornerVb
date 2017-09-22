<%@ Page Language="VB" AutoEventWireup="false" CodeFile="log_in.aspx.vb" Inherits="log_in" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>更新完了画面</title>
    <style type="text/css">
        .auto-style12 {
            height: 20px;
            width: 246px;
        }

        .auto-style13 {
            height: 20px;
            width: 247px;
        }

        .auto-style14 {
            width: 247px;
        }

        .auto-style15 {
            width: 246px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: left">
            <asp:Button ID="return_button" runat="server" Text="返回起始页" Style="height: 21px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        </div>
        <table align="center" style="width: 400px; align: center; text-align: center">
            <tr>
                <td colspan="3">
                    <h1>用户登录</h1>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">员工号： </td>
                <td class="auto-style13" colspan="2">
                    <asp:TextBox ID="user_code_textbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">姓名：</td>
                <td class="auto-style14" colspan="2">
                    <asp:TextBox ID="user_name_textbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style14" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style15">
                    <asp:Button ID="apply_button" runat="server" Text="登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td class="auto-style14">
                    <asp:Button ID="Button5" runat="server" Text="暂不登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td class="auto-style14">
                    <asp:Button ID="Button6" runat="server" Text="申请账户" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
            </tr>
        </table>
        <p style="text-align: center">
            &nbsp;
        </p>
    </form>
</body>
</html>

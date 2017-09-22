<%@ Page Language="VB" AutoEventWireup="false" CodeFile="right_manage.aspx.vb" Inherits="right_manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:Button ID="Button2" runat="server" Text="返 回" OnClick="Button2_Click" Width="70px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
            <table border="0" style="width: 100%">
                <tr>
                    <table>
                        <td style="text-align: center" class="auto-style1" colspan="2">
                            <h1 style="width: 1190px">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="系统管理员登陆"></asp:Label>
                            </h1>
                        </td>
                    </table>
            </table>
        </div>
        <asp:Label ID="Label1" runat="server" Text="用户名:"></asp:Label>&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
        <br />
        <asp:Label ID="Label2" runat="server" Text="密码  :"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <asp:Button ID="ButDengLu" runat="server" Text="登录"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
    </form>
</body>
</html>

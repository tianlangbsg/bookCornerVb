<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_set.aspx.vb" Inherits="book_set" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="Button1" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White" />

            <table>
                <td style="text-align: center" class="auto-style1" colspan="2">
                    <h1 style="width: 1190px">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="系统设置"></asp:Label>
                    </h1>
                </td>
            </table>
            <br />
            默认借阅周期：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            天<br />
            <br />
            每人最多借阅：<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            本<br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="保存"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>

        </div>
    </form>
</body>
</html>

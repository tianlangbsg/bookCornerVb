<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_type_in.aspx.vb" Inherits="tjfl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>请输入分类名称:
                    </td>
                    <td>
                        <asp:TextBox ID="boxname" runat="server" Height="40px" Width="281px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>请输入6位颜色值:
                    </td>
                    <td>
                        <asp:TextBox ID="boxcolor" runat="server" Height="40px" Width="281px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnjia" runat="server" Text="添加" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" ForeColor="White" Height="40px" Width="80px"/>
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

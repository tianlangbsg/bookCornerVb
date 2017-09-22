<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_comment.aspx.vb" Inherits="book_comment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>图书评论</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: right">
        当前用户：<asp:Label ID="user_name" runat="server" Text="Label"></asp:Label>
        &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="账户管理" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                        &nbsp;
        <asp:Button ID="quit" runat="server" Text="退出登录"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
    </div>
        <table class="auto-style1">
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="return_button" runat="server" Text="返回" style="height: 21px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
                <td style="text-align: center">
                    <h1>图书评论</h1></td>
                <td style="text-align: center" colspan="2">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    评分：<asp:DropDownList ID="DropDownList_grade" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem Value="5"></asp:ListItem>
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:CheckBox ID="anoy" runat="server" Text="匿名发表" />
                </td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
            </table>
        <p style="text-align: center">
                    <asp:TextBox ID="comment_TextBox1" runat="server" Height="149px" Width="358px"></asp:TextBox>
                 </p>
        <p style="text-align: center">
                    <asp:Button ID="attach_comment_button" runat="server" Text="发表评论" style="margin-bottom: 0px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                 </p>
    </form>
</body>
</html>

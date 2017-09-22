<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home_manager.aspx.vb" Inherits="home_manager" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 597px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" Height="22px" Text="退出登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        <asp:Label ID="Label1" runat="server" Text="Label"><h1 style="text-align: center">管理中心</h1></asp:Label>

        <table style="width: 100%">
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">书籍管理</td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_manager_list.aspx" style="color: #FF6666">图书管理</a></td>
                <td style="text-align: right">
                    <asp:Label ID="booknumber" runat="server" Text="Label" ForeColor="#FF0066"></asp:Label>本书上架</td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_insert.aspx" style="color: #FF6666">录入新书</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_type.aspx" style="color: #FF6666">图书类别</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_resource.aspx" style="color: #FF6666">图书来源</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_set.aspx" style="color: #FF6666">设置</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">成员管理</td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="user_review.aspx" style="color: #FF6666">审核名单</a></td>
                <td style="text-align: right">待审核<asp:Label ID="shenhe" runat="server" Text="Label" ForeColor="#FF0066"></asp:Label>人</td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="user_list.aspx" style="color: #FF6666">成员名单</a></td>
                <td style="text-align: right">有效成员<asp:Label ID="seeman" runat="server" Text="Label" ForeColor="#FF0066"></asp:Label>人</td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="user_chart.aspx" style="color: #FF6666">用户排行</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="book_search.aspx" style="color: #FF6666">借阅记录查询</a></td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">系统管理</td>
                <td></td>
            </tr>
            <tr>
                <td style="color: #008080; font-weight: bolder" class="auto-style1">●<a href="right_manage.aspx" style="color: #FF6666">权限管理</a></li>
                </td>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>

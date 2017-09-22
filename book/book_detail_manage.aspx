<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_detail_manage.aspx.vb" Inherits="book_detail_manage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 864px;
        }

        .auto-style6 {
            height: 8px;
            width: 383px;
        }

        .auto-style7 {
            width: 864px;
            height: 8px;
        }

        .auto-style10 {
            height: 10px;
            width: 383px;
        }

        .auto-style11 {
            width: 864px;
            height: 10px;
        }

        .auto-style13 {
            width: 1358px;
        }

        .auto-style16 {
            width: 64px;
            height: 28px;
        }

        .auto-style17 {
            width: 383px;
        }

        .auto-style26 {
            width: 66px;
        }

        .auto-style27 {
            width: 5px;
        }

        .auto-style28 {
            width: 36px;
        }

        .auto-style29 {
            width: 383px;
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="return_Button" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="30px" Width="64px" />
            <asp:Label ID="Label1" runat="server" Text="Label"><h1 style="text-align: center">图书详情</h1></asp:Label>
            <table style="width: 100%;">
                <tr style="border-style: double; border-color: #99CCFF">
                    <td class="auto-style10">来源</td>
                    <td class="auto-style11">
                        <asp:Label ID="Source" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                    <td rowspan="6" class="auto-style13">
                        <asp:Image ID="img" Width="141px" Height="200px" runat="server" Text="label"></asp:Image>共<asp:Label ID="pictrues" runat="server" Text=" "></asp:Label>
                        张图片</td>
                </tr>
                <tr style="border-style: double; border-color: #99CCFF">
                    <td class="auto-style17">录入时间</td>
                    <td class="auto-style1">
                        <asp:Label ID="IntoTime" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
                <tr style="border-style: double; border-color: #99CCFF">
                    <td class="auto-style6">借阅</td>
                    <td class="auto-style7">
                        <asp:Label ID="Borrows" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">收藏</td>
                    <td class="auto-style1">
                        <asp:Label ID="Collect" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">点赞</td>
                    <td class="auto-style1">
                        <asp:Label ID="Likes" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">书评</td>
                    <td class="auto-style1">
                        <asp:Label ID="Book_Review" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style29">简介</td>
                    <td class="auto-style16" colspan="2">
                        <asp:Label ID="intro" runat="server" BorderStyle="Double" BorderColor="#FF99CC"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Label ID="Hint" runat="server" Text="Label"></asp:Label>
            <asp:GridView ID="book_comment" runat="server" AutoGenerateColumns="False" Width="100%"
                DataKeyNames="USER_FULLNAME" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" />
                    <asp:BoundField DataField="COMMENT_GRADE" HeaderText="评分" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="COMMENT" HeaderText="评论" />
                    <asp:BoundField DataField="CREATED_AT" HeaderText="日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                </Columns>
                <RowStyle ForeColor="#333333" BackColor="#FFFBD6" Wrap="true" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center" class="auto-style26">
                        <asp:Button ID="write" runat="server" Text="编辑" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" /></td>
                    <td style="text-align: center" class="auto-style27">
                        <asp:Button ID="ago" runat="server" Text="借阅历史" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" /></td>
                    <td style="text-align: center" class="auto-style28">
                        <asp:Button ID="godown" runat="server" Text="下架" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

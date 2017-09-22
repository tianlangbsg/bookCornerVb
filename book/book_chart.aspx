<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_chart.aspx.vb" Inherits="book_chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>排行榜</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            margin-right: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right">
            当前用户：<asp:Label ID="user_name" runat="server" Text="Label"></asp:Label>
            &nbsp;
                    <asp:Button ID="user_center" runat="server" Text="账户管理" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
            &nbsp;
            <asp:Button ID="quit0" runat="server" Text="退出登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        </div>
        <table align="center" class="auto-style1">
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="return_button" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" colspan="2">
                    <h1>图书排行榜</h1>
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="last_month_chart" runat="server" Text="上月排行榜" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center">
                    <asp:Button ID="borrow_chart" runat="server" Text="借阅排行榜" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center">
                    <asp:Button ID="favourite_chart" runat="server" Text="收藏排行榜" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center">
                    <asp:Button ID="hot_chart" runat="server" Text="人气排行榜" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="chart_gridview" runat="server" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="BOOK_ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">

            <AlternatingRowStyle BackColor="White" />

            <Columns>
                <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" />

                <asp:HyperLinkField DataTextField="BOOK_ID" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="编号" ItemStyle-HorizontalAlign="Left" />
                <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="BOOK_TYPE_NAME" HeaderText="类型" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="BOOK_TYPE_COLOR" HeaderText="类型颜色" ControlStyle-BackColor="" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="BOOK_CREATED_DATE" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="BORROW_COUNT" HeaderText="借阅历史" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="COMMENT_COUNT" HeaderText="评论" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="BORROW_FLAG" HeaderText="状态" ItemStyle-HorizontalAlign="Left" />
                <asp:HyperLinkField DataTextField="BORROW" DataNavigateUrlFields="BOOK_ID" HeaderText="操作" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" ItemStyle-HorizontalAlign="Left" />
            </Columns>
            <RowStyle ForeColor="#333333" BackColor="#FFFBD6" Wrap="False" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <p style="text-align: center">
            <asp:Label ID="result_Label1" runat="server" Text="Label"></asp:Label>
        </p>
    </form>
</body>
</html>

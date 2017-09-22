<%@ Page Language="VB" AutoEventWireup="false" CodeFile="home_book.aspx.vb" Inherits="searchPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>起始页</title>
    <style type="text/css">
        .auto-style26 {
            width: 100%;
        }
    </style>
<%--    <script type="text/javascript">
        
        function pageonload() {
            var warning_flag = document.getElementById("warning_flag").attributes["limit"]
            if (warning_flag == true) {
                alert('借书数量超过上限，请先还书后再来！')
            }
        }
    </script>--%>
</head>
<body >
    <form id="form1" runat="server" align="center">
        <div>
            <table border="0" style="width: 100%; align: center">
                <tr>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: right" colspan="2">当前用户：<asp:Label ID="user_name" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;
                    <asp:Button ID="user_center" runat="server" Text="账户管理" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                        &nbsp;&nbsp;
                    <asp:Button ID="quit" runat="server" Text="退出登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: center">
                        <h1>书架</h1>
                    </td>
                    <td style="text-align: center">&nbsp;</td>
                </tr>

                <tr>
                    <td style="text-align: center">&nbsp;</td>
                    <td style="text-align: center">&nbsp;&nbsp; 
                    <asp:Button ID="book_chart_button" runat="server" Text="排行榜" Style="margin-right: 15px" Width="141px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                    </td>
                    <td style="text-align: center">&nbsp;</td>
                </tr>

            </table>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                &nbsp;&nbsp;&nbsp;
            </asp:Panel>
            <table align="center" class="auto-style26" style="width: 500px">
                <tr>
                    <td>请输入书名或条形码：</td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Width="124px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="searchBtn0" runat="server" Text="検索" Width="90px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td>条件检索：</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>借阅次数：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>降序</asp:ListItem>
                            <asp:ListItem>升序</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>录入时间：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList2" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>降序</asp:ListItem>
                            <asp:ListItem>升序</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>图书类别：</td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" runat="server" ForeColor="Blue" Width="134px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="searchBtn" runat="server" Text="検索" Width="90px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <table align="center" class="auto-style26">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <asp:GridView ID="book_shelf_GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="BOOK_ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">

                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" ItemStyle-HorizontalAlign="Right" />

                            <asp:HyperLinkField DataTextField="BOOK_ID" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="编号" ItemStyle-HorizontalAlign="Right" />
                            <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="BOOK_TYPE_NAME" HeaderText="类型" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="BOOK_TYPE_COLOR" HeaderText="类型颜色"/>
                            <asp:BoundField DataField="BOOK_CREATED_DATE" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BORROW_COUNT" HeaderText="借阅历史" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="COMMENT_COUNT" HeaderText="评论" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BORROW_FLAG" HeaderText="状态" ItemStyle-HorizontalAlign="Left" />
                            <asp:HyperLinkField DataTextField="BORROW" DataNavigateUrlFields="BOOK_ID" HeaderText="操作" DataNavigateUrlFormatString="book_borrow.aspx?book_id={0}" />
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
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

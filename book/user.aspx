<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user.aspx.vb" Inherits="user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户中心</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 4px;
        }

        .auto-style5 {
            height: 148px;
        }

        .auto-style6 {
            height: 25px;
        }

        .auto-style7 {
            width: 150px;
            height: 25px;
        }

        .auto-style8 {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right">
            当前用户：<asp:Label ID="user_name" runat="server" Text="Label"></asp:Label>
            <asp:Button ID="quit" runat="server" Text="退出登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        </div>
        <table class="auto-style1">
            <tr>
                <td style="text-align: left" class="auto-style8" colspan="6">
                    <asp:Button ID="Button5" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style8"></td>
                <td style="text-align: center" colspan="4" class="auto-style8">
                    <h1>用户中心</h1>
                </td>
                <td style="text-align: center" class="auto-style8"></td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="6" class="auto-style5">
                    <asp:GridView ID="GridView_user_info" runat="server" AutoGenerateColumns="False" Width="500px"
                        DataKeyNames="BORROW_COUNT" HorizontalAlign="Center">

                        <Columns>
                            <asp:BoundField DataField="BORROW_COUNT" HeaderText="已借" />
                            <asp:BoundField DataField="STAYSTILL_COUNT" HeaderText="待还" />
                            <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" />
                            <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" />
                            <asp:BoundField DataField="COMMENT_COUNT" HeaderText="书评" />
                        </Columns>
                        <RowStyle Wrap="False" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style6"></td>
                <td style="text-align: center" class="auto-style7">
                    <asp:Button ID="borrow_button" runat="server" Text="借还详情" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" class="auto-style7">
                    <asp:Button ID="favourite_button" runat="server" Text="我的收藏" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" class="auto-style7">
                    <asp:Button ID="praise_button" runat="server" Text="我的点赞" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" class="auto-style7">
                    <asp:Button ID="comment_button" runat="server" Text="我的书评" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center" class="auto-style6"></td>
            </tr>
        </table>
        <table class="auto-style1" width="700px">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td style="text-align: left; width: 800px;">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <asp:GridView ID="borrowandreturn" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="BOOK_ID" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:HyperLinkField DataTextField="BOOK_ID" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="编号" />
                            <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="BORROW_FLAG" HeaderText="状态" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="BORROW_DATE" HeaderText="借阅日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PLAN_RETURN_DATE" HeaderText="计划归还日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="RETURN_DATE" HeaderText="还书日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                            <asp:HyperLinkField DataTextField="CHANGE_RETURN_DATE" DataNavigateUrlFields="BOOK_ID" HeaderText="修改还书时间" DataNavigateUrlFormatString="book_return.aspx?book_id={0}" />
                            <asp:HyperLinkField DataTextField="BORROW" DataNavigateUrlFields="BOOK_ID" HeaderText="还书" DataNavigateUrlFormatString="book_return2.aspx?book_id={0}" />
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
                    <asp:GridView ID="myfavourite" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="BOOK_ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">

                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" />

                            <asp:HyperLinkField DataTextField="BOOK_ID" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="编号" />
                            <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="BOOK_CREATED_DATE" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="BORROW_COUNT" HeaderText="借阅历史" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="COMMENT_COUNT" HeaderText="评论" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BORROW_FLAG" HeaderText="状态" ItemStyle-HorizontalAlign="Left" />
                            <asp:HyperLinkField DataTextField="BORROW" DataNavigateUrlFields="BOOK_ID" HeaderText="操作" DataNavigateUrlFormatString="book_borrow2.aspx?book_id={0}&mode=2" />
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
                    <asp:GridView ID="mypraise" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="BOOK_ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">

                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" />

                            <asp:HyperLinkField DataTextField="BOOK_ID" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="编号" />
                            <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" />
                            <asp:BoundField DataField="BOOK_CREATED_DATE" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="BORROW_COUNT" HeaderText="借阅历史" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="COMMENT_COUNT" HeaderText="评论" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="BORROW_FLAG" HeaderText="状态" ItemStyle-HorizontalAlign="Left" />
                            <asp:HyperLinkField DataTextField="BORROW" DataNavigateUrlFields="BOOK_ID" HeaderText="操作" DataNavigateUrlFormatString="book_borrow2.aspx?book_id={0}&mode=3" />
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
                    <br />
                    <asp:GridView ID="mycomment" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="COMMENT_ID" OnSelectedIndexChanging="GridView1_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail.aspx?book_id={0}" HeaderText="书名" ItemStyle-HorizontalAlign="left" />
                            <asp:BoundField DataField="COMMENT_ID" HeaderText="评论编号" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="ANONYMOU_FLAG" HeaderText="是否匿名" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="CREATED_AT" HeaderText="评论日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="COMMENT_GRADE" HeaderText="评级" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="COMMENT" HeaderText="评论" ItemStyle-Width="100">
                                <ItemStyle Width="700" Wrap="true" />
                            </asp:BoundField>
                            <%--                            <asp:BoundField DataField="COMMENT" HeaderText="书评" ItemStyle-HorizontalAlign="Left" />--%>
                            <asp:HyperLinkField DataTextField="OPERATION" DataNavigateUrlFields="COMMENT_ID" DataNavigateUrlFormatString="book_comment_delete.aspx?comment_id={0}&mode=4" HeaderText="操作" />
                        </Columns>
                        <RowStyle ForeColor="#333333" BackColor="#FFFBD6" Wrap="TRUE" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                    <br />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

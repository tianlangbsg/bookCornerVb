<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_detail.aspx.vb" Inherits="book_chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>图书详情</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style4 {
            height: 20px;
        }

        .auto-style7 {
            width: 108px;
        }

        .auto-style10 {
            height: 20px;
            width: 238px;
        }

        .auto-style12 {
        }

        .auto-style13 {
            width: 238px;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right">
            <table class="auto-style1">
                <tr>
                    <td>
                        <div style="text-align: right">
                            <table class="auto-style1">
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Button ID="return_button" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="text-align: right">当前用户：<asp:Label ID="user_name" runat="server" Text="Label"></asp:Label>
                        &nbsp;
                    <asp:Button ID="user_center" runat="server" Text="账户管理" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                        &nbsp;
                        <asp:Button ID="quit0" runat="server" Text="退出登录" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                    </td>
                </tr>
            </table>
        </div>
        <table class="auto-style1" align="center" style="width: 700px;text-align:c">
            <tr>
                <td style="text-align: center" colspan="4"><h1>图书详情</h1></td>
            </tr>
            <tr>
                <td style="text-align: right">书名：</td>
                <td style="text-align: left" class="auto-style13">
                    <asp:Label ID="BOOK_NAME" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: LEFT" rowspan="9" class="auto-style12">
                    <asp:Image ID="Image1" runat="server" />
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">来源类型：</td>
                <td style="text-align: left" class="auto-style13">
                    <asp:Label ID="SOURCE_TYPE_NAME" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style4">录入时间：</td>
                <td style="text-align: left" class="auto-style10">
                    <asp:Label ID="BOOK_CREATED_DATE" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center" class="auto-style4">
                    <asp:Label ID="pic_num_label" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style4">借阅历史：</td>
                <td style="text-align: left" class="auto-style10">
                    <asp:Label ID="BORROW_COUNT" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center" class="auto-style4">
                    <asp:Button ID="more_pic_button" runat="server" Text="更多图片&gt;&gt;" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style4">收藏：</td>
                <td style="text-align: left" class="auto-style10">
                    <asp:Label ID="FAVOURITE_COUNT" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center" class="auto-style4"></td>
            </tr>
            <tr>
                <td style="text-align: right">点赞：</td>
                <td style="text-align: left" class="auto-style13">
                    <asp:Label ID="PRAISE_COUNT" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">评论：</td>
                <td style="text-align: left" class="auto-style13">
                    <asp:Label ID="COMMENT_COUNT" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right" class="auto-style4">状态：</td>
                <td style="text-align: left" class="auto-style10">
                    <asp:Label ID="BORROW_FLAG" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="text-align: center" class="auto-style4"></td>
            </tr>
            <tr>
                <td style="text-align: center">&nbsp;</td>
                <td style="text-align: center" class="auto-style13">&nbsp;</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            </table>
        <br />
        <table align="center" class="auto-style1" style="width: 600px">
            <tr>
                <td style="width: 400px; text-align: center;">
                    <asp:Button ID="borrow_button" runat="server" Text="借阅" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
                <td style="width: 400px; text-align: center;">
                    <asp:Button ID="favourite_button" runat="server" Text="收藏" Height="21px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
                <td style="width: 400px; text-align: center;">
                    <asp:Button ID="praise_button" runat="server" Text="点赞" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
                <td style="width: 400px; text-align: center;">
                    <asp:Button ID="comment_button" runat="server" Text="评价" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                </td>
            </tr>
            <tr>
                <td style="width: 400px; text-align: center;">简介：</td>
                <td colspan="3">
                    <asp:Label ID="BOOK_INTRO" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" class="auto-style1" style="width: 700px" border="1">
            <tr>
                <td class="auto-style7">书评：</td>
                <td>
                    <asp:GridView ID="comment_GridView2" runat="server" AutoGenerateColumns="False" Width="100%"
                        DataKeyNames="USER_FULLNAME" HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" ItemStyle-HorizontalAlign="Left"/>
                            <asp:BoundField DataField="COMMENT_GRADE" HeaderText="评分" ItemStyle-HorizontalAlign="Right"/>
                            <asp:BoundField DataField="COMMENT" HeaderText="评论"  ItemStyle-Width="100">
                                <ItemStyle Width="300" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATED_AT" HeaderText="日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Right"/>
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle Wrap="true" Width="300px" BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="result" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

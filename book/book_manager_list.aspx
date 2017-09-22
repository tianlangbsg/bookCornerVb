<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_manager_list.aspx.vb" Inherits="book_manager_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style5 {
            height: 25px;
        }

        .auto-style13 {
            width: 78px;
        }

        .auto-style14 {
            width: 78px;
            height: 25px;
        }

        .auto-style15 {
            width: 166px;
        }

        .auto-style20 {
            width: 38px;
        }

        .auto-style21 {
            width: 38px;
            height: 25px;
        }

        .auto-style23 {
            width: 741px;
        }

        .auto-style25 {
            width: 166px;
            height: 25px;
        }

        .auto-style27 {
            width: 166px;
            height: 23px;
        }

        .auto-style28 {
            width: 38px;
            height: 23px;
        }

        .auto-style29 {
            width: 78px;
            height: 23px;
        }

        .auto-style30 {
            height: 23px;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <asp:Button ID="return_button" runat="server"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White" Height="22px" Text="返回" Width="84px" />
            <table border="0" style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <h1>
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="图书管理"></asp:Label></h1>
                    </td>
                </tr>
            </table>
            <table border="0" style="width: 100%; height: 209px;">
                <tr>
                    <td class="auto-style25">请输入书名或条形码：</td>
                    <td class="auto-style21" style="width: 17%">
                        <asp:TextBox ID="NameSearch" runat="server" Width="130px"></asp:TextBox>
                    </td>
                    <td class="auto-style14" style="width: 20%">
                        <asp:Button ID="searchBtn0" runat="server" Text="検索" Width="90px"  BackColor="#820000"  BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White" />
                    </td>
                    <td class="auto-style5" style="width: 20%"></td>
                    <td class="auto-style5" style="width: 20%"></td>
                </tr>
                <tr>
                    <td class="auto-style25">是否上架：</td>
                    <td class="auto-style21">
                        <asp:DropDownList ID="Upbooks" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem>全部</asp:ListItem>
                            <asp:ListItem>上架中</asp:ListItem>
                            <asp:ListItem>下架中</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style14"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style25">借阅次数：</td>
                    <td class="auto-style21">
                        <asp:DropDownList ID="BorrowTime" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>降序</asp:ListItem>
                            <asp:ListItem>升序</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style14"></td>
                </tr>
                <tr>
                    <td class="auto-style27">录入时间：</td>
                    <td class="auto-style28">
                        <asp:DropDownList ID="InsertTime" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>降序</asp:ListItem>
                            <asp:ListItem>升序</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style29"></td>
                    <td class="auto-style30"></td>
                </tr>
                <tr>
                    <td class="auto-style15">图书类别：</td>
                    <td class="auto-style20">
                        <asp:DropDownList ID="booksty" runat="server" ForeColor="Blue" Width="134px">
                            <asp:ListItem>降序</asp:ListItem>
                            <asp:ListItem>升序</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style13">
                        <asp:Button ID="searchBtn" runat="server" Text="検索" Width="90px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style25"></td>
                    <td class="auto-style21">
                        <asp:CheckBox ID="SeeBorrow" runat="server" Text="只看待还" Width="90px" />
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td class="auto-style25">
                        <asp:Button ID="InsertBooks" runat="server"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White" Height="22px" Text="添加图书" Width="84px" />
                    </td>
                    <td class="auto-style21"></td>
                    <td class="auto-style14">&nbsp;</td>
                    <td class="auto-style5"></td>
                </tr>
                <tr>
                    <td colspan="5" class="auto-style23">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="height: 2px; width: 100%;">
                        <asp:GridView ID="AllBook" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="BOOK_ID" OnSelectedIndexChanging="AllBook_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Style="margin-left: 0px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>  
                                <asp:BoundField DataField="BOOK_ID" HeaderText="书号" />                   
                                <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" />
                                <asp:HyperLinkField DataTextField="BOOK_NAME" DataNavigateUrlFields="BOOK_ID" DataNavigateUrlFormatString="book_detail_manage.aspx?bookid={0}" HeaderText="书名" />
                                <asp:BoundField DataField="BOOK_TYPE_NAME" HeaderText="类型" />
                                <asp:BoundField DataField="BOOK_CREATED_DATE" HeaderText="录入日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" />
                                <asp:BoundField DataField="BORROW_COUNT" HeaderText="借阅历史" ItemStyle-HorizontalAlign="Right">        
                                </asp:BoundField>
                                <asp:BoundField DataField="FAVOURITE_COUNT" HeaderText="收藏" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:BoundField DataField="PRAISE_COUNT" HeaderText="点赞" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:BoundField DataField="COMMENT_COUNT" HeaderText="评论" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:BoundField DataField="BORROW_FLAG" HeaderText="借书状态" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:BoundField DataField="DEL_FLAG1" HeaderText="图书状态" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundField>
                                <asp:CommandField SelectText="編集" ShowSelectButton="True" />
                            </Columns>
                            <RowStyle ForeColor="#333333" BackColor="#FFFBD6" />
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
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center"></asp:Panel>
        </div>
    </form>
</body>
</html>

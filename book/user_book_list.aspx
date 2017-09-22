<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_book_list.aspx.vb" Inherits="user_book_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="40pt" ForeColor="Maroon" Font-Bold="True"></asp:Label></td>
                </tr>
            </table>
            <asp:GridView ID="yhsd" runat="server" AutoGenerateColumns="False" Width="700px"
                DataKeyNames="BOOK_ID" CellPadding="4" ForeColor="#333333" GridLines="None" Style="width: 100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="BORROW_ID" HeaderText="借阅记录" ItemStyle-HorizontalAlign="Center" />
                     <asp:BoundField DataField="BOOK_ID" HeaderText="书号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="BOOK_NAME" HeaderText="书名" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="BORROW_DATE" HeaderText="借阅日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                    <asp:BoundField DataField="RETURN_DATE" HeaderText="归还日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                    <asp:CommandField SelectText="详情" ShowSelectButton="True" FooterStyle-ForeColor="red" />
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
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnback" runat="server" Text="返 回" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="43px" Width="132px" Font-Size="15" BackColor="#820000" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

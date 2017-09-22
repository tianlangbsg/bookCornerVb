<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_history.aspx.vb" Inherits="book_history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                        <asp:Button ID="Button2" runat="server" Text="返回"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                    <br />
            <table>
                <td style="text-align: center" class="auto-style1" colspan="2">
                    <h1 style="width: 1190px">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="借阅历史"></asp:Label>
                    </h1>
                </td>
            </table>
            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="return" />
            全部<asp:RadioButton ID="RadioButton2" runat="server" GroupName="return" />
            已还<asp:RadioButton ID="RadioButton3" runat="server" GroupName="return" />
            待还
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            ---<asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="搜索" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                CellPadding="4" ForeColor="#333333" GridLines="None">

                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="BORROW_ID" HeaderText="借阅记录id" />
                    <asp:BoundField DataField="BOOK_ID" HeaderText="借阅人id" />
                    <asp:BoundField DataField="USER_ID" HeaderText="借阅人姓名" />
                    <asp:BoundField DataField="BORROW_DATE" HeaderText="借阅时间" />
                    <asp:BoundField DataField="RETURN_DATE" HeaderText="归还时间" />
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

        </div>
    </form>
</body>
</html>

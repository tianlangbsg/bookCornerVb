<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_list.aspx.vb" Inherits="user_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户列表</title>
    <style type="text/css">
        .auto-style1 {
            height: 2px;
            width: 1013px;
        }

        .auto-style2 {
            width: 377px;
        }

        .auto-style3 {
            width: 377px;
            height: 48px;
        }

        .auto-style4 {
            height: 48px;
        }

        .auto-style5 {
            height: 2px;
            width: 945px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="text-align: left">
        <asp:Button ID="write" runat="server" Text="返回" Width="71px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
        <asp:Label ID="Label1" runat="server" Text="Label"><h1 style="text-align: center">成员名单</h1></asp:Label>
        <table style="width: 100%">
            <tr>
                <td style="text-align: center; width: 50%;" class="auto-style3">
                    <asp:Button ID="searchEffMen" runat="server" Text="有效成员" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
                <td style="text-align: center; width: 50%;" class="auto-style4">
                    <asp:Button ID="searchEffIna" runat="server" Text="无效成员" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="auto-style2" colspan="2">&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="Effmember" runat="server" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="USER_ID" OnSelectedIndexChanging="Effmember_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="USER_ID" HeaderText="用户ID" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USER_CODE" HeaderText="员工号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="JOIN_AT" HeaderText="加入日期" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
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
        <asp:GridView ID="Inamember" runat="server" AutoGenerateColumns="False" Width="100%"
            DataKeyNames="USER_ID" OnSelectedIndexChanging="Inamember_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="USER_ID" HeaderText="用户ID" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USER_CODE" HeaderText="员工号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="JOIN_AT" HeaderText="加入日期" DataFormatString="{0:yyyy-MM-dd}" ItemStyle-HorizontalAlign="Center" />
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
        <p>
            <asp:Label ID="message" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>

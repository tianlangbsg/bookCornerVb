<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_chart.aspx.vb" Inherits="user_chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 33.3%;
        }
        .auto-style3 {
            width: 33.3%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btnback" runat="server" Text="返回" Height="37px" Width="69px" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" Font-Size="15pt" ForeColor="white" /></td>
                </tr>
                <tr>
                    <td>
                        <h1 style="text-align: center">用户排行</h1>
                    </td>
                </tr>
            </table>
            <table style="width: 100%; margin: 0 auto; text-align: center;">
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="btnread" runat="server" Text="阅读数排行" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="43px" Width="187px" Font-Size="15" BackColor="#820000" />
                    </td>
                    <td class="auto-style1">
                        <asp:Button ID="btnasc" runat="server" Text="加入时间（升序）" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="43px" Width="187px" Font-Size="15" BackColor="#820000" />
                    </td>
                    <td>
                        <asp:Button ID="btndesc" runat="server" Text="加入时间（降序）" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" Height="43px" Width="187px" Font-Size="15" BackColor="#820000" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%; margin: 0 auto; text-align: center;">
                <tr>
                    <td>
                        <asp:GridView ID="phmd" runat="server" AutoGenerateColumns="False" Width="700px"
                            DataKeyNames="USER_ID" OnSelectedIndexChanging="phmd_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None" Style="width: 100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="用户ID" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="JOIN_AT" HeaderText="加入日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                                <asp:BoundField DataField="BORROW_COUNT" HeaderText="用户借阅次数" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="BORROW_TIME" HeaderText="最近借书时间" ItemStyle-HorizontalAlign="Center" />
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
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

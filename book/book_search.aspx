<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_search.aspx.vb" Inherits="book_search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .a {
            Text-align ="center";
        }

        .auto-style20 {
            width: 659px;
        }

        .auto-style13 {
            width: 78px;
        }

        .auto-style27 {
        }

        .auto-style28 {
            width: 410px;
        }

        .auto-style29 {
            width: 605px;
            height: 6px;
        }

        .auto-style30 {
            width: 410px;
            height: 6px;
        }

        .auto-style31 {
            width: 659px;
            height: 6px;
        }

        .auto-style32 {
            width: 78px;
            height: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="returnbb" runat="server" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" Text="返回" ForeColor="White" Height="29px" Width="62px" />
            <table border="0" style="width: 100%; height: 100px;">
                <tr>
                    <td class="auto-style27" colspan="4">
                        <asp:Label ID="Label2" runat="server" Text="Label"><h1 style="text-align: center">借阅记录查询</h1></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style27">&nbsp;</td>
                    <td class="auto-style28">开始日期：
                        <asp:TextBox ID="Star_Day" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="16px" ImageUrl="~/images/01.png" />
                    </td>
                    <td class="auto-style20">
                        <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" Width="220px" Visible="False" ShowGridLines="True">
                            <DayHeaderStyle BackColor="#FFCC66" Height="1px" Font-Bold="True" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                        </asp:Calendar>
                    </td>
                    <td class="auto-style13"></td>
                </tr>
                <tr>
                    <td class="auto-style29"></td>
                    <td class="auto-style30">结束日期：
                        <asp:TextBox ID="End_Day" runat="server"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton2" runat="server" Height="16px" ImageUrl="~/images/01.png" />
                    </td>
                    <td class="auto-style31">
                        <asp:Calendar ID="Calendar2" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" Width="220px" Visible="False" ShowGridLines="True">
                            <DayHeaderStyle BackColor="#FFCC66" Height="1px" Font-Bold="True" />
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                            <OtherMonthDayStyle ForeColor="#CC9966" />
                            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                            <SelectorStyle BackColor="#FFCC66" />
                            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                        </asp:Calendar>
                    </td>
                    <td class="auto-style32"></td>
                </tr>
                <tr>
                    <td class="auto-style27">&nbsp;</td>
                    <td class="auto-style28">借书状态：
                    <asp:DropDownList ID="book_borrow_flag" runat="server" ForeColor="Blue" Width="154px" Height="23px" BackColor="#FFCCCC">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem>已还</asp:ListItem>
                        <asp:ListItem>待还</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td class="auto-style13">
                        <asp:Button ID="searchBtn" runat="server" Text="查询" Width="90px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="message" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="width: 100%">
                    <td style="width: 100%">
                        <asp:GridView ID="book_centent" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="BOOK_NAME" HeaderText="图书名称" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_CODE" HeaderText="借阅人名" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BORROW_DATE" HeaderText="借阅日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PLAN_RETURN_DATE" HeaderText="计划归还日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="RETURN_DATE" HeaderText="实际归还日期" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="KK" HeaderText="借书状态" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
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

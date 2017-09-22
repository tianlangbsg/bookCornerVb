<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_return.aspx.vb" Inherits="book_return" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>更新完了画面</title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 34px;
            width: 102px;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
        }
        .auto-style7 {
            height: 26px;
            width: 285px;
        }
        .auto-style8 {
            width: 285px;
        }
        .auto-style9 {
            width: 179px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
        <table class="auto-style1">
            <tr>
                <td style="text-align: left" class="auto-style7">
                    <asp:Button ID="Button5" runat="server" Text="返回主页" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White"/>
                </td>
                <td style="text-align: center" class="auto-style4" colspan="2">
                    <h1>更改還書日期</h1></td>
                <td style="text-align: center" class="auto-style2">
                    </td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style8" rowspan="2">
                    &nbsp;</td>
                <td style="text-align: left" class="auto-style3">
                    还书日期&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="return_date" runat="server"></asp:TextBox>
                    <br />
                    &nbsp;
                </td>
                <td style="text-align: left" class="auto-style9">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px">
                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                        <OtherMonthDayStyle ForeColor="#CC9966" />
                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                        <SelectorStyle BackColor="#FFCC66" />
                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td style="text-align: center" class="auto-style5" colspan="2">
                    <asp:Button ID="apply_button" runat="server" Text="提交" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White"/>
                </td>
            </tr>
            </table>
        <p style="text-align: center">
                    &nbsp;</p>
        <p style="text-align: center">
                    &nbsp;</p>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_admin.aspx.vb" Inherits="user_admin" %>

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
                    <td>
                        <asp:Button ID="btnback" runat="server" Text="返回" Height="37px" Width="69px" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" Font-Size="15pt" ForeColor="white" /></td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <h1>管理员管理</h1></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnadmin" runat="server" Text="管理员列表" Height="50px" BorderColor="#FFCCCC" BorderStyle="Double" Width="140px" BackColor="#820000" Font-Bold="True" Font-Size="15pt" ForeColor="White" />
                    </td>
                    <td style="text-align: center">
                        <asp:Button ID="btnin" runat="server" Text="添加管理员" Height="50px" BorderColor="#FFCCCC" BorderStyle="Double" Width="140px" BackColor="#820000" Font-Bold="True" Font-Size="15pt" ForeColor="white" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="admin" runat="server" AutoGenerateColumns="False" Width="700px" align="center" Style="width: 100%"
                DataKeyNames="USER_ID" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCheck" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="USER_ID" HeaderText="用户id" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="USER_CODE" HeaderText="员工号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="APPLY_AT" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                    <asp:BoundField DataField="JOIN_AT" HeaderText="加入日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
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
            <asp:CheckBox ID="chkAll" runat="server" Text="全选" AutoPostBack="True" />
            <table style="width: 100%; margin: 0 auto; text-align: center;">
                <tr>
                    <td style="font-size: 40px;">
                        <asp:Button ID="btndel" runat="server" BorderColor="#FFCCCC" BorderStyle="Double" Text="删除权限" ForeColor="White" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                        <asp:Button ID="btntj" runat="server" BorderColor="#FFCCCC" BorderStyle="Double" Text="添加权限" ForeColor="White" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

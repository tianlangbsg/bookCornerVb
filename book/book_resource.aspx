<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_resource.aspx.vb" Inherits="book_resource" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .auto-style2 {
            width: 50%;
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
                    <td style="text-align: center">
                        <h1>图书来源</h1></td>
                </tr>
            </table>
            <asp:GridView ID="tsly" runat="server" AutoGenerateColumns="False" Width="700px" align="center" Style="width: 100%"
                DataKeyNames="SOURCE_TYPE_ID" OnSelectedIndexChanging="tsly_SelectedIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCheck" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SOURCE_TYPE_ID" HeaderText="来源id" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SOURCE_TYPE_NAME" HeaderText="来源名称" ItemStyle-HorizontalAlign="Center" />
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
            <asp:CheckBox ID="chkAll" runat="server" Text="全选" AutoPostBack="True" />
            <table style="width: 100%">
                <tr>
                    <td class="auto-style2" style="text-align: center">
                        <asp:Button ID="btntj" runat="server" Text="添加来源" align="center" BorderColor="#FFCCCC" BorderStyle="Double" BackColor="#820000" Font-Bold="True" ForeColor="White" Height="43px" Width="125px" Font-Size="15" />
                    </td>
                    <td class="auto-style1" style="text-align: center">
                        <asp:Button ID="btnDel" runat="server" Text="删除" align="center" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" Font-Bold="True" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

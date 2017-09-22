<%@ Page Language="VB" AutoEventWireup="false" CodeFile="user_review.aspx.vb" Inherits="shenhe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 803px;
        }
        .auto-style6 {
            width: 374px;
        }
        .auto-style7 {
            width: 477px;
        }
        .auto-style8 {
            width: 474px;
        }
        .auto-style9 {
            width: 474px;
            height: 40px;
        }
        .auto-style10 {
            width: 477px;
            height: 40px;
        }
        .auto-style11 {
            width: 374px;
            height: 40px;
        }
        .auto-style12 {
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
                        <asp:Button ID="btnback" runat="server" Text="返回" Height="37px" Width="69px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" Font-Bold="True" Font-Size="15pt" ForeColor="white" /></td>
                </tr>
                <tr>
                    <td>
                        <h1 style="text-align: center">审核名单</h1>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td class="auto-style9" style="text-align: center">
                        <asp:Button ID="btndsh" runat="server" Text="待审核" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                    </td>
                    <td class="auto-style10" style="text-align: center">
                        <asp:Button ID="btntg" runat="server" Text="通过审核" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                    </td>
                    <td class="auto-style11" style="text-align: center">
                        <asp:Button ID="btnjs" runat="server" Text="拒绝申请" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="shmd" runat="server" AutoGenerateColumns="False" Width="700px"
            DataKeyNames="REVIEW_ID" CellPadding="4" ForeColor="#333333" GridLines="None" Style="width: 100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkCheck" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="REVIEW_ID" HeaderText="申请ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="OPEN_ID" HeaderText="用户id" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USER_FULLNAME" HeaderText="用户名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="USER_CODE" HeaderText="用户号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="APPLY_AT" HeaderText="申请日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="REVIEW_AT" HeaderText="加入日期" ItemStyle-HorizontalAlign="Center" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"/>
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
        <br />
        <asp:CheckBox ID="chkAll" runat="server" Text="全选" AutoPostBack="True" />
        <table style="width: 100%">
            <tr>
                <td class="auto-style12" style="text-align: center">
                    <asp:Button ID="btnpass" runat="server" Text="审核通过" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                </td>
                <td class="auto-style12" style="text-align: center">
                    <asp:Button ID="btnjj" runat="server" Text="拒绝" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

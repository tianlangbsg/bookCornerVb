<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_type.aspx.vb" Inherits="book_type" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 126px;
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
                       <h1>图书类别</h1> </asp:Label></td>
                </tr>
            </table>
            <asp:GridView ID="tslb" runat="server" AutoGenerateColumns="False" Width="700px"
                DataKeyNames="BOOK_TYPE_ID" CellPadding="4" ForeColor="#333333" GridLines="None" Style="width: 100%" OnSelectedIndexChanging="tslb_SelectedIndexChanging" OnRowDeleting="tslb_RowDeleting" OnRowEditing="tslb_RowEditing" OnRowCancelingEdit="tslb_RowCancelingEdit">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="BOOK_TYPE_ID" HeaderText="类别ID" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="BOOK_TYPE_NAME" HeaderText="类别名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="BOOK_TYPE_COLOR" HeaderText="类别颜色RGB" ItemStyle-HorizontalAlign="Center" />                  
                    <asp:CommandField SelectText="编辑" ShowSelectButton="True" />
                    <asp:CommandField DeleteText="删除"  ShowDeleteButton ="true" />
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
            <table style="width: 100%; margin: 0 auto; text-align: center;">
                <tr>
                    <td style="font-size: 40px;">
                        <asp:Button ID="btnin" runat="server" Text="添加分类" ForeColor="White" BorderColor="#FFCCCC" BorderStyle="Double" Height="43px" Width="125px" Font-Size="15" BackColor="#820000" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

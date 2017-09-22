<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_insert.aspx.vb" Inherits="book_insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style13 {
            width: 78px;
        }

        .auto-style29 {
            width: 78px;
            height: 25px;
        }

        .auto-style31 {
            width: 190px;
            height: 26px;
        }

        .auto-style32 {
            width: 78px;
            height: 26px;
        }

        .auto-style34 {
            width: 190px;
            height: 25px;
        }

        .auto-style36 {
            width: 190px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="return_button" runat="server" Text="返回" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" />
            <table border="0" style="width: 1000PX; height: 209px;">
                <h1 style="text-align: center">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="图书管理"></asp:Label></h1>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="  条形码："></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:TextBox ID="Tiaoxingma" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="图书名称："></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:TextBox ID="book_Name" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label1" runat="server" Text="图书详情："></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:TextBox ID="centent" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style31" style="text-align: right">图书分类：
                       
                    </td>
                    <td class="auto-style32">
                        <asp:DropDownList ID="book_Type" runat="server" ForeColor="Blue" Width="154px" Height="23px">
                            <asp:ListItem>综合类</asp:ListItem>
                            <asp:ListItem>开发类</asp:ListItem>
                            <asp:ListItem>管理类</asp:ListItem>
                            <asp:ListItem>养生保健类</asp:ListItem>
                            <asp:ListItem>生活家居类</asp:ListItem>
                            <asp:ListItem>过期刊物类</asp:ListItem>
                            <asp:ListItem>时尚小说类</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style31" style="text-align: right">图书来源：                      
                    </td>
                    <td class="auto-style32">
                        <asp:DropDownList ID="bookSource" runat="server" ForeColor="Blue" Width="154px" Height="23px">
                            <asp:ListItem>员工捐赠</asp:ListItem>
                            <asp:ListItem>公司购入</asp:ListItem>
                            <asp:ListItem>工会捐赠</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style34" style="text-align: right">捐赠人ID：
                       
                    </td>
                    <td class="auto-style29">
                        <asp:DropDownList ID="userID" runat="server" ForeColor="Blue" Width="154px" Height="23px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label5" runat="server" Text="上传图片："></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">&nbsp;</td>
                    <td>
                        <asp:Button ID="loadin" runat="server" Text="录入" Width="84px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double" ForeColor="White" /></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label9" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label10" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label11" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="auto-style36" style="text-align: right">
                        <asp:Label ID="Label13" runat="server" Text=""></asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <table style="width: 100%">
            <asp:GridView ID="Book_centent" runat="server" AlternatingRowStyle-HorizontalAlign="left" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="105px" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="BOOK_BARCODE" HeaderText="条形码" />
                    <asp:BoundField DataField="BOOK_NAME" HeaderText="图书名称" />
                    <asp:BoundField DataField="BOOK_INTRO" HeaderText="图书详情" />
                    <asp:BoundField DataField="BOOK_TYPE_ID" HeaderText="图书分类" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="SOURCE_TYPE_ID" HeaderText="图书来源" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="CONTRIBUTOR_ID" HeaderText="捐赠人ID" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="80px" />
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
        </table>
        <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>

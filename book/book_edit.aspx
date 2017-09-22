<%@ Page Language="VB" AutoEventWireup="false" CodeFile="book_edit.aspx.vb" Inherits="book_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <tr>                        <asp:Button ID="Button1" runat="server" Text="返回"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                    </tr>
        <div style="text-align: center">
            <tr>
            <table border="0" style="width: 100%; text-align: left;">
                <tr>
                    <td><h1 style="text-align: center"><asp:Label ID="Label8" runat="server" Font-Bold="True" Text="编辑信息"></asp:Label></h1><br />
                        <div style="text-align: left">
                            <br />
                            <br />
                        </div>
                        <br />
                        图书id ：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbid" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        图书名称：&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbname" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        图书条形码： <asp:TextBox ID="tbma" runat="server"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        图书简介：&nbsp;&nbsp;&nbsp; <asp:TextBox ID="tbtext" runat="server" Height="16px" Width="399px" Style="margin-top: 3px"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        分&nbsp;类：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="148px">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <br />
                        来&nbsp;源：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="DropDownList2" runat="server" Height="16px" Width="148px">
                        </asp:DropDownList>
                        <br />
                        <br />
                        捐&nbsp;赠人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="DropDownList3" runat="server" Height="17px" Width="148px">
                        </asp:DropDownList>
                        <br />
                        <br />
                        （来源仅员工捐赠时有效）<br />
                        <br />
                        <br />
                        
                        <div>
                            图片：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:FileUpload ID="FileUpload1" runat="server" Height="16px" Width="157px" />
                            &nbsp;&nbsp;<br />
                            <br />
                            <asp:Button runat="server" Text="开始上传" ID="UploadButton" OnClick="UploadButton_Click"  Width="90px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"></asp:Button>
                        </div>
                        <div>
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><br />
                        </div>
                        <div style="padding: 10px 0">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="bsave" runat="server" Text="保存" Width="90px" BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                            <br />
                            <br />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

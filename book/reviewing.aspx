<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reviewing.aspx.vb" Inherits="reviewing" %>

<head>
    <style type="text/css">
        .auto-style1 {
            width: 278px;
        }

        .auto-style2 {
            width: 214px;
        }
        .auto-style3 {
            width: 214px;
            height: 23px;
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 214px;
            height: 20px;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <tr>
                                <asp:Button ID="Button2" runat="server" Text="返 回" OnClick="Button2_Click" Width="70px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                    
            </tr>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: center" class="auto-style1" colspan="2">
                        <h1 style="width: 1190px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="等待审核"></asp:Label>
                        </h1>
                    </td>
                </tr>
                <tr>

                    <td class="auto-style3">姓名
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="TextBox1" runat="server" Enabled="False"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <br />
                        工号
                   
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server" Enabled="False"></asp:TextBox></td>

                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td>您的申请正在审核，请稍候。<br />
                    </td>

                </tr>
                <tr>
                    <td class="auto-style2">申请日期
                    
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" Enabled="False"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style5">
                        </td>

                </tr>
            </table>
        </div>
    </form>
</body>


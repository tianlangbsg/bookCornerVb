<%@ Page Language="VB" AutoEventWireup="false" CodeFile="review_confuse.aspx.vb" Inherits="review_confuse" %>

<body>
    <form id="form1" runat="server">
        <div>
            <tr>
                                <asp:Button ID="Button2" runat="server" Text="返 回" OnClick="Button2_Click" Width="70px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                    
            </tr>
            <table>
                <td style="text-align: center" class="auto-style1" colspan="2">
                    <h1 style="width: 1190px">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="申请被拒绝"></asp:Label>
                    </h1>
                </td>
            </table>
            &nbsp;<table style="width: 50%;">
                <tr>
                    <td class="auto-style2">姓&nbsp;&nbsp; 名&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style3">工&nbsp;&nbsp; 号&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:TextBox ID="TextBox2" runat="server" Enabled="False"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">您的申请被拒绝，请重新申请或联系管理员。<br />
                        申请日期&nbsp;&nbsp;&nbsp; 
                    <asp:TextBox ID="TextBox3" runat="server" Enabled="False"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="重新申请" OnClick="Button1_Click" Style="height: 21px"  BackColor="#820000" BorderColor="#FFCCCC" BorderStyle="Double"  ForeColor="White"/>
                        &nbsp;&nbsp;&nbsp;
                    </td>

                </tr>
            </table>
        </div>
    </form>
</body>

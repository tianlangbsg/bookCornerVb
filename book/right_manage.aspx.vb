Imports System.Data.SqlClient

''' <summary>
''' 系统管理员登陆
''' 孙德鑫
''' </summary>
''' <remarks></remarks>
Partial Class right_manage
    Inherits System.Web.UI.Page

    Protected Sub ButDengLu_Click(sender As Object, e As EventArgs) Handles ButDengLu.Click
        Dim bCommon As New BusinessCommon
        Dim Sqlconn As SqlConnection
        Sqlconn = bCommon.GetConnection()
        Sqlconn.Open()
        If TextBox1.Text.Trim = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('用户名不能为空')</script>")
        End If
        If TextBox2.Text.Trim = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('密码不能为空')</script>")
        End If
        Dim tb As String = TextBox1.Text.Trim
        Dim sqlstr As String = " SELECT MANAGER_NAME FROM SYSTEM_MANAGER_INFO WHERE MANAGER_NAME='" & TextBox1.Text.Trim & "'"

        If TextBox1.Text.Trim = bCommon.ExecScalar(sqlstr) Then
            Response.Redirect("user_admin.aspx?")
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('用户名不正确')</script>")
        End If
        Dim sqlstr2 As String = " SELECT MANAGER_PASSWORD FROM SYSTEM_MANAGER_INFO WHERE MANAGER_PASSWORD='" & TextBox2.Text.Trim & "'"
        If TextBox2.Text.Trim = bCommon.ExecScalar(sqlstr2) Then
            Response.Redirect("user_admin.aspx?")
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('密码不正确')</script>")
        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("home_manager.aspx")
    End Sub
End Class
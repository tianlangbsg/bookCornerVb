Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 起始页
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class index
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Protected Sub apply_button_Click(sender As Object, e As EventArgs) Handles apply_button.Click
        '跳转到用户登录页面
        Response.Redirect("log_in.aspx")
    End Sub

    Protected Sub apply_button0_Click(sender As Object, e As EventArgs) Handles apply_button0.Click
        '跳转到管理员页面
        Response.Redirect("home_manager.aspx")
    End Sub
End Class

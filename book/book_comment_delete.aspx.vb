Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 配合USER页面完成评论删除功能
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class book_comment_delete

    Inherits System.Web.UI.Page
    Public book_id As String
    Public open_id As String
    Public user_id As String
    Public user_fullname As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '取得用户的编号
        user_id = Session("user_id")
        '取得用户的全名
        user_fullname = Session("user_fullname")
        '取得用户的openid
        open_id = Session("open_id")
        '取得圖書ID
        book_id = Request("book_id")

        If user_id = "" Then
            Response.Redirect("log_in.aspx")
        Else
            'MsgBox("确认要删除吗?")
            'Dim comment_delete_confirm As String
            Dim SqlserverConn As SqlConnection
            Dim iRow As Integer = 0
            Dim bCommon As New BusinessCommon
            SqlserverConn = bCommon.GetConnection()

            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Try
                '与数据库连接
                myconn = bCommon.GetConnection()
                Dim strSql2 As String
                '删除评论
                strSql2 = "DELETE FROM  BOOK_COMMENT_INFO WHERE COMMENT_ID = " & Request("comment_id")
                myconn.Open()
                dbCommand = New SqlCommand(strSql2, myconn)
                dbCommand.ExecuteNonQuery()

                dbCommand.Dispose()
                myconn.Close()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借書成功！')</script>")

            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借書失敗！')</script>")
                Throw New Exception(ex.Message, ex)
            End Try
            Response.Redirect("user.aspx?mode=" & Request("mode"))
        End If

    End Sub

End Class

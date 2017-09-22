Imports System.Data
Imports System.Data.SqlClient
Partial Class book_return2

    Inherits System.Web.UI.Page

    '定义全局变量book_id
    Public book_id As String
    '定义全局变量open_id
    Public open_id As String
    '定义全局变量user_id
    Public user_id As String
    '定义全局变量user_fullname
    Public user_fullname As String

    ''' <summary>
    ''' 页面加载函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '取得用户的编号
        user_id = Session("user_id")
        '取得用户的全名
        user_fullname = Session("user_fullname")
        '取得用户的openid
        open_id = Session("open_id")
        '取得圖書ID
        book_id = Request("book_id")
        '定义数据库操作工具对象
        Dim bCommon As New BusinessCommon
        Try
            '取得连接
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Try
                '与数据库连接
                myconn = bCommon.GetConnection()
                Dim strSql2 As String
                '還書，写入还书日期，不删除借书记录
                strSql2 = "UPDATE BOOK_BORROW_INFO SET RETURN_DATE= CONVERT(CHAR(8),GETDATE(),112)  WHERE USER_ID=" & user_id & " AND BOOK_ID = " & book_id
                myconn.Open()
                dbCommand = New SqlCommand(strSql2, myconn)
                '执行语句
                dbCommand.ExecuteNonQuery()

                '将book_info中的borrow_flag设置为1
                strSql2 = "UPDATE BOOK_INFO SET BORROW_FLAG='1',UPDATED_AT=GETDATE() WHERE DEL_FLAG = 0 AND BOOK_ID = " & book_id
                'myconn.Open()
                dbCommand = New SqlCommand(strSql2, myconn)
                dbCommand.ExecuteNonQuery()

                dbCommand.Dispose()
                myconn.Close()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('還書成功！')</script>")

            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('還書失败！')</script>")
                Throw New Exception(ex.Message, ex)
            End Try
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
        Response.Redirect("user.aspx")
    End Sub

End Class

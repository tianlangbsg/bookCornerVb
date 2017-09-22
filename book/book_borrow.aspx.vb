Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 配合完成主页的借书操作
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class book_borrow

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
    ''' 页面加载函数，配合完成主页的借书操作
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
        '如果用户的登录状态失效，打回登录页面
        If user_id = "" Then
            Response.Redirect("log_in.aspx")
        Else
            '定义数据库连接对象
            Dim SqlserverConn As SqlConnection
            '定义数据库工具对象bCommon
            Dim bCommon As New BusinessCommon
            '取得数据库库连接
            SqlserverConn = bCommon.GetConnection()
            Dim myconn = bCommon.GetConnection()
            '定义数据库执行器
            Dim dbCommand As New SqlCommand

            '判断是否借书超过上限
            Try
                '定义借书上限
                Dim sql1 As String = "SELECT SET_VALUES FROM SYSTEM_PARMSET_INFO WHERE PARM_ID = 2"
                Dim borrow_limit As Integer = Convert.ToInt32(bCommon.ExecScalar(sql1))
                '定义用户已经借阅的图书数量
                Dim sql2 As String = "SELECT COUNT(*) FROM BOOK_BORROW_INFO WHERE USER_ID =" & user_id &
                                     " AND RETURN_DATE IS NULL"
                Dim already_borrow As Integer = Convert.ToInt32(bCommon.ExecScalar(sql2))
                '判断已借阅数量是否超过上限
                If already_borrow >= borrow_limit Then
                    Session("failure") = "borrow_limit"
                    Response.Redirect("home_book.aspx")
                Else
                    Try
                        '与数据库连接
                        myconn = bCommon.GetConnection()
                        Dim strSql2 As String
                        '插入借书记录
                        strSql2 = "INSERT INTO  BOOK_BORROW_INFO(BOOK_ID,USER_ID,BORROW_DATE,PLAN_RETURN_DATE,RETURN_DATE,CREATED_AT,UPDATED_AT)" &
                                  " VALUES   (" & book_id & "," & user_id & ",CONVERT(CHAR(8),GETDATE(),112),CONVERT(varchar(8)," &
                                  " DATEADD(DAY,+CONVERT(INTEGER,(SELECT SET_VALUES FROM SYSTEM_PARMSET_INFO WHERE PARM_ID = 1)),GETDATE()),112),NULL,GETDATE(),GETDATE())"
                        '打开连接
                        myconn.Open()
                        '执行插入语句
                        dbCommand = New SqlCommand(strSql2, myconn)
                        dbCommand.ExecuteNonQuery()

                        '将book_info中的borrow_flag设置为0
                        strSql2 = "UPDATE BOOK_INFO SET BORROW_FLAG='0',UPDATED_AT=GETDATE() WHERE DEL_FLAG = 0 AND BOOK_ID = " & book_id
                        dbCommand = New SqlCommand(strSql2, myconn)
                        '执行更新操作
                        dbCommand.ExecuteNonQuery()
                        '关闭连接，释放资源
                        dbCommand.Dispose()
                        myconn.Close()
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借書成功！')</script>")
                    Catch ex As Exception
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借書失敗！')</script>")
                        Throw New Exception(ex.Message, ex)
                    End Try
                    '跳转到图书主页
                    Response.Redirect("home_book.aspx")
                End If
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借书操作发生异常，请联系管理员！')</script>")
            End Try
        End If
    End Sub
End Class

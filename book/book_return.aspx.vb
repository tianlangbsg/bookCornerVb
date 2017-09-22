Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 配合USER页面中的还书操作
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class book_return
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
    ''' 页面加赞函数，完成USER页面中的还书操作
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
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Try
            '与数据库连接
            myconn = bCommon.GetConnection()
            myconn.Open()
            Dim strSql2 As String
            '取出图书对应的归还时间
            strSql2 = "SELECT CONVERT(DATE,PLAN_RETURN_DATE) AS PLAN_RETURN_DATE FROM BOOK_BORROW_INFO WHERE RETURN_DATE IS NULL AND BOOK_ID= " & book_id & " AND USER_ID = " & user_id
            '定义SQL执行对象
            Dim myCmd = New SqlCommand(strSql2, myconn)
            myCmd.CommandType = System.Data.CommandType.Text
            Dim dataReader As SqlDataReader
            dataReader = myCmd.ExecuteReader()
            '取得计划归还日期
            While dataReader.Read
                return_date.Text = CStr(dataReader("PLAN_RETURN_DATE")).Replace("/", "-")
            End While
            '释放资源
            dataReader.Close()
            myCmd.Dispose()
            SqlserverConn.Close()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub

    ''' <summary>
    ''' 跳转到用户中心
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Response.Redirect("user.aspx")
    End Sub

    ''' <summary>
    ''' 提交修改计划还书日期事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub apply_button_Click(sender As Object, e As EventArgs) Handles apply_button.Click
        '将显示日期文本框内容进行刷新
        return_date.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd")
        '定义字符串接受日历选择的日期
        Dim plan_return_date As String = Calendar1.SelectedDate.ToString("yyyyMMdd")
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        SqlserverConn = bCommon.GetConnection()
        '取得连接对象
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Try
            '与数据库连接
            myconn = bCommon.GetConnection()
            Dim strSql2 As String
            '更改還書時間
            strSql2 = "UPDATE BOOK_BORROW_INFO SET PLAN_RETURN_DATE='" & plan_return_date & "' WHERE RETURN_DATE IS NULL AND BOOK_ID = " &
                      book_id & " AND USER_ID = " & user_id
            myconn.Open()
            dbCommand = New SqlCommand(strSql2, myconn)
            '执行语句
            dbCommand.ExecuteNonQuery()
            '释放资源
            dbCommand.Dispose()
            myconn.Close()
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改還書時間成功！')</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改還書時間失敗！')</script>")
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub

    ''' <summary>
    ''' 将修改过后的时期实时刷新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        return_date.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd")
    End Sub
End Class

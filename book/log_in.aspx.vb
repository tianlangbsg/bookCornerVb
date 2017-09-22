Imports System.Data
Imports System.Data.SqlClient
Partial Class log_in

    Inherits System.Web.UI.Page

    '定义全局变量book_id
    Public book_id As String
    '定义全局变量open_id
    Public open_id As String
    '定义全局变量user_id
    Public user_id As String
    '定义全局变量user_fullname
    Public user_fullname As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        apply_button.Focus()
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Response.Redirect("home_book.aspx")
    End Sub

    Protected Sub apply_button_Click(sender As Object, e As EventArgs) Handles apply_button.Click
        '定义员工号
        Dim user_code As String = user_code_textbox.Text
        '定义员工全名
        Dim user_fullname As String = user_name_textbox.Text
        '定义数据库连接
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        '定义SQL语句
        Dim strSql As String
        '如果输入的文本包含非法字符
        If user_code.Contains("'") Or user_fullname.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('禁止SQL注入！')</script>")
        Else
            '得到连接对象
            SqlserverConn = bCommon.GetConnection()
            SqlserverConn.Open()
            '查找对应的用户数据
            strSql = "SELECT * FROM USER_INFO WHERE USER_CODE = '" & user_code & "' AND USER_FULLNAME=　'" & user_fullname & "'"
            Dim myCmd = New SqlCommand(strSql, SqlserverConn)
            myCmd.CommandType = System.Data.CommandType.Text
            Dim dataReader As SqlDataReader
            dataReader = myCmd.ExecuteReader()
            '如果督导数据，则进行赋值
            If dataReader.Read Then
                open_id = CStr(dataReader("OPEN_ID"))
                user_id = CStr(dataReader("USER_ID"))
                user_fullname = CStr(dataReader("USER_FULLNAME"))
                '将登陆信息写入到SESSION域对象中
                Session("open_id") = open_id
                Session("user_id") = user_id
                Session("user_fullname") = user_fullname
                Response.Redirect("home_book.aspx")
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('错误的用户编号或姓名！')</script>")
            End If
            '释放资源
            dataReader.Close()
            myCmd.Dispose()
            SqlserverConn.Close()
        End If

    End Sub

    ''' <summary>
    ''' 跳转到申请审核页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Response.Redirect("apply.aspx")
    End Sub

    ''' <summary>
    ''' 跳转到起始页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_button_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("index.aspx")
    End Sub

    Protected Sub user_code_textbox_TextChanged(sender As Object, e As EventArgs) Handles user_code_textbox.TextChanged
        apply_button.Focus()
    End Sub
End Class

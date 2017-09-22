Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 图书评论页面
''' 输入并提交图书评论
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class book_comment

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
        '去的图书ID
        book_id = Request("book_id")
        '根据登录状态，初始化登录按钮和标签
        If user_id = "" Then
            user_name.Text = "游客模式"
            quit.Text = "登录"
        Else
            user_name.Text = user_fullname
        End If
    End Sub

    ''' <summary>
    ''' 返回图书详情页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("book_detail.aspx?book_id=" & book_id)
    End Sub

    ''' <summary>
    ''' 提交评论按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub attach_comment_button_Click(sender As Object, e As EventArgs) Handles attach_comment_button.Click
        'open_id = "ADMIN-1234-EFGH-5678"
        Dim ifAnoyFlag As String
        '判断是否匿名
        If anoy.Checked = True Then
            '匿名
            ifAnoyFlag = "1"
        Else
            '不匿名
            ifAnoyFlag = "0"
        End If
        '判断用户是否登录
        If open_id <> "" Then
            Dim bCommon As New BusinessCommon
            Try
                '取得数据库连接
                Dim myconn = bCommon.GetConnection()
                Dim dbCommand As New SqlCommand
                Try
                    '与数据库连接
                    myconn = bCommon.GetConnection()
                    Dim strSql2 As String
                    '插入评论语句
                    strSql2 = "INSERT INTO  BOOK_COMMENT_INFO(USER_ID,BOOK_ID,COMMENT_GRADE," &
                        "COMMENT,ANONYMOU_FLAG,DEL_FLAG,CREATED_AT,UPDATED_AT)" &
                        " VALUES   (" & user_id & "," & book_id & ",'" &
                        DropDownList_grade.Text & "','" & comment_TextBox1.Text & "','" & ifAnoyFlag & "',0,GETDATE(),GETDATE()) "
                    myconn.Open()
                    dbCommand = New SqlCommand(strSql2, myconn)
                    '执行插入语句
                    dbCommand.ExecuteNonQuery()
                    '释放资源
                    dbCommand.Dispose()
                    myconn.Close()
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('发表书评成功！')</script>")
                    Response.Redirect("book_detail.aspx?book_id=" & book_id) '" open_id=" & open_id)
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('发表书评失败！')</script>")
                    Throw New Exception(ex.Message, ex)
                End Try
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
            End Try
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请先登录后再评论！')</script>")

        End If
    End Sub

    ''' <summary>
    ''' 退出登录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub quit_Click(sender As Object, e As EventArgs) Handles quit.Click
        '退出登录
        Session("user_fullname") = ""
        Session("user_id") = ""
        Session("open_id") = ""
        Response.Redirect("log_in.aspx")
    End Sub

    ''' <summary>
    '''  跳转到用户中心
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("user.aspx")
    End Sub
End Class

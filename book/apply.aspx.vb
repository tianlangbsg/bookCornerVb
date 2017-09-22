Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 用户注册界面
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class apply

    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 页面加载函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    ''' <summary>
    ''' 返回登录页面
    ''' </summary>
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles return_log_in.Click
        Response.Redirect("log_in.aspx")
    End Sub

    ''' <summary>
    ''' 点击申请按钮事件函数
    ''' </summary>
    Protected Sub apply_button_Click(sender As Object, e As EventArgs) Handles apply_button.Click
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        '
        Dim bCommon As New BusinessCommon
        SqlserverConn = bCommon.GetConnection()
        Dim strSql As String
        '定义用户CODE后四位字符串
        Dim user_open_id_last As String
        '定义DJB后面三位字符串
        Dim user_open_id_djb As String
        '定义用户ID
        Dim userCode As Integer
        Try
            userCode = Convert.ToInt32(user_code_textbox.Text.Trim)
            '将usercode转换成4位数字形式
            user_open_id_last = Format(userCode, "0000")
            '将usercode转换成4位数字形式
            user_open_id_djb = Format(userCode, "000")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('员工号限定为1-999的整数！')</script>")
        End Try
        '取得用户名字
        Dim userName As String = user_name_textbox.Text.Trim
        '对两个输入框进行非空判断
        If user_code_textbox.Text.Trim = "" Or userName = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('员工号和用户名不能为空！')</script>")
        ElseIf userCode > 999 Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('员工号限定为1-999的整数！')</script>")
        ElseIf userCode <> 0 And userName <> "" Then
            '根据员工的CODE计算出OPEN_ID
            Dim open_id = "USER-1234-EFGH-" & user_open_id_last
            '查询是否已存在该用户
            strSql = "SELECT * FROM USER_INFO A	WHERE A.USER_CODE = '" & userCode & "' 	AND A.VALID_FLAG = '0'"
            '定义数据集
            Dim ds As New DataSet()
            '定义数据集适配器
            Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
            Try
                sda.Fill(ds, "if_has_user")
                '如果该用户记录不存在
                If ds.Tables(0).Rows.Count = 0 Then
                    '取得数据库连接
                    Dim myconn = bCommon.GetConnection()
                    Dim dbCommand As New SqlCommand
                    Try
                        '与数据库连接
                        myconn = bCommon.GetConnection()
                        Dim strSql2 As String
                        '往待审核表中插入新的用户
                        strSql2 = "SELECT REVIEW_FLAG FROM  USER_REVIEW_INFO WHERE USER_CODE ='DJB" & user_open_id_djb & "' AND USER_FULLNAME='" & userName & "'"
                        '定义当前用户的审核状态
                        Dim review_status As String = bCommon.ExecScalar(strSql2)
                        '0表示未审核
                        If review_status = "0" Then
                            Dim url = "reviewing.aspx?name=" & userName & "&usercode=" & user_open_id_djb & "&date=" & Date.Today.ToString("yyyy-MM-dd")
                            Response.Redirect(url)
                            '2表示审核被拒绝
                        ElseIf review_status = "2" Then
                            Dim url = "review_confuse.aspx?name=" & userName & "&usercode=" & user_open_id_djb & "&date=" & Date.Today.ToString("yyyy-MM-dd")
                            Response.Redirect(url)
                        ElseIf review_status = "" Then
                            '如果不存在相应的申请记录，就往待审核表中插入新的用户
                            strSql2 = "INSERT INTO USER_REVIEW_INFO (OPEN_ID, USER_CODE, USER_FULLNAME, APPLY_AT, REVIEW_FLAG," &
                            "CREATED_AT, UPDATED_AT ) VALUES ('" & open_id & "','DJB" & user_open_id_djb & "', '" &
                            userName & "',CONVERT(CHAR(8),GETDATE(),112), '0', GETDATE(), GETDATE())	 "
                            '打开数据库连接
                            myconn.Open()
                            dbCommand = New SqlCommand(strSql2, myconn)
                            '执行插入语句
                            dbCommand.ExecuteNonQuery()
                            '关闭连接，释放资源
                            dbCommand.Dispose()
                            myconn.Close()
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('申请已提交，等待管理员审核！')</script>")
                        End If
                    Catch ex As Exception
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('注册失败！')</script>")
                        Throw New Exception(ex.Message, ex)
                    End Try
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('该用户已注册！')</script>")
                End If
                '关闭连接，释放资源
                sda.Dispose()
                ds.Dispose()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
            End Try
        End If
    End Sub
End Class

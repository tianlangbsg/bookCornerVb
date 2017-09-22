Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 图书详情页面
''' 显示图书的借阅次数、点赞收藏情况以及图片信息等
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class book_chart

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
        '取得图书的编号
        book_id = Request("book_id")
        '取得用户的编号
        user_id = Session("user_id")
        '取得用户的全名
        user_fullname = Session("user_fullname")
        '取得用户的openid
        open_id = Session("open_id")
        '初始化登陆标签和按钮文本
        If user_id = "" Then
            user_name.Text = "游客模式"
            quit0.Text = "登录"
        Else
            user_name.Text = user_fullname
        End If
        init()
    End Sub

    '抽取出来的页面刷新方法
    Protected Sub init()
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得数据库连接
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        Dim strSql As String
        '根据BOOK_ID取出详细信息
        strSql = "SELECT  A.PICTURE1,A.PICTURE2,A.PICTURE3,A.PICTURE4,A.PICTURE5,A.PICTURE6,A.PICTURE7,A.PICTURE8,A.PICTURE9," &
                "A.BOOK_INTRO,ST.SOURCE_TYPE_NAME,A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                "A.CREATED_AT AS BOOK_CREATED_DATE," &
                "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '0'  ) AS FAVOURITE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                " FROM  BOOK_INFO A " &
                "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " &
                "LEFT JOIN  BOOK_SOURCE_TYPE_INFO ST  ON A.SOURCE_TYPE_ID  = ST.SOURCE_TYPE_ID " &
                " LEFT JOIN BOOK_BORROW_INFO  C ON (A.BOOK_ID=C.BOOK_ID)  WHERE    A.BOOK_ID =  " & book_id &
                "  ORDER BY  BORROW_COUNT DESC"
        '执行查询语句
        Dim myCmd = New SqlCommand(strSql, SqlserverConn)
        myCmd.CommandType = System.Data.CommandType.Text
        Dim dataReader As SqlDataReader
        dataReader = myCmd.ExecuteReader()
        '读取到数据
        While dataReader.Read
            BOOK_NAME.Text = CStr(dataReader("BOOK_NAME"))
            SOURCE_TYPE_NAME.Text = CStr(dataReader("SOURCE_TYPE_NAME"))
            BOOK_CREATED_DATE.Text = CStr(dataReader("BOOK_CREATED_DATE"))
            BORROW_COUNT.Text = CStr(dataReader("BORROW_COUNT"))
            FAVOURITE_COUNT.Text = CStr(dataReader("FAVOURITE_COUNT"))
            PRAISE_COUNT.Text = CStr(dataReader("PRAISE_COUNT"))
            COMMENT_COUNT.Text = CStr(dataReader("COMMENT_COUNT"))
            BORROW_FLAG.Text = CStr(dataReader("BORROW_FLAG"))
            BOOK_INTRO.Text = CStr(dataReader("BOOK_INTRO"))
            '定义图片路径
            Image1.ImageUrl = "images/" + CStr(dataReader("PICTURE1"))
            Image1.Height = "200"
            Image1.Width = "200"
            '图片数目计算
            Dim pic_num As Integer = 0
            For i = 1 To 9
                If dataReader("PICTURE" & i).ToString <> "" Then
                    pic_num = pic_num + 1
                End If
            Next
            pic_num_label.Text = "共有" & pic_num & "张图片"
        End While
        '关闭连接，释放资源
        dataReader.Close()
        myCmd.Dispose()
        SqlserverConn.Close()

        '定义数据连接
        Dim SqlserverConn3 As SqlConnection
        Dim bCommon3 As New BusinessCommon
        '取得连接
        SqlserverConn3 = bCommon3.GetConnection()
        Dim strSql3 As String
        '取得书评语句
        strSql3 = "SELECT CASE BCI.ANONYMOU_FLAG WHEN '1' THEN '匿名用户' WHEN '0' THEN UI.USER_FULLNAME end USER_FULLNAME,BCI.COMMENT_GRADE,BCI.COMMENT,BCI.CREATED_AT FROM BOOK_COMMENT_INFO BCI " &
                  " LEFT JOIN  USER_INFO UI  ON BCI.USER_ID = UI.USER_ID WHERE   BCI.DEL_FLAG = '0'  AND  BCI.BOOK_ID = " & book_id
        Dim ds3 As New DataSet()
        Dim sda3 As New SqlDataAdapter(strSql3, SqlserverConn3)
        Try
            sda3.Fill(ds3, "BOOK_COMMENT")
            '若读取到数据
            If ds3.Tables(0).Rows.Count > 0 Then
                result.Text = ""
                comment_GridView2.DataSource = ds3
                comment_GridView2.DataBind()
            Else
                '未读取到数据
                result.Text = "暂无评论，再刷新试试"
                comment_GridView2.DataSource = Nothing
                comment_GridView2.DataBind()
            End If
            '释放资源
            sda3.Dispose()
            ds3.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try

        '初始化收藏标签
        favourite_button.Text = "收藏"
        '初始化点赞标签
        praise_button.Text = "点赞"
        '判断用户是否已经登录，若登录，则进行收藏和点赞状态查询
        If user_id <> "" Then
            '判断用户是否收藏本书，并且初始化收藏按钮
            strSql = "SELECT * FROM BOOK_FAVORITE_INFO  WHERE DEL_FLAG = '0' AND BOOK_ID = " & book_id & " AND USER_ID =" & user_id
            Dim ds5 As New DataSet()
            Dim sda5 As New SqlDataAdapter(strSql, SqlserverConn)
            Try
                sda5.Fill(ds5, "BOOK_HAS_FAVOURITE")
                If ds5.Tables(0).Rows.Count = 1 Then
                    favourite_button.Text = "已收藏"
                End If
            Catch sse As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('读取收藏状态出错！')</script>")
            End Try

            '判断用户是否点赞本书，并且初始化点赞按钮
            strSql = "SELECT * FROM BOOK_PRAISE_INFO  WHERE  BOOK_ID = " & book_id & " AND USER_ID =" & user_id
            Dim ds6 As New DataSet()
            Dim sda6 As New SqlDataAdapter(strSql, SqlserverConn)
            Try
                sda6.Fill(ds6, "BOOK_HAS_PRAISE")
                If ds6.Tables(0).Rows.Count = 1 Then
                    praise_button.Text = "已点赞"
                End If
            Catch sse As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('读取点赞状态出错！')</script>")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 借书按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub borrow_button_Click(sender As Object, e As EventArgs) Handles borrow_button.Click
        If Session("user_id") = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请登录后再操作！')</script>")
        Else
            If BORROW_FLAG.Text = "待返" Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('本书暂不可借阅！')</script>")
            Else
                Try
                    '定义数据库工具对象
                    Dim bCommon As New BusinessCommon
                    '定义借书上限
                    Dim sql1 As String = "SELECT SET_VALUES FROM SYSTEM_PARMSET_INFO WHERE PARM_ID = 2"
                    Dim borrow_limit As Integer = Convert.ToInt32(bCommon.ExecScalar(sql1))
                    '定义用户已经借阅的图书数量
                    Dim sql2 As String = "SELECT COUNT(*) FROM BOOK_BORROW_INFO WHERE USER_ID =" & user_id &
                                         " AND RETURN_DATE IS NULL"
                    Dim already_borrow As Integer = Convert.ToInt32(bCommon.ExecScalar(sql2))
                    '判断已借阅数量是否超过上限
                    If already_borrow >= borrow_limit Then
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借阅数量超过上限！')</script>")
                    Else
                        Response.Redirect("book_borrow2.aspx?book_id=" & book_id)
                    End If
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('操作发生异常，请联系管理员！')</script>")
                End Try
            End If
        End If
    End Sub
    ''' <summary>
    ''' 点赞按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub favourite_button_Click(sender As Object, e As EventArgs) Handles favourite_button.Click
        '判断用户是否登录
        If user_id = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请登录后再操作！')</script>")
        Else
            '进行收藏操作
            If favourite_button.Text = "收藏" Then
                Dim SqlserverConn As SqlConnection
                Dim iRow As Integer = 0
                Dim bCommon As New BusinessCommon
                SqlserverConn = bCommon.GetConnection()
                Dim strSql As String
                '收藏查询语句
                strSql = "SELECT * FROM BOOK_FAVORITE_INFO  WHERE BOOK_ID = " & book_id & " AND USER_ID =" & user_id

                Dim ds As New DataSet()
                Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
                Try
                    sda.Fill(ds, "BOOK_FAVORITE")
                    ' 如果没有找到相关收藏记录
                    If ds.Tables(0).Rows.Count = 0 Then
                        Dim myconn = bCommon.GetConnection()
                        Dim dbCommand As New SqlCommand
                        Try
                            '与数据库连接
                            myconn = bCommon.GetConnection()
                            Dim strSql2 As String
                            strSql2 = "INSERT INTO  BOOK_FAVORITE_INFO(BOOK_ID,USER_ID,DEL_FLAG,CREATED_AT,UPDATED_AT) VALUES(" &
                                      book_id & "," & user_id & ",'0',GETDATE(),GETDATE())"
                            '打开连接
                            myconn.Open()
                            dbCommand = New SqlCommand(strSql2, myconn)
                            '执行插入操作
                            dbCommand.ExecuteNonQuery()
                            '释放资源
                            dbCommand.Dispose()
                            myconn.Close()
                            'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('收藏成功！')</script>")
                        Catch ex As Exception
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('收藏失败！')</script>")
                            Throw New Exception(ex.Message, ex)
                        End Try
                    Else
                        '取得连接对象
                        Dim myconn = bCommon.GetConnection()
                        Dim dbCommand As New SqlCommand
                        Try
                            '与数据库连接
                            myconn = bCommon.GetConnection()
                            Dim strSql2 As String
                            '更新表内原有的收藏记录，进行收藏操作
                            strSql2 = "UPDATE BOOK_FAVORITE_INFO SET DEL_FLAG='0',UPDATED_AT=GETDATE() WHERE DEL_FLAG = 1 AND BOOK_ID = " &
                                book_id & " AND USER_ID =" & user_id
                            '打开连接
                            myconn.Open()
                            dbCommand = New SqlCommand(strSql2, myconn)
                            '执行操作
                            dbCommand.ExecuteNonQuery()
                            dbCommand.Dispose()
                            myconn.Close()
                            'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('收藏成功！')</script>")
                            init()
                        Catch ex As Exception
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('收藏失败！')</script>")
                            Throw New Exception(ex.Message, ex)
                        End Try
                    End If
                    '释放资源
                    sda.Dispose()
                    ds.Dispose()
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
                End Try
                init()
            ElseIf favourite_button.Text = "已收藏" Then
                '取消收藏操作
                Dim bCommon As New BusinessCommon
                Dim myconn = bCommon.GetConnection()
                Dim dbCommand As New SqlCommand
                Try
                    '与数据库连接
                    myconn = bCommon.GetConnection()
                    Dim strSql2 As String
                    '更新表内原有的收藏记录，进行取消收藏操作
                    strSql2 = "UPDATE BOOK_FAVORITE_INFO SET DEL_FLAG='1',UPDATED_AT=GETDATE() WHERE DEL_FLAG = 0 AND BOOK_ID = " &
                        book_id & " AND USER_ID =" & user_id
                    myconn.Open()
                    dbCommand = New SqlCommand(strSql2, myconn)
                    dbCommand.ExecuteNonQuery()
                    dbCommand.Dispose()
                    myconn.Close()
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('取消收藏成功！')</script>")
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('取消收藏失败！')</script>")
                    Throw New Exception(ex.Message, ex)
                End Try
                init()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 点赞操作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub praise_button_Click(sender As Object, e As EventArgs) Handles praise_button.Click
        '判断用户是否登录
        If user_id = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请登录后再操作！')</script>")
        Else
            '进行点赞操作
            If praise_button.Text = "点赞" Then
                Dim SqlserverConn As SqlConnection
                Dim iRow As Integer = 0
                Dim bCommon As New BusinessCommon
                SqlserverConn = bCommon.GetConnection()
                '定义SQL语句
                Dim strSql As String
                '查找点赞记录
                strSql = "SELECT * FROM BOOK_PRAISE_INFO  WHERE BOOK_ID = " & book_id & " AND USER_ID =" & user_id
                '定义数据集
                Dim ds As New DataSet()
                Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
                Try
                    sda.Fill(ds, "BOOK_PRAISE")
                    ' 如果没找到相关点赞记录
                    If ds.Tables(0).Rows.Count = 0 Then
                        Dim myconn = bCommon.GetConnection()
                        Dim dbCommand As New SqlCommand
                        Try
                            '与数据库连接
                            myconn = bCommon.GetConnection()
                            Dim strSql2 As String
                            '插入点赞记录
                            strSql2 = "INSERT INTO  BOOK_PRAISE_INFO(BOOK_ID,USER_ID,CREATED_AT,UPDATED_AT) VALUES(" &
                                book_id & "," & user_id & ",GETDATE(),GETDATE())"
                            myconn.Open()
                            dbCommand = New SqlCommand(strSql2, myconn)
                            '执行操作
                            dbCommand.ExecuteNonQuery()
                            '释放资源
                            dbCommand.Dispose()
                            myconn.Close()
                            'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('点赞成功！')</script>")
                        Catch ex As Exception
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('点赞失败！')</script>")
                            Throw New Exception(ex.Message, ex)
                        End Try
                    End If
                    '释放资源
                    sda.Dispose()
                    ds.Dispose()
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
                End Try
                init()
            ElseIf praise_button.Text = "已点赞" Then
                '取消点赞操作
                Dim bCommon As New BusinessCommon
                Dim myconn = bCommon.GetConnection()
                Dim dbCommand As New SqlCommand
                Try
                    '与数据库连接
                    myconn = bCommon.GetConnection()
                    Dim strSql2 As String
                    '删除点赞记录
                    strSql2 = "DELETE FROM BOOK_PRAISE_INFO WHERE BOOK_ID = " &
                        book_id & " AND USER_ID = 1"
                    '打开数据库连接
                    myconn.Open()
                    dbCommand = New SqlCommand(strSql2, myconn)
                    '执行操作
                    dbCommand.ExecuteNonQuery()
                    '释放资源
                    dbCommand.Dispose()
                    myconn.Close()
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('取消点赞成功！')</script>")
                Catch ex As Exception
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('取消点赞失败！')</script>")
                    Throw New Exception(ex.Message, ex)
                End Try
                '初始化页面
                init()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 评论按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub comment_button_Click(sender As Object, e As EventArgs) Handles comment_button.Click
        '发表书评页面
        If user_id <> "" Then
            Response.Redirect("book_comment.aspx?book_id=" & book_id)
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请登录后再评论！')</script>")
        End If
    End Sub

    ''' <summary>
    ''' 跳转带图片详情
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub more_pic_button_Click(sender As Object, e As EventArgs) Handles more_pic_button.Click
        Response.Redirect("book_pic_list.aspx?book_id=" & book_id)
    End Sub

    ''' <summary>
    ''' 退出按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub quit0_Click(sender As Object, e As EventArgs) Handles quit0.Click
        '退出登录
        Session("user_fullname") = ""
        Session("user_id") = ""
        Session("open_id") = ""
        Response.Redirect("log_in.aspx")
    End Sub

    ''' <summary>
    ''' 用户中心按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles user_center.Click
        '跳转到用户中心
        Response.Redirect("user.aspx")
    End Sub

    ''' <summary>
    ''' 对连续的英文评论进行自动换行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView2_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles comment_GridView2.RowCreated
        comment_GridView2.Style.Add("word-break", "break-all")
        comment_GridView2.Style.Add("word-wrap", "break-word")
    End Sub

    ''' <summary>
    ''' 返回主页
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_button_Click(sender As Object, e As EventArgs) Handles return_button.Click
        '返回主页
        Response.Redirect("home_book.aspx")
    End Sub
End Class

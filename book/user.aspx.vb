Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 用户中心，包含借还书，书评，点赞收藏等等功能
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class user
    Inherits System.Web.UI.Page

    '定义全局共用变量
    '定义全局变量book_id
    Public book_id As String
    '定义全局变量open_id
    Public open_id As String
    '定义全局变量user_id
    Public user_id As String
    '定义全局变量user_fullname
    Public user_fullname As String
    '定义全局变量mode
    Public mode As String

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
        '取得返回模式
        mode = Request("mode")
        '判断用户登录状态，进行登录按钮和标签的初始化操作
        If user_id = "" Then
            user_name.Text = "游客模式"
            quit.Text = "登录"
        Else
            user_name.Text = user_fullname
        End If
        '判断用户是否登录，若未登录，返回登录页面
        If user_id = "" Then
            Response.Redirect("log_in.aspx")
            'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请先登录！')</script>")
        Else
            '定义数据库连接对象
            Dim SqlserverConn As SqlConnection
            Dim bCommon As New BusinessCommon
            '取得连接对象
            SqlserverConn = bCommon.GetConnection()
            Dim strSql As String
            '查找用户的收藏点赞借书还书书评总数
            strSql = "SELECT  " &
                     "(SELECT COUNT(*) FROM  BOOK_BORROW_INFO B LEFT JOIN  BOOK_INFO I  ON I.BOOK_ID = B.BOOK_ID WHERE I.DEL_FLAG='0'  AND B.USER_ID = " & user_id & ") AS BORROW_COUNT, " &
                     "(SELECT COUNT(*) FROM  BOOK_BORROW_INFO C LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = C.BOOK_ID  WHERE A.DEL_FLAG = '0' AND C.USER_ID = " & user_id & " AND C.RETURN_DATE IS NULL) AS STAYSTILL_COUNT, " &
                     "(SELECT COUNT(*) FROM  BOOK_FAVORITE_INFO D LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = D.BOOK_ID  WHERE A.DEL_FLAG = '0' AND D.DEL_FLAG = '0' AND D.USER_ID = " & user_id & " ) AS FAVOURITE_COUNT, " &
                     "(SELECT COUNT(*) FROM  BOOK_PRAISE_INFO E  LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = E.BOOK_ID  WHERE A.DEL_FLAG = '0'  AND E.USER_ID = " & user_id & ") AS PRAISE_COUNT, " &
                     "(SELECT COUNT(*) FROM  BOOK_COMMENT_INFO F LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = F.BOOK_ID  WHERE A.DEL_FLAG = '0' AND F.USER_ID = " & user_id & ") AS COMMENT_COUNT "
            '定义数据集
            Dim ds As New DataSet()
            Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
            Try
                sda.Fill(ds, "USER_INFO")
                '如果取到了数据
                If ds.Tables(0).Rows.Count > 0 Then
                    Label1.Text = ""
                    GridView_user_info.DataSource = ds
                    GridView_user_info.DataBind()
                Else
                    '未取到数据
                    Label1.Text = "暂无记录。"
                    GridView_user_info.DataSource = Nothing
                    GridView_user_info.DataBind()
                End If
                '释放资源
                sda.Dispose()
                ds.Dispose()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
            End Try
            '根据返回的模式数字，进行相应的页面展示
            borrow_button_Click(sender, e)
            If mode = "2" Then
                favourite_button_Click(sender, e)
            ElseIf mode = "3" Then
                praise_button_Click(sender, e)
            ElseIf mode = "4" Then
                comment_button_Click(sender, e)
            End If
        End If
        '借书数量超过上限返回的提示信息
        Dim borrow_limit_warning As String = Session("failure")
        If borrow_limit_warning = "borrow_limit" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借书数量超过上限，请先还书后再来！')</script>")
            Session("failure") = ""
        End If
    End Sub

    ''' <summary>
    ''' 我的借阅详情
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub borrow_button_Click(sender As Object, e As EventArgs) Handles borrow_button.Click
        '将本按钮对应的gridview设为可见，其他设为不可见
        borrowandreturn.Visible = True
        myfavourite.Visible = False
        mypraise.Visible = False
        mycomment.Visible = False
        '定义数据库对象
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        '取得数据库连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '查找我的借阅详情
        strSql = "SELECT A.BOOK_ID,B.BOOK_NAME,CONVERT(DATE,A.BORROW_DATE) AS BORROW_DATE,CONVERT(DATE,A.RETURN_DATE) AS RETURN_DATE,CONVERT(DATE,A.PLAN_RETURN_DATE) AS PLAN_RETURN_DATE,case A.RETURN_DATE when  NULL then '待返'  else '可借'  end BORROW_FLAG," &
                "case when A.RETURN_DATE is null then '還書' else '' end BORROW," &
                "case when A.RETURN_DATE is null then '修改還書時間' else '' end CHANGE_RETURN_DATE " &
                " FROM  BOOK_BORROW_INFO A " &
                "LEFT JOIN  BOOK_INFO B  ON A.BOOK_ID = B.BOOK_ID WHERE B.DEL_FLAG='0' AND A.USER_ID = " & user_id &
                "  ORDER BY  BORROW_DATE DESC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '如果取到了数据
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                borrowandreturn.DataSource = ds
                borrowandreturn.DataBind()
            Else
                '未取到数据
                Label1.Text = "暂无记录。"
                borrowandreturn.DataSource = Nothing
                borrowandreturn.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 我的收藏
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub favourite_button_Click(sender As Object, e As EventArgs) Handles favourite_button.Click
        '将本按钮对应的gridview设为可见，其他设为不可见
        borrowandreturn.Visible = False
        myfavourite.Visible = True
        mypraise.Visible = False
        mycomment.Visible = False
        '定义数据库对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接对象
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '查询我的收藏语句
        strSql = "SELECT A.BOOK_ID,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID," &
                "A.CREATED_AT AS BOOK_CREATED_DATE," &
                "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = ''  ) AS FAVOURITE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                " FROM  BOOK_FAVORITE_INFO D " &
                "LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = D.BOOK_ID  WHERE A.DEL_FLAG = '0'" &
                " AND D.DEL_FLAG = '0' AND D.USER_ID =  " & user_id &
                "  ORDER BY D.BOOK_ID ASC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '取得数据以及数据绑定
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                myfavourite.DataSource = ds
                myfavourite.DataBind()
            Else
                Label1.Text = "暂无记录。"
                myfavourite.DataSource = Nothing
                myfavourite.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 我的点赞
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub praise_button_Click(sender As Object, e As EventArgs) Handles praise_button.Click
        '将本按钮对应的gridview设为可见，其他设为不可见
        borrowandreturn.Visible = False
        myfavourite.Visible = False
        mypraise.Visible = True
        mycomment.Visible = False
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得数据库连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '我的点赞详情
        strSql = "SELECT A.BOOK_ID,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID," &
                "A.CREATED_AT AS BOOK_CREATED_DATE," &
                "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                " FROM  BOOK_PRAISE_INFO E " &
                "LEFT JOIN  BOOK_INFO A  ON A.BOOK_ID = E.BOOK_ID  WHERE A.DEL_FLAG = '0'" &
                " AND E.USER_ID =  " & user_id &
                "  ORDER BY E.BOOK_ID ASC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '取得数据以及数据绑定
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                mypraise.DataSource = ds
                mypraise.DataBind()
            Else
                Label1.Text = "暂无记录。"
                myfavourite.DataSource = Nothing
                myfavourite.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 我的评论
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub comment_button_Click(sender As Object, e As EventArgs) Handles comment_button.Click
        '将本按钮对应的gridview设为可见，其他设为不可见
        borrowandreturn.Visible = False
        myfavourite.Visible = False
        mypraise.Visible = False
        mycomment.Visible = True
        Label1.Text = ""

        '定义数据库连接对象
        Dim SqlserverConn3 As SqlConnection
        Dim bCommon3 As New BusinessCommon
        '得到数据库连接
        SqlserverConn3 = bCommon3.GetConnection()
        Dim strSql3 As String
        '查询出我的评论详情
        strSql3 = "SELECT BI.BOOK_NAME,BI.BOOK_ID,BCI.COMMENT_GRADE,CASE BCI.ANONYMOU_FLAG WHEN '0' THEN '不匿名' ELSE '匿名' END ANONYMOU_FLAG,BCI.COMMENT,BCI.COMMENT_ID,BCI.CREATED_AT,'刪除' as OPERATION FROM BOOK_COMMENT_INFO BCI " &
            " LEFT JOIN  BOOK_INFO BI ON BCI.BOOK_ID = BI.BOOK_ID WHERE  BI.DEL_FLAG = '0'  AND  BCI.USER_ID = " & user_id
        '定义数据集
        Dim ds3 As New DataSet()
        Dim sda3 As New SqlDataAdapter(strSql3, SqlserverConn3)
        Try
            sda3.Fill(ds3, "BOOK_COMMENT")
            '取得数据并进行数据绑定
            If ds3.Tables(0).Rows.Count > 0 Then
                mycomment.DataSource = ds3
                mycomment.DataBind()
            Else
                mycomment.DataSource = Nothing
                mycomment.DataBind()
                Label1.Text = "暂无记录。"
            End If
            '释放资源
            sda3.Dispose()
            ds3.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles mypraise.SelectedIndexChanging, mycomment.SelectedIndexChanging
    End Sub

    ''' <summary>
    ''' 返回主页
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Dim from_page = Request("from")
        'If from_page = "book_detail" Then
        '    Dim mode = Request("mode")
        '    book_id = Request("book_id")
        '    Response.Redirect("book_detail.aspx?from=user&book_id=" & book_id & "& mode=" & mode)
        '    '返回主页
        'Else
        Response.Redirect("home_book.aspx")
        'End If

    End Sub

    ''' <summary>
    ''' 退出登录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub quit_Click(sender As Object, e As EventArgs) Handles quit.Click
        Session("user_fullname") = ""
        Session("user_id") = ""
        Session("open_id") = ""
        Response.Redirect("log_in.aspx")
    End Sub

    '该方法已过时，弃之不用
    Protected Sub borrowandreturn_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles borrowandreturn.SelectedIndexChanging
        book_id = GridView_user_info.DataKeys(e.NewSelectedIndex).Value.ToString()
        'GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString()
        'book_id = GridView1.Rows(e.NewSelectedIndex).Cells(0).ToString
        Dim bCommon As New BusinessCommon
        Try
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Try
                '与数据库连接
                myconn = bCommon.GetConnection()
                Dim strSql2 As String
                '還書
                strSql2 = "DELETE FROM BOOK_BORROW_INFO WHERE USER_ID=" & user_id & " AND BOOK_ID = " & book_id
                myconn.Open()
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
    End Sub

    ''' <summary>
    ''' 对连续的英文评论进行自动换行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub mycomment_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles mycomment.RowCreated
        mycomment.Style.Add("word-break", "break-all")
        mycomment.Style.Add("word-wrap", "break-word")
    End Sub
End Class

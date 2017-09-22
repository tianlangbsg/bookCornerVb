Imports System.Data
Imports BusinessCommon
Imports System.Data.SqlClient

''' <summary>
''' 作者：陈强
''' 网页：图书详情页面
''' 内容：主要是浏览图书的详细信息。包括书名、书号、借阅次数、点赞次数等等。
''' </summary>
''' <remarks></remarks>
Partial Class book_detail_manage
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入页面调用pageload（）方法是该页面能够刷新
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        pageload()
    End Sub

    ''' <summary>
    ''' 点击编辑按钮跳转到编辑图书页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub write_Click(sender As Object, e As EventArgs) Handles write.Click
        Response.Redirect("book_edit.aspx?BOOK_ID=" & Request("bookid"))
    End Sub

    ''' <summary>
    ''' 点击借阅历史按钮跳转到借阅历史页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ago_Click(sender As Object, e As EventArgs) Handles ago.Click
        Response.Redirect("book_history.aspx?BOOK_ID=" & Request("bookid"))
    End Sub

    ''' <summary>
    ''' 点击下架/上架按钮实现该图书的图书状态的变化   1.当该按钮为上架时，点击会使该书变为上架中的图书  1.当该按钮为下架时，点击会使该书变为已下架的图书
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub godown_Click(sender As Object, e As EventArgs) Handles godown.Click
        '获取BusinessCommon的对象
        Dim bCommon As New BusinessCommon
        '获取上个页面传过来的bookid
        Dim bookid = Request("bookid")
        Dim common As BusinessCommon = New BusinessCommon
        Dim ds As New DataSet()
        'sql语句的拼写
        Dim strSql As String
        strSql = "SELECT DEL_FLAG FROM BOOK_INFO WHERE BOOK_ID= " & bookid
        Dim flag As String
        flag = bCommon.ExecScalar(strSql)
        If flag = "1" Then
            '定义flag变量，当flag为1时即该书已下架 将该书变为上架
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Try
                '与数据库连接
                Dim sqlStr1 As String = "UPDATE BOOK_INFO SET DEL_FLAG=0 WHERE BOOK_ID= " & bookid
                myconn.Open()
                dbCommand = New SqlCommand(sqlStr1, myconn)
                dbCommand.ExecuteNonQuery()
                dbCommand.Dispose()
                myconn.Close()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改图书状态成功。')</script>")
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改图书状态失败。')</script>")
                Throw New Exception(ex.Message, ex)
            End Try
        Else
            '定义flag变量，当flag为0时即该书上架中 将该书变为下架
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Try
                '与数据库连接
                Dim sqlStr2 As String = "UPDATE BOOK_INFO SET DEL_FLAG=1 WHERE BOOK_ID= " & bookid
                myconn.Open()
                dbCommand = New SqlCommand(sqlStr2, myconn)
                dbCommand.ExecuteNonQuery()
                dbCommand.Dispose()
                myconn.Close()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改图书状态成功。')</script>")
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改图书状态失败。')</script>")
                Throw New Exception(ex.Message, ex)
            End Try

        End If
        pageload()
    End Sub

    ''' <summary>
    ''' pageload函数就是数据加载的页面，只不过写成一个方法是为了能在点击上架/下架时刷新页面
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub pageload()
        '获取BusinessCommon的对象
        Dim bCommon As New BusinessCommon
        '获取上个页面传过来的bookid
        Dim bookid = Request("bookid")
        Dim common As BusinessCommon = New BusinessCommon
        Dim data As book_infoData
        data = common.getdata3(bookid)
        '每个label对应相应的值
        Me.Source.Text = data.source_type_name
        Me.IntoTime.Text = data.book_created_date
        Me.Borrows.Text = data.borrow_count
        Me.Collect.Text = data.favorite_count
        Me.Likes.Text = data.praise
        Me.Book_Review.Text = data.comment_count
        Me.intro.Text = data.detail
        Me.pictrues.Text = data.imgCount
        img.ImageUrl = data.imgUrl
        Dim iRow As Integer = 0
        Dim SqlserverConn2 As SqlConnection
        Dim iRow2 As Integer = 0
        Dim bCommon2 As New BusinessCommon
        SqlserverConn2 = bCommon2.GetConnection()
        Dim strSql2 As String
        Dim strSql3 As String
        '查询传入书的id
        strSql3 = "SELECT DEL_FLAG FROM BOOK_INFO WHERE BOOK_ID= " & bookid
        '查询用户的工号，和对此书评论，评分等有关信息
        strSql2 = "SELECT  UI.USER_FULLNAME,BCI.COMMENT_GRADE,BCI.COMMENT,BCI.CREATED_AT FROM BOOK_COMMENT_INFO BCI " &
            " LEFT JOIN  USER_INFO UI  ON BCI.USER_ID = UI.USER_ID WHERE   BCI.DEL_FLAG = '0'  AND  BCI.BOOK_ID = " & bookid
        '判断该图书的图书状态，如果为1则按钮显示上架，为0则按钮显示下架
        Dim flag As String
        flag = bCommon.ExecScalar(strSql3)
        If flag = "1" Then
            godown.Text = "上架"
        Else
            godown.Text = "下架"
        End If
        Dim ds2 As New DataSet()
        Dim sda2 As New SqlDataAdapter(strSql2, SqlserverConn2)
        Try
            '填充数据，有则填充，无则提示
            sda2.Fill(ds2, "BOOK_COMMENT")
            If ds2.Tables(0).Rows.Count > 0 Then
                Hint.Text = ""
                book_comment.DataSource = ds2
                book_comment.DataBind()
            Else
                Hint.Text = "没有符合条件的数据!"
                book_comment.DataSource = Nothing
                book_comment.DataBind()
            End If
            sda2.Dispose()
            ds2.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 点击返回按钮返回到图书管理界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_Button_Click(sender As Object, e As EventArgs) Handles return_Button.Click
        Response.Redirect("book_manager_list.aspx")
    End Sub

    ''' <summary>
    ''' 评论为全字母时换行
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView2_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles book_comment.RowCreated
        book_comment.Style.Add("word-break", "break-all")
        book_comment.Style.Add("word-wrap", "break-word")
    End Sub
End Class

Imports System.Data.SqlClient
Imports System.Data

''' <summary>
''' 作者：陈强
''' 网页：借阅记录查询页面
''' 内容：主要是完成对用户借阅图书记录的查询
''' </summary>
''' <remarks></remarks>
Partial Class book_search
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入页面显示所有图书的借阅信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '数据库连接
        Dim SqlDbConn As SqlConnection
        Dim iRow As Integer = 0
        '建立连接对象
        Dim bCommon As New BusinessCommon
        SqlDbConn = bCommon.GetConnection()
        Dim strSql As String
        'sql语句
        strSql = "SELECT A.BOOK_NAME, U.USER_CODE ,B.BORROW_DATE ,B.PLAN_RETURN_DATE,B.RETURN_DATE ,case  when RETURN_DATE is NULL then '待还' else  '已还' end KK FROM BOOK_INFO A ,USER_INFO U,BOOK_BORROW_INFO B  WHERE A.BOOK_ID = B.BOOK_ID AND B.USER_ID =U.USER_ID select * from BOOK_BORROW_INFO  WHERE RETURN_DATE is null"
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlDbConn)
        '抛出异常
        Try
            '填充数据，有则填充
            sda.Fill(ds, "BOOK_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                message.Text = ""
                book_centent.DataSource = ds
                book_centent.DataBind()
            Else
                message.Text = "没有符合条件的数据!"
                book_centent.DataSource = Nothing
                book_centent.DataBind()
            End If
        Catch ex As Exception
        End Try
        sda.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 点击查询按钮会按借阅的开始日期、结束日期以及结束状态联合查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchBtn_Click(sender As Object, e As EventArgs) Handles searchBtn.Click
        '防止恶意sql注入
        If Star_Day.Text.Contains("'") Or End_Day.Text.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('禁止sql注入')</script>")
        Else
            '数据库连接
            Dim SqlDbConn As SqlConnection
            Dim iRow As Integer = 0
            '建立连接对象
            Dim bCommon As New BusinessCommon
            SqlDbConn = bCommon.GetConnection()
            '定义变量 开始日期、结束日期
            Dim starday As String
            Dim endday As String
            Dim sss As String
            '通过四个变量将现在的日期改为八位的日期格式
            Dim date1 As String
            Dim date2() As String
            Dim date3 As String
            '提取当前时间
            date1 = Date.Now()
            '取前十位,定义数组接收由“/”分开的日期
            date2 = Split(date1.Substring(0, 10), "/")
            '将数组里的值拼接成一个八位的日期
            date3 = date2(0) + date2(1) + date2(2)
            sss = book_borrow_flag.Text
            starday = Star_Day.Text
            endday = End_Day.Text
            '判断日期
            Dim strSql As String
            If Not "".Equals(starday) Then
                If Not "".Equals(endday) Then
                    If starday < endday Then
                        If starday < date3 Then
                            '如果开始和结束日期都不为空的话，开始日期需在今天以及结束日期以前
                            strSql = "SELECT A.BOOK_NAME, U.USER_CODE ,B.BORROW_DATE ,B.PLAN_RETURN_DATE,B.RETURN_DATE ,case  when RETURN_DATE is NULL then '待还' else  '已还' end KK" &
                                     " FROM BOOK_INFO A ,USER_INFO U,BOOK_BORROW_INFO B WHERE A.BOOK_ID = B.BOOK_ID AND B.USER_ID =U.USER_ID and BORROW_DATE >= " & starday & " and RETURN_DATE  <= " & endday & " "
                        Else
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('开始日期需在今天以前')</script>")
                        End If
                    Else
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('开始日期需在结束日期以前')</script>")
                    End If
                Else
                    '如果开始日期不为空且结束日期为空的话，则查找未还书的用户借书信息
                    strSql = "SELECT A.BOOK_NAME, U.USER_CODE ,B.BORROW_DATE ,B.PLAN_RETURN_DATE,B.RETURN_DATE ,case  when RETURN_DATE is NULL then '待还' else  '已还' end KK" &
                             " FROM BOOK_INFO A ,USER_INFO U,BOOK_BORROW_INFO B WHERE A.BOOK_ID = B.BOOK_ID AND B.USER_ID =U.USER_ID and BORROW_DATE >= " & starday
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请输入开始日期')</script>")
            End If
            '拼接sql语句
            If sss = "全部" Then
                strSql = strSql + ""
            End If
            If sss = "已还" Then
                strSql = strSql + "AND  RETURN_DATE is NOT null"
            End If
            If sss = "待还" Then
                strSql = strSql + "AND  RETURN_DATE is null"
            End If
            Dim ds As New DataSet()
            Dim sda As New SqlDataAdapter(strSql, SqlDbConn)
            '抛出异常
            Try
                '填充数据，有则填充，无则提示
                sda.Fill(ds, "BOOK_INFO")
                If ds.Tables(0).Rows.Count > 0 Then
                    message.Text = ""
                    book_centent.DataSource = ds
                    book_centent.DataBind()
                Else
                    message.Text = "没有符合条件的数据！"
                    book_centent.DataSource = Nothing
                    book_centent.DataBind()
                End If
            Catch ex As Exception
            End Try
            sda.Dispose()
            ds.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' 点击返回按钮跳转到管理中心页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub returnbb_Click(sender As Object, e As EventArgs) Handles returnbb.Click
        Response.Redirect("home_manager.aspx")
    End Sub

    ''' <summary>
    ''' 加入日期控件，并在页面载入时将其隐藏
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        Star_Day.Text = Calendar1.SelectedDate.ToString("yyyyMMdd")
        Calendar1.Visible = False
    End Sub

    ''' <summary>
    ''' 点击图片按钮显示日期控件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Calendar1.Visible = True
        '点击按扭后显示日期控件
    End Sub


    ''' <summary>
    ''' 加入日期控件，并在页面载入时将其隐藏
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Calendar2_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar2.SelectionChanged
        End_Day.Text = Calendar2.SelectedDate.ToString("yyyyMMdd")
        Calendar2.Visible = False
    End Sub

    ''' <summary>
    ''' 点击图片按钮显示日期控件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        Calendar2.Visible = True
        '点击按扭后显示日期控件
    End Sub
End Class
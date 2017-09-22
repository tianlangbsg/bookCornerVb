Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 图书排行榜页面
''' 包含了上月排行榜，借阅收藏以及人气排行榜
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
        '取得用户的编号
        user_id = Session("user_id")
        '取得用户的全名
        user_fullname = Session("user_fullname")
        '取得用户的openid
        open_id = Session("open_id")
        '判断是否登录，将登陆按钮显示初始化
        If user_id = "" Then
            user_name.Text = "游客模式"
            quit0.Text = "登录"
        Else
            user_name.Text = user_fullname
        End If
        '定义数据库连接
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        SqlserverConn = bCommon.GetConnection()
        '定义数据库查询语句
        Dim strSql As String
        '上月排行榜
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " &
                 " LEFT JOIN BOOK_BORROW_INFO  C ON (A.BOOK_ID=C.BOOK_ID)  WHERE   A.DEL_FLAG = '0' AND  CONVERT(varchar(6),C.BORROW_DATE,112)=CONVERT(varchar(6),DATEADD(MONTH,-1,GETDATE()),112)" &
                 "  ORDER BY  BORROW_COUNT DESC"
        '定义数据集合
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            '将读取到的数据存入到集合中
            sda.Fill(ds, "BOOK_INFO")
            '如果读取到的的数据不为空
            If ds.Tables(0).Rows.Count > 0 Then
                '将结果显示标签内容清空
                result_Label1.Text = ""
                '绑定数据源到gridview
                chart_gridview.DataSource = ds
                chart_gridview.DataBind()
            Else
                '未读取到数据
                result_Label1.Text = "暂无数据。"
                chart_gridview.DataSource = Nothing
                chart_gridview.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try

    End Sub

    ''' <summary>
    ''' 上月排行榜按钮事件桉树
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub last_month_chart_Click(sender As Object, e As EventArgs) Handles last_month_chart.Click
        '定义数据库连接
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '上月排行榜
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " &
                 " LEFT JOIN BOOK_BORROW_INFO  C ON (A.BOOK_ID=C.BOOK_ID)  WHERE   A.DEL_FLAG = '0' AND  CONVERT(varchar(6),C.BORROW_DATE,112)=CONVERT(varchar(6),DATEADD(MONTH,-1,GETDATE()),112)" &
                 "  ORDER BY  BORROW_COUNT DESC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            '绑定数据源到控件
            sda.Fill(ds, "lastmonth")
            If ds.Tables(0).Rows.Count > 0 Then
                '若读取到数据，进行数据绑定和标签初始化
                result_Label1.Text = ""
                chart_gridview.DataSource = ds
                chart_gridview.DataBind()
            Else
                '未读取到数据，进行数据绑定和标签初始化
                result_Label1.Text = "暂无数据。"
                chart_gridview.DataSource = Nothing
                chart_gridview.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 借阅排行榜按钮事件函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub borrow_chart_Click(sender As Object, e As EventArgs) Handles borrow_chart.Click
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '借阅排行榜
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                  "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID WHERE   A.DEL_FLAG = '0'" &
                 "  ORDER BY  BORROW_COUNT DESC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '若读取到数据
            If ds.Tables(0).Rows.Count > 0 Then
                result_Label1.Text = ""
                chart_gridview.DataSource = ds
                chart_gridview.DataBind()

            Else
                '未读取到数据
                result_Label1.Text = "暂无数据。"
                chart_gridview.DataSource = Nothing
                chart_gridview.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 按钮跳转事件,跳转到图书详情页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim book_id As String
        book_id = chart_gridview.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("book_detail.aspx?book_id=" & book_id)
    End Sub

    ''' <summary>
    ''' 收藏排行榜
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub favourite_chart_Click(sender As Object, e As EventArgs) Handles favourite_chart.Click
        '定义连接对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String
        '收藏排行榜
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID WHERE   A.DEL_FLAG = '0'" &
                 "  ORDER BY  FAVOURITE_COUNT DESC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '若查询到数据
            If ds.Tables(0).Rows.Count > 0 Then
                result_Label1.Text = ""
                chart_gridview.DataSource = ds
                chart_gridview.DataBind()
            Else
                '未查询到数据
                result_Label1.Text = "暂无数据。"
                chart_gridview.DataSource = Nothing
                chart_gridview.DataBind()

            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 人气排行榜按钮事件
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub hot_chart_Click(sender As Object, e As EventArgs) Handles hot_chart.Click
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        '定义SQL语句
        Dim strSql As String

        '人气排行榜
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                " FROM  BOOK_INFO A " &
                "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID WHERE   A.DEL_FLAG = '0'" &
                "  ORDER BY  PRAISE_COUNT DESC"
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            '若查询掉数据
            If ds.Tables(0).Rows.Count > 0 Then
                result_Label1.Text = ""
                chart_gridview.DataSource = ds
                chart_gridview.DataBind()
            Else
                '未查询到数据
                result_Label1.Text = "暂无数据。"
                chart_gridview.DataSource = Nothing
                chart_gridview.DataBind()
            End If
            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 返回图书主页
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("home_book.aspx")
    End Sub

    ''' <summary>
    ''' 退出登录
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub quit0_Click(sender As Object, e As EventArgs) Handles quit0.Click
        Session("user_fullname") = ""
        Session("user_id") = ""
        Session("open_id") = ""
        Response.Redirect("log_in.aspx")
    End Sub

    ''' <summary>
    ''' 跳转到用户中心页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles user_center.Click
        Response.Redirect("user.aspx")
    End Sub
End Class

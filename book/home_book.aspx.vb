Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 图书书架主页
''' 显示图书，可按条件查询和书名模糊查询，在登录状态下可以借书
''' 王康
''' </summary>
''' <remarks></remarks>
Partial Class searchPage
    Inherits System.Web.UI.Page

    '定义全局变量book_id
    Public book_id As String
    '定义全局变量open_id
    Public open_id As String
    '定义全局变量user_id
    Public user_id As String
    '定义全局变量user_fullname
    Public user_fullname As String
    '定义全局变量color_list
    Public color_list As ArrayList

    ''' <summary>
    ''' 页面加载函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bCommon As New BusinessCommon
        '取得用户的编号
        user_id = Session("user_id")
        '取得用户的全名
        user_fullname = Session("user_fullname")
        '取得用户的openid
        open_id = Session("open_id")
        '根据用户登录状态，初始化登录按钮和标签
        If user_id = "" Then
            user_name.Text = "游客模式"
            quit.Text = "登录"
        Else
            user_name.Text = user_fullname
        End If
        '如果不是本页回跳
        If IsPostBack = False Then
            '初始化下拉框
            Dim arrList As New ArrayList()
            arrList = bCommon.getBookTypeInfo()
            DropDownList3.DataSource = arrList
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "")
        End If
        '定义数据库连接
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        '取得数据库连接
        SqlserverConn = bCommon.GetConnection()
        Dim strSql As String
        '初始化查询
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 1 then '借書' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " &
                 " WHERE   A.DEL_FLAG = '0'" &
                 " ORDER BY  BORROW_COUNT DESC , BOOK_CREATED_DATE DESC"

        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")
            ' 判断是否取得数据
            If ds.Tables(0).Rows.Count > 0 Then
                '取得数据
                Label1.Text = ""
                book_shelf_GridView1.DataSource = ds
                book_shelf_GridView1.DataBind()

            Else
                '未取得数据
                Label1.Text = "未找到符合条件项目。"
                book_shelf_GridView1.DataSource = Nothing
                book_shelf_GridView1.DataBind()
            End If
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR") <> "" Then
                    'color_list.Add(ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                    book_shelf_GridView1.Rows(i).BackColor = System.Drawing.Color.FromName("#" & ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                End If
            Next

            '释放资源
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
        '借书数量超过上限返回的提示信息
        Dim borrow_limit_warning As String = Session("failure")
        If borrow_limit_warning = "borrow_limit" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('借书数量超过上限，请先还书后再来！')</script>")
            'warning_flag.Attributes.Add("limit", "true")
            'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>function limit_warning(){alert('借书数量超过上限，请先还书后再来！')}</script>")
            Session("failure") = ""
        End If
        'For i = 0 To ds.Tables("BOOK_TYPE_COLOR").Rows.Count
        '    If ds.Tables("BOOK_TYPE_COLOR").Rows(i).ToString <> "" Then
        '        color_list.Add(ds.Tables("BOOK_TYPE_COLOR").Rows(i).ToString)
        '        book_shelf_GridView1.Rows(i).BackColor = System.Drawing.Color.FromName("#" & ds.Tables("BOOK_TYPE_COLOR").Rows(i).ToString)
        '    End If
        'Next

    End Sub

    ''' <summary>
    ''' 条件查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchBtn_Click(sender As Object, e As EventArgs) Handles searchBtn.Click
        '定义数据库连接对象
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        Dim strSql As String
        '定义借阅和录入时间的FLAG
        Dim borrowTimes As Integer = DropDownList1.SelectedIndex
        Dim createdTime As Integer = DropDownList2.SelectedIndex
        Dim borrowTimesSc As String
        Dim createdTimeSc As String

        '借阅次数
        '1表示降序
        If borrowTimes = 1 Then
            borrowTimesSc = " ORDER BY  BORROW_COUNT DESC"
            '2表示升序
        ElseIf borrowTimes = 2 Then
            borrowTimesSc = " ORDER BY  BORROW_COUNT ASC"
        Else
            borrowTimesSc = ""
        End If

        If borrowTimes <> 0 Then
            '录入时间
            '1表示降序
            If createdTime = 1 Then
                createdTimeSc = "  ,BOOK_CREATED_DATE DESC "
                '2表示升序
            ElseIf createdTime = 2 Then
                createdTimeSc = "  ,BOOK_CREATED_DATE ASC"
            Else
                createdTimeSc = ""
            End If
        Else
            '录入时间
            '1表示降序
            If createdTime = 1 Then
                createdTimeSc = "ORDER BY BOOK_CREATED_DATE DESC "
                '2表示升序
            ElseIf createdTime = 2 Then
                createdTimeSc = "ORDER BY BOOK_CREATED_DATE ASC"
            Else
                createdTimeSc = ""
            End If
        End If

        Dim bookType As String = ""
        '判断是否选择了图书类型，进行SQL拼装
        If Not DropDownList3.SelectedValue = "" Then
            Dim type() As String
            type = DropDownList3.SelectedValue.Split(":")
            bookType = " AND  A.BOOK_TYPE_ID=" & type(0)
        End If
        '根据录入时间，借阅次数和图书类型的选择，拼装SQL语句
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                  "A.CREATED_AT AS BOOK_CREATED_DATE," &
                  "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                  "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                  "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                  " (SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                  "case A.BORROW_FLAG when 0 then '待返' when 1 then '可借' else '' end BORROW_FLAG ," &
                  "case A.BORROW_FLAG when 0 then '' when 1 then '借書' else '' end BORROW " &
                  " FROM  BOOK_INFO A " &
                  "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID WHERE   A.DEL_FLAG = '0' " &
                  bookType & borrowTimesSc & createdTimeSc
        '定义数据集
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            sda.Fill(ds, "BOOK_INFO")

            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                '定义数据源
                book_shelf_GridView1.DataSource = ds
                '绑定数据
                book_shelf_GridView1.DataBind()
            Else
                Label1.Text = "未找到符合条件项目。"
                book_shelf_GridView1.DataSource = Nothing
                book_shelf_GridView1.DataBind()
            End If
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR") <> "" Then
                    'color_list.Add(ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                    book_shelf_GridView1.Rows(i).BackColor = System.Drawing.Color.FromName("#" & ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                End If
            Next

            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try

    End Sub

    ''' <summary>
    ''' 跳转到图书详情页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView1_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles book_shelf_GridView1.SelectedIndexChanging
        book_id = book_shelf_GridView1.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("book_detail.aspx?book_id=" & book_id)
    End Sub

    Protected Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    ''' <summary>
    ''' 名字条形码查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchBtn0_Click(sender As Object, e As EventArgs) Handles searchBtn0.Click
        If TextBox1.Text.Trim.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('禁止SQL注入')</script>")
        Else
            '定义数据库连接对象
            Dim SqlserverConn As SqlConnection
            Dim bCommon As New BusinessCommon
            '取得连接
            SqlserverConn = bCommon.GetConnection()
            Dim strSql As String

            '根据条形码或书名进行全局模糊查询
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
                     " AND (BOOK_NAME like '%" & TextBox1.Text.Trim & "%' or BOOK_BARCODE like '%" &
                     TextBox1.Text.Trim & "%') ORDER BY  BORROW_COUNT DESC , BOOK_CREATED_DATE DESC"
            '定义数据集
            Dim ds As New DataSet()
            Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
            Try
                sda.Fill(ds, "BOOK_INFO")
                '判断是否取得数据
                If ds.Tables(0).Rows.Count > 0 Then
                    Label1.Text = ""
                    book_shelf_GridView1.DataSource = ds
                    book_shelf_GridView1.DataBind()

                Else
                    '没有取到数据
                    Label1.Text = "未找到符合条件项目。"
                    book_shelf_GridView1.DataSource = Nothing
                    book_shelf_GridView1.DataBind()
                End If
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR") <> "" Then
                        'color_list.Add(ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                        book_shelf_GridView1.Rows(i).BackColor = System.Drawing.Color.FromName("#" & ds.Tables(0).Rows(i).Item("BOOK_TYPE_COLOR"))
                    End If
                Next

                '释放资源
                sda.Dispose()
                ds.Dispose()
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 跳转到排行榜页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub book_chart_button_Click(sender As Object, e As EventArgs) Handles book_chart_button.Click
        '跳转到排行榜
        Response.Redirect("book_chart.aspx")
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

    ''' <summary>
    ''' 跳转到用户中心
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles user_center.Click
        Response.Redirect("user.aspx")
    End Sub

End Class

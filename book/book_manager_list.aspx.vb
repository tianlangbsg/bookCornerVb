Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 作者：陈强
''' 网页：图书管理页面
''' 内容：主要是完成对图书的查找，可分为按书名、条形码或按照借阅时间、是否上架、录入图书、图书类别联合查询。
''' </summary>
''' <remarks></remarks>
Partial Class book_manager_list
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 分条件对图书的查询并显示所用图书
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bCommon As New BusinessCommon
        If IsPostBack = False Then
            Dim arrList As New ArrayList()
            arrList = bCommon.getBookTypeInfo()
            '从数据库里调用数据到下拉框里
            booksty.DataSource = arrList
            booksty.DataBind()
            booksty.Items.Insert(0, "")
        End If
        '建立数据库连接
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim strSql As String
        SqlserverConn = bCommon.GetConnection()
        'sql语句的拼写，实现联合查询
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,case A.DEL_FLAG when 0 then '上架' when 1 then '下架' else '' end DEL_FLAG1,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                "A.CREATED_AT AS BOOK_CREATED_DATE," &
                "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                "(SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                "case A.BORROW_FLAG when 0 then '待还' when 1 then '可借' else '' end BORROW_FLAG, " &
                "case A.BORROW_FLAG when 1 then '借书' else '' end BORROW " &
                " FROM  BOOK_INFO A " &
                "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " &
                " ORDER BY  BORROW_COUNT DESC , BOOK_CREATED_DATE DESC"
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        '对异常的处理
        Try
            '填充数据，有则填充，无则提示
            sda.Fill(ds, "BOOK_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                AllBook.DataSource = ds
                AllBook.DataBind()
            Else
                Label1.Text = "没有符合条件的数据"
                AllBook.DataSource = Nothing
                AllBook.DataBind()
            End If
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 点击查询按钮会按所选的条件查询图书
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchBtn_Click(sender As Object, e As EventArgs) Handles searchBtn.Click
        '建立数据库连接
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        Dim strSql As String
        SqlserverConn = bCommon.GetConnection()
        '去各个下拉框的数据
        Dim borrowTimes As Integer = BorrowTime.SelectedIndex
        Dim createdTime As Integer = InsertTime.SelectedIndex
        Dim updown As Integer = Upbooks.SelectedIndex
        Dim borrowTimesSc As String
        Dim createdTimeSc As String
        Dim updownSc As String
        Dim check111 As String
        '拼sql语句
        '当复选框被选择 或者 图书状态不为'0' 或者 图书类型里有值是才加where
        Dim WHERE As String
        If SeeBorrow.Checked = True Or updown <> 0 Or booksty.SelectedValue <> "" Then
            WHERE = " WHERE "
        Else
            WHERE = " "
        End If
        '判断sql语句中间是否得用and连接
        Dim AND1 As String
        If SeeBorrow.Checked = True And updown <> 0 Then
            AND1 = " AND "
        Else
            AND1 = " "
        End If
        Dim AND2 As String
        If (updown <> 0 Or SeeBorrow.Checked = True) And booksty.SelectedValue <> "" Then
            AND2 = " AND "
        Else
            AND2 = " "
        End If
        If SeeBorrow.Checked = True Then
            check111 = "A.BORROW_FLAG='0'"
        Else
            check111 = " "
        End If
        '图书状态 0表全部 1表上架 2表下架
        If updown = 0 Then
            updownSc = ""
        ElseIf updown = 1 Then
            updownSc = " A.DEL_FLAG='0' "
        ElseIf updown = 2 Then
            updownSc = " A.DEL_FLAG='1' "
        End If
        '借阅次数 0表示不排序 1表示降序 2表示升序
        If borrowTimes = 1 Then
            borrowTimesSc = " ORDER BY  BORROW_COUNT DESC"
        ElseIf borrowTimes = 2 Then
            borrowTimesSc = " ORDER BY  BORROW_COUNT ASC"
        Else
            borrowTimesSc = " "
        End If
        '录入时间 0表示不排序 1表示降序 2表示升序
        If borrowTimes <> 0 Then
            If createdTime = 1 Then
                createdTimeSc = "  , BOOK_CREATED_DATE DESC "
            ElseIf createdTime = 2 Then
                createdTimeSc = "  , BOOK_CREATED_DATE ASC"
            Else
                createdTimeSc = " "
            End If
        Else
            If createdTime = 1 Then
                createdTimeSc = "ORDER BY BOOK_CREATED_DATE DESC "
            ElseIf createdTime = 2 Then
                createdTimeSc = "ORDER BY BOOK_CREATED_DATE ASC"
            Else
                createdTimeSc = " "
            End If
        End If
        Dim bookType As String = ""
        '调出图书类型号
        If Not booksty.SelectedValue = "" Then
            Dim type() As String
            type = booksty.SelectedValue.Split(":")
            bookType = "  A.BOOK_TYPE_ID=" & type(0)
        End If
        '拼sql语句
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,case A.DEL_FLAG when 0 then '上架' when 1 then '下架' else '' end DEL_FLAG1,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "A.CREATED_AT AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待还' when 1 then '可借' else '' end BORROW_FLAG ," &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借书' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID " & WHERE & updownSc & AND1 & check111 & AND2 &
                   bookType & borrowTimesSc & createdTimeSc
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        '捕捉异常
        Try
            '填充数据，有则填充，无则提示
            sda.Fill(ds, "BOOK_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                AllBook.DataSource = ds
                AllBook.DataBind()
            Else
                Label1.Text = "没有符合条件的数据"
                AllBook.DataSource = Nothing
                AllBook.DataBind()
            End If
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try

    End Sub

    ''' <summary>
    ''' 点击编辑带值跳转到修改图书页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub AllBook_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim BOOK_ID As String
        '跳到编集界面 并且用BOOK_ID去传参数
        Dim ee As System.Web.UI.WebControls.GridViewSelectEventArgs
        ee = e
        '跳转到选中的行，第1列对应的页面去
        BOOK_ID = AllBook.Rows(ee.NewSelectedIndex).Cells(0).Text
        Response.Redirect("book_edit.aspx?BOOK_ID=" & BOOK_ID)
    End Sub

    ''' <summary>
    ''' 点击查询按钮根据文本框里的书名或者条形码进行模糊查询
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchBtn0_Click(sender As Object, e As EventArgs) Handles searchBtn0.Click
        '建立数据库连接
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim strSql As String
        Dim bCommon As New BusinessCommon
        SqlserverConn = bCommon.GetConnection()
        '拼接sql语句
        strSql = "SELECT A.BOOK_ID,B.BOOK_TYPE_NAME,case A.DEL_FLAG when 0 then '上架' when 1 then '下架' else '' end DEL_FLAG1,A.BOOK_BARCODE,A.BOOK_NAME,A.BOOK_TYPE_ID,B.BOOK_TYPE_COLOR," &
                 "CONVERT(CHAR(8),A.CREATED_AT,112) AS BOOK_CREATED_DATE," &
                 "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
                 "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1'  ) AS FAVOURITE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT, " &
                 "(SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0' ) AS COMMENT_COUNT," &
                 "case A.BORROW_FLAG when 0 then '待?' when 1 then '可借' else '' end BORROW_FLAG, " &
                 "case A.BORROW_FLAG when 0 then '' when 1 then '借?' else '' end BORROW " &
                 " FROM  BOOK_INFO A " &
                 "LEFT JOIN  BOOK_TYPE_INFO B  ON A.BOOK_TYPE_ID = B.BOOK_TYPE_ID WHERE " &
                 " (BOOK_NAME like '%" & NameSearch.Text.Trim & "%' or BOOK_BARCODE like '%" &
                 NameSearch.Text.Trim & "%') ORDER BY  BORROW_COUNT DESC , BOOK_CREATED_DATE DESC"
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlserverConn)
        Try
            '填充数据，有则填充，无则提示
            sda.Fill(ds, "BOOK_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                Label1.Text = ""
                AllBook.DataSource = ds
                AllBook.DataBind()
            Else
                Label1.Text = "没有符合条件的数据!"
                AllBook.DataSource = Nothing
                AllBook.DataBind()
            End If
            sda.Dispose()
            ds.Dispose()
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('出错！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 点击按钮跳转到插入图书信息页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub InsertBooks_Click(sender As Object, e As EventArgs) Handles InsertBooks.Click
        Response.Redirect("book_insert.aspx")
    End Sub

    ''' <summary>
    ''' 点击按钮跳转到管理中心页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_button_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("home_manager.aspx")
    End Sub

End Class

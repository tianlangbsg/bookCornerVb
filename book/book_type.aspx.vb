Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：图书类别
''' 内容：查看所有图书类别，可实现添加、修改、删除功能
Partial Class book_type
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入界面，调bind方法
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            bind()
        End If
    End Sub

    ''' <summary>
    '''  编辑方法
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tslb_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        tslb.EditIndex = e.NewEditIndex
        bind()
    End Sub

    ''' <summary>
    ''' 编辑
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tslb_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        tslb.EditIndex = -1
        bind()
    End Sub

    ''' <summary>
    ''' 删除方法
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tslb_RowDeleting(ByVal sender As Object, e As GridViewDeleteEventArgs)
        Dim bCommon As New BusinessCommon
        Dim SqlserverConn As SqlConnection
        '与数据库连接
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        Dim iRow As Integer = 0
        Dim sqlStr As String
        Dim sqlStr1 As String
        Dim myCmd As SqlCommand
        Dim myCmd1 As SqlCommand
        '把图书列表和类别表中，同时存在的类别的DEL_FLAG改成1
        sqlStr1 = "update BOOK_TYPE_INFO set DEL_FLAG='1' from BOOK_INFO,BOOK_TYPE_INFO where BOOK_INFO.BOOK_TYPE_ID=BOOK_TYPE_INFO.BOOK_TYPE_ID"
        myCmd1 = New SqlCommand(sqlStr1, SqlserverConn)
        myCmd1.ExecuteNonQuery()
        Dim arrList As New ArrayList()
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        SqlserverConn = bCommon.GetConnection()
        Dim strSql As String
        '查询改行ID对应的DEL_FLAG
        strSql = "select DEL_FLAG from BOOK_TYPE_INFO where BOOK_TYPE_ID='" & tslb.DataKeys(e.RowIndex).Value.ToString() & "'"
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = System.Data.CommandType.Text
        SqlserverConn.Open()
        '对数据库进行查询并得到结果。ExecuteReader 返回一个DataReader对象
        dataReader = dbCommand.ExecuteReader()
        'SDataReader.Read的每次调用都会从结果集中返回一行。
        dataReader.Read()
        '将查询到的DEL_FLAG赋值给i
        Dim i As String = dataReader("DEL_FLAG")
        dataReader.Close()
        '如果DEL_FLAG=1，弹出对话框
        If i = 1 Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('存在该类别图书，不能删除。')</script>")
            '如果DEL_FLAG=0，可以删除该类别，执行delete语句
        Else
            sqlStr = "delete BOOK_TYPE_INFO where DEL_FLAG='0' and BOOK_TYPE_ID='" & tslb.DataKeys(e.RowIndex).Value.ToString() & "'"
            myCmd = New SqlCommand(sqlStr, SqlserverConn)
            myCmd.ExecuteNonQuery()
        End If
        dbCommand.Dispose()
        SqlserverConn.Close()
        '再検索
        Call bind()
    End Sub

    ''' <summary>
    ''' 检索图书类别表中的所有信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub bind()
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        strSql = "select * from BOOK_TYPE_INFO"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "BOOK_TYPE_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            tslb.DataSource = ds
            tslb.DataBind()
            '将颜色值赋值给表格背景色
            Dim arrList As New ArrayList()
            arrList = getcolor()
            For i = 0 To tslb.Rows.Count - 1
                tslb.Rows(i).Cells(2).BackColor = System.Drawing.Color.FromName("#" & arrList(i))
            Next
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 读取颜色方法
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getcolor() As ArrayList
        Dim arrList As New ArrayList()
        Dim SqlDbConn As SqlConnection
        Dim bCommon As New BusinessCommon
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim strSql As String
        '查询颜色
        strSql = "select BOOK_TYPE_COLOR from BOOK_TYPE_INFO ORDER BY BOOK_TYPE_ID"
        dbCommand = New SqlCommand(strSql, SqlDbConn)
        dbCommand.CommandType = System.Data.CommandType.Text
        dataReader = dbCommand.ExecuteReader()
        While dataReader.Read
            arrList.Add(dataReader("BOOK_TYPE_COLOR"))
        End While
        dataReader.Close()
        dbCommand.Dispose()
        SqlDbConn.Close()
        getcolor = arrList
    End Function

    ''' <summary>
    '''  '添加类别按钮，跳转到类别添加界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnin_Click(sender As Object, e As EventArgs) Handles btnin.Click
        Response.Redirect("book_type_in.aspx")
    End Sub

    ''' <summary>
    ''' '返回到用户管理界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("home_manager.aspx")
    End Sub

    ''' <summary>
    ''' '“编辑”超链接，跳转到编辑界面，将type_id参数传给编辑界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tslb_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim typeid As String
        typeid = tslb.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("book_type_up.aspx?BOOK_TYPE_ID=" & typeid)
    End Sub
End Class


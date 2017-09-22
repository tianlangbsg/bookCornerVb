Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：图书来源界面
''' 内容：查看图书的所有来源，可实现添加、修改、删除功能
Partial Class book_resource
    Inherits System.Web.UI.Page
    '载入界面，查看所有来源
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Call Bind()
        End If
    End Sub

    ''' <summary>
    ''' 添加来源，跳转到添加界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btntj_Click(sender As Object, e As EventArgs) Handles btntj.Click
        Response.Redirect("book_resource_in.aspx")
    End Sub

    ''' <summary>
    ''' 编辑超链接，传给编辑界面一个参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tsly_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim typeid As String
        typeid = tsly.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("book_resource_up.aspx?SOURCE_TYPE_ID=" & typeid)
    End Sub

    ''' <summary>
    ''' 删除按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDel_Click(sender As Object, e As EventArgs) Handles btnDel.Click
        Dim list As ArrayList = New ArrayList
        Dim chk As CheckBox
        For Each item As GridViewRow In tsly.Rows
            If item.RowType <> DataControlRowType.Header Then
                chk = item.FindControl("chkCheck")
                If chk.Checked Then
                    Dim str As String = item.Cells(1).Text
                    list.Add(str)
                End If
            End If
        Next
        If (list.Count > 0) Then
            Delete2(list)
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请选择要删除的数据')</script>")
        End If
    End Sub

    ''' <summary>
    '''全选按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer
        Dim chk As CheckBox
        For i = 0 To Me.tsly.Rows.Count - 1
            chk = CType(Me.tsly.Rows(i).FindControl("chkCheck"), CheckBox)
            'chk = Me.GridView1.Rows(i).FindControl("chkCheck")
            If (Me.chkAll.Checked) Then
                chk.Checked = True
            Else
                chk.Checked = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' 删除方法
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub Delete2(ByVal lst As ArrayList)
        '连接数据库
        Dim SqlDbConn As SqlConnection
        Dim bCommon As New BusinessCommon
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim strSql As String
        '将数据库中所有来源的DEL_FLAG改成1（1无效）
        strSql = "update BOOK_SOURCE_TYPE_INFO set DEL_FLAG=1 from BOOK_SOURCE_TYPE_INFO"
        dbCommand = New SqlCommand(strSql, SqlDbConn)
        dbCommand.ExecuteNonQuery()
        '将数据库中来源中有书的数据的DEL_FLAG改成0（0有效）
        strSql = "update BOOK_SOURCE_TYPE_INFO set DEL_FLAG=0 from BOOK_INFO,BOOK_SOURCE_TYPE_INFO where BOOK_INFO.SOURCE_TYPE_ID = BOOK_SOURCE_TYPE_INFO.SOURCE_TYPE_ID"
        dbCommand = New SqlCommand(strSql, SqlDbConn)
        dbCommand.ExecuteNonQuery()
        For i = 0 To lst.Count - 1
            '查询该来源的DEL_FLAG的值
            strSql = "select DEL_FLAG from BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_ID = " & lst(i).ToString()
            dbCommand = New SqlCommand(strSql, SqlDbConn)
            dbCommand.CommandType = System.Data.CommandType.Text
            dataReader = dbCommand.ExecuteReader()
            dataReader.Read()
            Dim ii As String = dataReader("DEL_FLAG")
            dataReader.Close()
            '判定DEL_FLAG的值，
            If ii = 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('存在该来源的书籍，无法删除！')</script>")
            Else
                strSql = "delete BOOK_SOURCE_TYPE_INFO where DEL_FLAG=1 and SOURCE_TYPE_ID = " & lst(i).ToString()
                dbCommand = New SqlCommand(strSql, SqlDbConn)
                dbCommand.ExecuteNonQuery()
            End If
        Next
        dbCommand.Dispose()
        SqlDbConn.Close()
        '再查询
        Call Bind()
    End Sub

    ''' <summary>
    '''  查询所有来源
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Bind()
        '连接数据库
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        '建立连接对象
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        'sql语句
        Dim strSql As String
        strSql = "select SOURCE_TYPE_ID,SOURCE_TYPE_NAME from BOOK_SOURCE_TYPE_INFO"
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, OleDbConn)
        sda.Fill(ds, "BOOK_SOURCE_TYPE_INFO")
        tsly.DataSource = ds
        tsly.DataBind()
        sda.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 返回按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("home_manager.aspx")
    End Sub
End Class
'

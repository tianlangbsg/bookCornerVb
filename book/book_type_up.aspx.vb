Imports System.Data
Imports System.Data.SqlClient

''' 作者：单瑶
''' 界面：图书类别编辑界面
''' 内容：可编辑已选的图书类别
Partial Class lbbj
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '载入界面，获取参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bCommon As New BusinessCommon
        If IsPostBack = False Then
            '获取type界面传来的id参数
            Dim typeid As String = Request.QueryString("BOOK_TYPE_ID")
            setData(typeid)
            Session("typeid") = typeid
        End If
    End Sub

    ''' <summary>
    ''' '数据读取方法
    ''' </summary>
    ''' <param name="no"></param>
    ''' <remarks></remarks>
    Protected Sub setData(ByVal no As String)
        Dim arrList As New ArrayList()
        Dim bCommon As New BusinessCommon
        '与数据库连接
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        '查询该id对应的类别信息
        Dim strSql As String = "select * from BOOK_TYPE_INFO where BOOK_TYPE_ID = " & no
        dbCommand = New SqlCommand(strSql, myconn)
        dbCommand.CommandType = System.Data.CommandType.Text
        myconn.Open()
        dataReader = dbCommand.ExecuteReader()
        '将查询到的颜色和姓名值显示到输入框中
        While (dataReader.Read())
            Me.boxcolor.Text = dataReader("BOOK_TYPE_COLOR").ToString()
            Me.boxname.Text = dataReader("BOOK_TYPE_NAME").ToString()
        End While
    End Sub

    ''' <summary>
    ''' '取消按钮，跳转到图书分类界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        Response.Redirect("book_type.aspx")
    End Sub
    
    ''' <summary>
    ''' '添加按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnin_Click(sender As Object, e As EventArgs) Handles btnin.Click
        Dim SqlDbConn As SqlConnection
        Dim bCommon As New BusinessCommon
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim typename As String = boxname.Text.Trim
        Dim typecolor As String = boxcolor.Text.Trim
        Dim strSql As String
        '非法字符判定
        If boxname.Text.Trim.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('名称含有非法字符！')</script>")
        Else
            '非空判定
            If Not "".Equals(typename) Then
                If Not "".Equals(typecolor) Then
                    '判定修改的类别名和数据库中已有的类别名是否重复（除自身之外）
                    strSql = "SELECT COUNT(*) as a FROM BOOK_TYPE_INFO WHERE BOOK_TYPE_NAME ='" & boxname.Text.Trim & "'AND DEL_FLAG='0'" &
                      "AND BOOK_TYPE_ID <> " + Session("typeid")
                    dbCommand = New SqlCommand(strSql, SqlDbConn)
                    dbCommand.CommandType = System.Data.CommandType.Text
                    dataReader = dbCommand.ExecuteReader()
                    dataReader.Read()
                    Dim ii As String = dataReader("a")
                    dataReader.Close()
                    '如果重复,弹出对话框
                    If ii <> 0 Then
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('该类别名已存在，无法加入')</script>")
                        '如果没有重复的，插入新数据到数据库中
                    Else
                        '判定修改的类别颜色和数据库中已有的类别颜色是否重复（除自身之外）
                        strSql = "SELECT COUNT(*) as b FROM BOOK_TYPE_INFO WHERE BOOK_TYPE_COLOR ='" & boxcolor.Text.Trim &
                     "'AND BOOK_TYPE_ID <> " + Session("typeid")
                        dbCommand = New SqlCommand(strSql, SqlDbConn)
                        dbCommand.CommandType = System.Data.CommandType.Text
                        dataReader = dbCommand.ExecuteReader()
                        dataReader.Read()
                        '把数据?中数据保存在iii中
                        Dim iii As String = dataReader("b")
                        dataReader.Close()
                        If iii > 0 Then
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('该类别颜色已存在，无法加入')</script>")
                        Else
                            '修改语句
                            strSql = "UPDATE BOOK_TYPE_INFO SET BOOK_TYPE_COLOR = '" & Me.boxcolor.Text
                            strSql = strSql & "', BOOK_TYPE_NAME = '" & Me.boxname.Text
                            strSql = strSql & "' WHERE BOOK_TYPE_ID = " + Session("typeid")
                            dbCommand = New SqlCommand(strSql, SqlDbConn)
                            dbCommand.ExecuteNonQuery()
                            Response.Redirect("book_type.aspx")
                        End If
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('类别颜色不能为空！')</script>")
                End If
                dbCommand.Dispose()
                SqlDbConn.Close()
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('类别名称不能为空！')</script>")
            End If
        End If
    End Sub
End Class

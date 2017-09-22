Imports System.Data
Imports System.Data.SqlClient

''' 作者：单瑶
''' 界面：来源编辑界面
''' 内容：可编辑来源
Partial Class book_resource_up
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入界面，获取参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bCommon As New BusinessCommon
        If IsPostBack = False Then
            Dim typeid As String = Request.QueryString("SOURCE_TYPE_ID")
            setData(typeid)
            Session("typeid") = typeid
        End If
    End Sub

    ''' <summary>
    ''' 数据读取方法
    ''' </summary>
    ''' <param name="no"></param>
    ''' <remarks></remarks>
    Protected Sub setData(ByVal no As String)
        '定义列表
        Dim arrList As New ArrayList()
        Dim bCommon As New BusinessCommon
        '与数据库连接
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        'sql语句
        Dim strSql As String = "select * from BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_ID = " & no
        dbCommand = New SqlCommand(strSql, myconn)
        dbCommand.CommandType = System.Data.CommandType.Text
        myconn.Open()
        dataReader = dbCommand.ExecuteReader()
        '将该id对应的来源名赋值给boxname
        While (dataReader.Read())
            Me.boxname.Text = dataReader("SOURCE_TYPE_NAME").ToString()
        End While
    End Sub

    ''' <summary>
    ''' 取消按钮，返回来源界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        Response.Redirect("book_resource.aspx")
    End Sub

    ''' <summary>
    ''' 保存按钮
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
        Dim lyName As String = boxname.Text.Trim
        Dim strSql As String
        '非法字符判定
        If boxname.Text.Trim.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('名称含有非法字符！')</script>")
        Else
            '非空判定
            If Not "".Equals(lyName) Then
                '判定新添的类别名和数据库中已有的类别名是否重复
                strSql = "select COUNT(*) as a from BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_NAME ='" & boxname.Text.Trim &
                    "' AND DEL_FLAG = '0' AND SOURCE_TYPE_ID <>" + Session("typeid")
                dbCommand = New SqlCommand(strSql, SqlDbConn)
                dbCommand.CommandType = System.Data.CommandType.Text
                dataReader = dbCommand.ExecuteReader()
                dataReader.Read()
                Dim ii As String = dataReader("a")
                dataReader.Close()
                '如果重复,弹出对话框
                If ii <> 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('该来源名已存在，无法加入')</script>")
                    '如果没有重复的，修改来源名称
                Else
                    strSql = "UPDATE BOOK_SOURCE_TYPE_INFO SET SOURCE_TYPE_NAME = '" & Me.boxname.Text
                    strSql = strSql & " 'WHERE SOURCE_TYPE_ID = " + Session("typeid")
                    dbCommand = New SqlCommand(strSql, SqlDbConn)
                    dbCommand.ExecuteNonQuery()
                    Response.Redirect("book_resource.aspx")
                End If
                dbCommand.Dispose()
                SqlDbConn.Close()
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('来源名称不能为空！')</script>")
            End If
        End If
    End Sub
End Class

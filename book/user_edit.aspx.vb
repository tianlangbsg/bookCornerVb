Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 作者：陈强
''' 网页：修改有效成员页面
''' 内容：主要是完成对有效成员的员工号、用户名以及使其失效的操作
''' </summary>
''' <remarks></remarks>
Partial Class user_edit
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 载入页面通过session取USER_ID的值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim SqlserverConn As SqlConnection
            Dim bCommon As New BusinessCommon
            Dim USER_ID As String = Request.QueryString("USER_ID")
            SqlserverConn = bCommon.GetConnection()
            setData(USER_ID)
            Session("USER_ID") = USER_ID
        End If
    End Sub

    ''' <summary>
    ''' 点击修改按钮在该成员原有的基础上修改员工号、用户名
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub write_Click(sender As Object, e As EventArgs) Handles write.Click
        If DNo.Text = "" Or DName.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请输入修改的信息')</script>")
        Else
            Dim bCommon As New BusinessCommon
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Dim no = DNo.Text
            Dim name = DName.Text
            '抛出异常
            Try
                '与数据库连接
                myconn = bCommon.GetConnection()
                '拼写sql语句
                Dim sqlStr As String = "UPDATE USER_INFO SET USER_CODE = '" & no & "'"
                sqlStr = sqlStr & ", USER_FULLNAME ='" & name & "'"
                sqlStr = sqlStr & " WHERE USER_ID = " + Session("USER_ID")
                myconn.Open()
                dbCommand = New SqlCommand(sqlStr, myconn)
                dbCommand.ExecuteNonQuery()
                dbCommand.Dispose()
                myconn.Close()
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('update成功。')</script>")
            Catch ex As Exception
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('update失敗、rollbackしました。')</script>")
                Throw New Exception(ex.Message, ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 点击失效按钮使该成员变为无效成员
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub loseEff_Click(sender As Object, e As EventArgs) Handles loseEff.Click
        Dim bCommon As New BusinessCommon
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        '抛出异常
        Try
            '与数据库连接
            myconn = bCommon.GetConnection()
            '拼写sql语句
            Dim sqlStr As String = "UPDATE USER_INFO SET VALID_FLAG = '1' WHERE USER_ID =" + Session("USER_ID")
            myconn.Open()
            dbCommand = New SqlCommand(sqlStr, myconn)
            dbCommand.ExecuteNonQuery()
            dbCommand.Dispose()
            myconn.Close()
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改状态成功。')</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('修改状态失败。')</script>")
            Throw New Exception(ex.Message, ex)
        End Try
        '跳转到成员名单界面
        Response.Redirect("user_list.aspx")
    End Sub

    ''' <summary>
    ''' 写一个进入修改页面能在文本框中显示该成员原有的信息的方法
    ''' </summary>
    ''' <param name="USER_ID"></param>
    ''' <remarks></remarks>
    Protected Sub setData(ByVal USER_ID As String)
        Dim arrList As New ArrayList()
        Dim bCommon As New BusinessCommon
        '与数据库连接
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        Dim strSql As String = "select * from USER_INFO where USER_ID = " + USER_ID
        dbCommand = New SqlCommand(strSql, myconn)
        dbCommand.CommandType = System.Data.CommandType.Text
        myconn.Open()
        dataReader = dbCommand.ExecuteReader()
        '当dataReader读到数据时，将该成员的原有信息显示出来
        While (dataReader.Read())
            Me.DNo.Text = dataReader("USER_CODE").ToString()
            Me.DName.Text = dataReader("USER_FULLNAME").ToString()
        End While
    End Sub

    ''' <summary>
    ''' 点击返回按钮跳转到成员名单界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_button_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("user_list.aspx")
    End Sub
End Class

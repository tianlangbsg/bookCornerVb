Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 系统设置
''' 孙德鑫
''' </summary>
''' <remarks></remarks>
Partial Class book_set
    Inherits System.Web.UI.Page
    '
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            '与数据库连接
            Dim bCommon As New BusinessCommon
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            '显示原本的内容
            Dim strSql As String = "select SET_VALUES from BOOK_CORNER.dbo.SYSTEM_PARMSET_INFO where PARM_ID =1 "
            Dim strSql1 As String = "select SET_VALUES from BOOK_CORNER.dbo.SYSTEM_PARMSET_INFO where PARM_ID =2 "
            TextBox1.Text = bCommon.ExecScalar("select SET_VALUES from BOOK_CORNER.dbo.SYSTEM_PARMSET_INFO where PARM_ID =1 ")
            TextBox2.Text = bCommon.ExecScalar("select SET_VALUES from BOOK_CORNER.dbo.SYSTEM_PARMSET_INFO where PARM_ID =2 ")
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("home_manager.aspx?")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim bCommon As New BusinessCommon
        Dim myconn = bCommon.GetConnection()
        Try
            '与数据库连接
            myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Dim dbCommand1 As New SqlCommand
            '对内容进行修改
            Dim sqlStr As String = "UPDATE SYSTEM_PARMSET_INFO SET SET_VALUES = '" & TextBox1.Text & "' where PARM_ID =1"
            Dim sqlStr1 As String = "UPDATE SYSTEM_PARMSET_INFO SET SET_VALUES = '" & TextBox2.Text & "' where PARM_ID =2"
            myconn.Open()
            dbCommand = New SqlCommand(sqlStr, myconn)
            dbCommand1 = New SqlCommand(sqlStr1, myconn)
            dbCommand.ExecuteNonQuery()
            dbCommand1.ExecuteNonQuery()
            dbCommand.Dispose()
            myconn.Close()
            '成功后进行跳转
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改成功');location.href='home_manager.aspx'</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改失败')</script>")
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub

End Class

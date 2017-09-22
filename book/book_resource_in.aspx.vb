Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：来源添加界面
''' 内容：可添加来源
Partial Class book_resource_in
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 添加按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnjia_Click(sender As Object, e As EventArgs) Handles btnjia.Click
        Dim SqlDbConn As SqlConnection
        Dim bCommon As New BusinessCommon
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim typeName As String = lyname.Text.Trim
        Dim strSql As String
        '非法字符判定
        If lyname.Text.Trim.Contains("'") Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('名称含有非法字符！')</script>")
        Else
            '非空判定
            If Not "".Equals(typeName) Then
                '判定新添的类别名和数据库中已有的类别名是否重复
                strSql = "select COUNT(*) as a from BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_NAME ='" & lyname.Text.Trim & "'"
                dbCommand = New SqlCommand(strSql, SqlDbConn)
                dbCommand.CommandType = System.Data.CommandType.Text
                dataReader = dbCommand.ExecuteReader()
                dataReader.Read()
                Dim ii As String = dataReader("a")
                dataReader.Close()
                '如果重复,弹出对话框
                If ii <> 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('该来源名已存在，无法加入')</script>")
                    '如果没有重复的，插入新数据到数据库中
                Else
                    strSql = "INSERT INTO BOOK_SOURCE_TYPE_INFO (SOURCE_TYPE_NAME,DEL_FLAG,CREATED_AT,UPDATED_AT) VALUES ('" & lyname.Text.Trim & "','0',GETDATE(),GETDATE())"
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

    ''' <summary>
    '''  取消按钮，返回来源界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        Response.Redirect("book_resource.aspx")
    End Sub
End Class

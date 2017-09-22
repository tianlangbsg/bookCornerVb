Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：图书类别添加界面
''' 内容：添加图书类别
Partial Class tjfl
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '取消按钮，直接返回type界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        Response.Redirect("book_type.aspx")
    End Sub

    ''' <summary>
    ''' '添加按钮，
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnjia_Click(sender As Object, e As EventArgs) Handles btnjia.Click     
        '连接数据库
        Dim SqlDbConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '建立连接对象
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
                    '判定新添的类别名和数据库中已有的类别名是否重复
                    strSql = "select COUNT(*) as a from BOOK_TYPE_INFO where BOOK_TYPE_NAME ='" & boxname.Text.Trim & "'"
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
                        strSql = "INSERT INTO  BOOK_TYPE_INFO(BOOK_TYPE_NAME,BOOK_TYPE_COLOR,DEL_FLAG,CREATED_AT,UPDATED_AT) VALUES('" & boxname.Text.Trim & "','" & boxcolor.Text.Trim & "','0',GETDATE(),GETDATE())"
                        dbCommand = New SqlCommand(strSql, SqlDbConn)
                        dbCommand.ExecuteNonQuery()
                        Response.Redirect("book_type.aspx")
                    End If
                    dbCommand.Dispose()
                    SqlDbConn.Close()
                Else
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('类别名称和颜色不能为空！')</script>")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('类别名称和颜色不能为空！')</script>")
            End If
        End If
    End Sub
End Class


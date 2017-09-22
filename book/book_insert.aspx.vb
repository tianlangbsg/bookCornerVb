Imports System.Data.SqlClient
Imports System.Data

''' <summary>
''' 作者：陈强
''' 网页：图书录入页面
''' 内容：主要是完成对图书信息的录入，并能够插入图片
''' </summary>
''' <remarks></remarks>
Partial Class book_insert
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入页面显示已有图书及其信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '数据库连接
        Dim SqlDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        SqlDbConn = bCommon.GetConnection()
        '定义strSql变量
        Dim strSql As String
        strSql = "SELECT BOOK_BARCODE,BOOK_NAME,BOOK_INTRO,case BOOK_TYPE_ID " &
                 "when 1 then '综合类' when 2 then '开发类' when 3 then '管理类' when 4 then '养生保健类'when 5 then '生活家居类'when 6 then '过期刊物类'else '' end BOOK_TYPE_ID" &
                 ",case SOURCE_TYPE_ID when 1 then '员工捐赠' when 2 then '公司购入' when 3 then '工会捐赠'else '' end SOURCE_TYPE_ID,CONTRIBUTOR_ID  from BOOK_INFO "
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, SqlDbConn)
        '抛出异常
        Try
            '填充数据，有则填充，无则提示
            sda.Fill(ds, "BOOK_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                Book_centent.DataSource = ds
                Book_centent.DataBind()
            Else
                Book_centent.DataSource = Nothing
                Book_centent.DataBind()
            End If
        Catch ex As Exception
        End Try
        sda.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 点击录入按钮，会将所输入的新图书的图书信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub loadin_Click(sender As Object, e As EventArgs) Handles loadin.Click
        '定义数据库连接对象
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        '抛出异常
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        '数据库连接
        myconn = bCommon.GetConnection()
        '定义变量
        Dim bookCode As String
        Dim bookName As String
        Dim detail As String
        Dim booktype As String
        Dim sourcetype As String
        Dim donorId As String
        Dim sourceid As String
        Dim strSql As String
        '从前台控件里取值赋给变量
        bookCode = Tiaoxingma.Text
        bookName = book_Name.Text
        detail = centent.Text
        booktype = book_Type.Text
        sourcetype = bookSource.Text
        donorId = userID.Text
        '根据选择的类型查询
        Try
            booktype = bCommon.ExecScalar("select BOOK_TYPE_ID from BOOK_TYPE_INFO where BOOK_TYPE_NAME='" & book_Type.Text & "'")
            If FileUpload1.HasFile Then
                Dim pic As String = System.IO.Path.GetExtension(FileUpload1.FileName)
                If pic.Equals(".jpg") Or pic.Equals(".png") Or pic.Equals(".gif") Then
                    Try
                        FileUpload1.SaveAs(Server.MapPath("images/") + FileUpload1.FileName)
                        Label7.Text = "客户端路径:" + FileUpload1.PostedFile.FileName + "<br>"
                        Label8.Text = "文件名:" + System.IO.Path.GetFileName(FileUpload1.FileName) + "<br>"
                        Label9.Text = "文件拓展名:" + System.IO.Path.GetExtension(FileUpload1.FileName) + "<br>"
                        Label10.Text = "文件大小:" + FileUpload1.PostedFile.ContentLength.ToString + "KB<br>"
                        Label11.Text = "文件MIME类型型:" + FileUpload1.PostedFile.ContentType + "<br>"
                        Label12.Text = "保存路径:" + Server.MapPath("images/") + FileUpload1.FileName
                    Catch ex As Exception
                        Label13.Text = "发生错误:" + ex.Message.ToString
                    End Try
                Else
                    Label13.Text = "只允许上传jpg,png,gif文件"
                End If
            End If
            Dim imgno As String = System.IO.Path.GetFileName(FileUpload1.FileName)
            sourceid = bCommon.ExecScalar("select SOURCE_TYPE_ID from BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_NAME='" & bookSource.Text & "'")
            sourcetype = sourceid
            'if语句判断图书来源是否[员工捐赠]，然后录入图书信息
            If bookSource.Text.Equals("员工捐赠") Then
                strSql = "insert into BOOK_INFO(BOOK_BARCODE,BOOK_NAME,BOOK_INTRO, BOOK_TYPE_ID,SOURCE_TYPE_ID,CONTRIBUTOR_ID,BORROW_FLAG,DEL_FLAG,CREATED_USER,UPDATED_USER,CREATED_AT,UPDATED_AT) values('" & bookCode & "','" & bookName & "','" & detail & "', '" & booktype & "', '" & sourcetype & "','" & donorId & "',0,1,1,1,GETDATE(),GETDATE(),'" & imgno & "')"
            Else
                strSql = "insert into BOOK_INFO(BOOK_BARCODE,BOOK_NAME,BOOK_INTRO, BOOK_TYPE_ID,SOURCE_TYPE_ID,BORROW_FLAG,DEL_FLAG,CREATED_USER,UPDATED_USER,CREATED_AT,UPDATED_AT,PICTURE1) values('" & bookCode & "','" & bookName & "','" & detail & "', '" & booktype & "', '" & sourcetype & "',0,1,1,1,GETDATE(),GETDATE(),'" & imgno & "')"
            End If
            myconn.Open()
            dbCommand = New SqlCommand(strSql, myconn)
            dbCommand.ExecuteNonQuery()
            dbCommand.Dispose()
            myconn.Close()
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('插入成功！')</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('插入失败！')</script>")
        End Try
    End Sub

    ''' <summary>
    ''' 点击返回按钮跳转到管理中心页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub return_button_Click(sender As Object, e As EventArgs) Handles return_button.Click
        Response.Redirect("home_manager.aspx")
    End Sub

End Class
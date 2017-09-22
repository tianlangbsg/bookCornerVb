Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 图书信息编辑
''' 孙德鑫
''' </summary>
''' <remarks></remarks>
Partial Class book_edit

    Inherits System.Web.UI.Page
    ''' <summary>
    ''' 在?面加???示??的原本信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim BOOK_ID As String = Request.QueryString("BOOK_ID")
            Label1.Text = ""
            Label2.Text = ""
            Label3.Text = ""
            Label4.Text = ""
            Label5.Text = ""
            Label6.Text = ""
            Label7.Text = ""
            Dim bCommon As New BusinessCommon
            Dim arrList As New ArrayList()
            Dim arrList1 As New ArrayList()
            Dim arrList2 As New ArrayList()
            arrList = bCommon.getDeptb()
            arrList1 = bCommon.getDeptc()
            arrList2 = bCommon.getDeptd()
            DropDownList1.DataSource = arrList
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "")
            DropDownList2.DataSource = arrList1
            DropDownList2.DataBind()
            DropDownList2.Items.Insert(0, "")
            DropDownList3.DataSource = arrList2
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "")
            '与数据??接
            Dim myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            '?示原本的内容
            Dim strSql As String = "select BOOK_NAME from BOOK_INFO where BOOK_ID =" & BOOK_ID
            Dim BOOK_NAME As String
            BOOK_NAME = bCommon.ExecScalar(strSql)
            tbid.Text = BOOK_ID
            tbname.Text = BOOK_NAME
            tbid.Enabled = False
            Dim strSql1 As String = "select BOOK_INTRO from BOOK_INFO where BOOK_ID =" & BOOK_ID
            Dim strSql2 As String = "select BOOK_TYPE_ID from BOOK_INFO where BOOK_ID =" & BOOK_ID
            Dim strSql3 As String = "select SOURCE_TYPE_ID from BOOK_INFO where BOOK_ID =" & BOOK_ID
            Dim strSql4 As String = "select BOOK_BARCODE from BOOK_INFO where BOOK_ID =" & BOOK_ID
            tbtext.Text = bCommon.ExecScalar(strSql1)
            tbma.Text = bCommon.ExecScalar(strSql4)
            '?示分?
            Dim BOOK_TYPE_ID As String
            BOOK_TYPE_ID = bCommon.ExecScalar(strSql2)
            DropDownList1.Text = bCommon.ExecScalar("select BOOK_TYPE_NAME from BOOK_CORNER.dbo.BOOK_TYPE_INFO where BOOK_TYPE_ID =" & BOOK_TYPE_ID)
            '?示来源
            Dim SOURCE_TYPE_ID As Integer
            SOURCE_TYPE_ID = bCommon.ExecScalar(strSql3)

            DropDownList2.Text = bCommon.ExecScalar("select SOURCE_TYPE_NAME from BOOK_CORNER.dbo.BOOK_SOURCE_TYPE_INFO where SOURCE_TYPE_ID =" & SOURCE_TYPE_ID)
            If SOURCE_TYPE_ID = 1 Then
                DropDownList3.Text = bCommon.ExecScalar("select CONTRIBUTOR_ID from BOOK_CORNER.dbo.BOOK_INFO where BOOK_ID =" & BOOK_ID)
            Else : DropDownList3.Enabled = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' 将?片上?
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub UploadButton_Click(sender As Object, e As EventArgs) Handles UploadButton.Click

        Dim pic As String = System.IO.Path.GetExtension(FileUpload1.FileName)
        '??片格式?行判断
        If (pic.Equals(".jpg")) Or (pic.Equals(".png")) Or (pic.Equals(".gif")) Or (pic.Equals(".JPG")) Or (pic.Equals(".PNG")) Or (pic.Equals(".GIF")) Then
            FileUpload1.SaveAs(Server.MapPath("images/") + FileUpload1.FileName)
            Label1.Text = "客?端路径:" + FileUpload1.PostedFile.FileName + "<br>"
            Label2.Text = "文件名:" + System.IO.Path.GetFileName(FileUpload1.FileName) + "<br>"
            Label3.Text = "文件?展名:" + System.IO.Path.GetExtension(FileUpload1.FileName) + "<br>"
            Label4.Text = "文件大小:" + FileUpload1.PostedFile.ContentLength.ToString + "KB<br>"
            Label5.Text = "文件MIME?型:" + FileUpload1.PostedFile.ContentType + "<br>"
            Label6.Text = "保存路径:" + Server.MapPath("images/") + FileUpload1.FileName
        Else
            Label7.Text = "只允?上?jpg,png,gif文件"
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Response.Redirect("book_manager_list.aspx?")

    End Sub

    ''' <summary>
    ''' ???信息?行修改
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub bsave_Click(sender As Object, e As EventArgs) Handles bsave.Click

        Dim BOOK_ID As String = Request.QueryString("BOOK_ID")
        Dim bCommon As New BusinessCommon
        Dim myconn = bCommon.GetConnection()
        Try
            '与数据??接
            myconn = bCommon.GetConnection()
            Dim dbCommand As New SqlCommand
            Dim type1 As Integer
            Dim type As String
            type = DropDownList1.SelectedIndex
            ''判断??的???型
            'Select Case type
            '    Case "?合?"
            '        type1 = 1
            '    Case "???"
            '        type1 = 2
            '    Case "管理?"
            '        type1 = 3
            '    Case "?生保健?"
            '        type1 = 4
            '    Case "生活家居?"
            '        type1 = 5
            '    Case "?尚小??"
            '        type1 = 6
            '    Case "?期刊物?"
            '        type1 = 7
            '    Case "理ke?"
            '        type1 = 8
            '    Case "外?生活?"
            '        type1 = 9
            '    Case "情感生活?"
            '        type1 = 10
            'End Select
            Dim source1 As Integer
            'Dim source As String
            source1 = DropDownList2.SelectedIndex
            'Select Case source
            '    Case "?工捐?"
            '        source1 = 1
            '    Case "公司?入"
            '        source1 = 2
            '    Case "工会捐?"
            '        source1 = 3
            'End Select
            Dim strSql3 As String = "select SOURCE_TYPE_ID from BOOK_INFO where BOOK_ID =" & BOOK_ID
            Dim SOURCE_TYPE_ID As Integer
            SOURCE_TYPE_ID = bCommon.ExecScalar(strSql3)
            Dim con As String
            Dim con1 As String
            con1 = DropDownList3.SelectedValue
            '判断??的用? ID
            Select Case SOURCE_TYPE_ID
                Case 1
                    Select Case con1
                        Case "2"
                            con = 2
                        Case Else
                            con = "null"
                    End Select
                Case Else
                    con = "null"
            End Select
            If (Not "1".Equals(source1)) Then
                '?内容?行修改
                Dim sqlStr As String = "UPDATE BOOK_INFO SET BOOK_NAME = '" & tbname.Text & "' ,BOOK_INTRO ='" & tbtext.Text & "' ,BOOK_BARCODE ='" & tbma.Text & "' ,BOOK_TYPE_ID =" & type & " ,SOURCE_TYPE_ID =" & source1 & " ,CONTRIBUTOR_ID =" & con & "  where BOOK_ID =" & BOOK_ID
                myconn.Open()
                dbCommand = New SqlCommand(sqlStr, myconn)
            Else : Dim sqlStr As String = "UPDATE BOOK_INFO SET BOOK_NAME = '" & tbname.Text & "' ,BOOK_INTRO ='" & tbtext.Text & "' ,BOOK_BARCODE ='" & tbma.Text & "' ,BOOK_TYPE_ID =" & type & " ,SOURCE_TYPE_ID =" & source1 & " ,CONTRIBUTOR_ID =" & con & "  where BOOK_ID =" & BOOK_ID
                myconn.Open()
                dbCommand = New SqlCommand(sqlStr, myconn)
            End If
            dbCommand.ExecuteNonQuery()
            dbCommand.Dispose()
            myconn.Close()
            '成功后?行跳?
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改成功');location.href='book_manager_list.aspx'</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('更改失?')</script>")
            Throw New Exception(ex.Message, ex)
        End Try
    End Sub

End Class

Imports System.Data
Imports System.Data.SqlClient
''' <summary>
''' 借阅历史
''' 孙德鑫
''' 获取图书的借阅历史
Partial Class book_history
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim BOOK_ID As String = Request.QueryString("BOOK_ID")
        Dim bCommon As New BusinessCommon

        If IsPostBack = False Then
            Dim arrList As New ArrayList()
            arrList = bCommon.getDepta()
            DropDownList1.DataSource = arrList
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "")

            DropDownList2.DataSource = arrList
            DropDownList2.DataBind()
            DropDownList2.Items.Insert(0, "")

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0

        Dim bCommon As New BusinessCommon()
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String

        strSql = "select  BORROW_ID,BOOK_ID,USER_ID,BORROW_DATE,RETURN_DATE from BOOK_CORNER.dbo.BOOK_BORROW_INFO "
        If RadioButton1.Checked = True Then
            strSql = strSql & " where (RETURN_DATE is null or RETURN_DATE is not null)"
        ElseIf RadioButton2.Checked = True Then
            strSql = strSql & " where (RETURN_DATE is not null)"
        ElseIf RadioButton3.Checked = True Then
            strSql = strSql & " where (RETURN_DATE is null)"
        End If

        If DropDownList1.SelectedValue <> "" Then

            Dim id1 As Integer
            id1 = DropDownList1.Text.Trim
            If DropDownList2.SelectedValue <> "" Then

                Dim id2 As Integer
                id2 = DropDownList2.Text.Trim
                If (id1 <= id2) Then
                    strSql = strSql & " and (BORROW_DATE >=" & id1 & "and BORROW_DATE<=" & id2 & ")"
                Else : Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请重新选择')</script>")
                End If
            End If
        End If

        If DropDownList1.SelectedValue <> "" Then
            Dim id1 As Integer
            id1 = DropDownList1.Text.Trim
            If DropDownList2.SelectedValue = "" Then

                strSql = strSql & " and (BORROW_DATE >=" & id1 & ")"

            End If

        End If

        If DropDownList1.SelectedValue = "" Then
            If DropDownList2.SelectedValue <> "" Then
                Dim id2 As Integer
                id2 = DropDownList2.Text.Trim
                strSql = strSql & " and (BORROW_DATE <=" & id2 & ")"

            End If

        End If

        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        '在画面中显示检索内容
        dataAdapter.Fill(ds, "BOOK_BORROW_INFO")

        If ds.Tables(0).Rows.Count > 0 Then
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("book_detail_manage.aspx?bookid=" & Request.QueryString("BOOK_ID"))
    End Sub

End Class


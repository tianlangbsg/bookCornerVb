''' <summary>
''' 等待审核页面
''' 孙德鑫
''' </summary>
''' <remarks></remarks>
Partial Class reviewing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then

        End If

        Dim name As String = Request.QueryString("name")
        Dim number As String = Request.QueryString("usercode")
        Dim user_date As String = Request.QueryString("date")
        TextBox1.Text = name
        TextBox2.Text = number
        TextBox3.Text = user_date
        'Dim bCommon As New BusinessCommon
        'TextBox3.Text = bCommon.ExecScalar("select APPLY_AT from BOOK_CORNER.dbo.USER_REVIEW_INFO where USER_FULLNAME ='" & name & "'")

    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("apply.aspx?")
    End Sub
End Class


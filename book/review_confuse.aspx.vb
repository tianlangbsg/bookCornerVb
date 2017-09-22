Partial Class review_confuse
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 申请被拒绝
    ''' 孙德鑫
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then

            Dim name As String = Request.QueryString("name")
            Dim number As String = Request.QueryString("usercode")
            Dim confuse_date As String = Request.QueryString("date")
            TextBox1.Text = name
            TextBox2.Text = number
            TextBox3.Text = confuse_date

        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("apply.aspx")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Redirect("home_book.aspx")
    End Sub
End Class


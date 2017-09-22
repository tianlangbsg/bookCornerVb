Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：用户书单界面
''' 内容：可查看该用户借阅过的书单，点击详情可查看该图书的详情
Partial Class user_book_list
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '载入界面，获取参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
     Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim bCommon As New BusinessCommon
        If IsPostBack = False Then
            '获取从用户排行界面传来的两个参数
            Dim userid As String = Request.QueryString("USER_ID")
            Dim name As String = Request.QueryString("USER_NAME")
            '读取setdata方法中的数据
            setData(userid)
            Session("userid") = userid
            '文字显示参数+的书单
            Label1.Text = name & "的书单"
        End If
    End Sub

    ''' <summary>
    ''' '根据传来的ID，从数据库中读取其他信息
    ''' </summary>
    ''' <param name="no"></param>
    ''' <remarks></remarks>
    Protected Sub setData(ByVal no As String)
        Dim arrList As New ArrayList()
        Dim bCommon As New BusinessCommon
        '与数据库连接
        Dim myconn = bCommon.GetConnection()
        Dim dbCommand As New SqlCommand
        Dim dataReader As SqlDataReader
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        '按借阅时间降续，查询ID对应的其他信息
        Dim strSql As String = "select BBI.BORROW_ID,BI.BOOK_ID,BI.BOOK_NAME,CONVERT(DATE,BBI.BORROW_DATE) AS BORROW_DATE,CONVERT(DATE,BBI.RETURN_DATE) AS RETURN_DATE from  BOOK_BORROW_INFO BBI JOIN BOOK_INFO BI ON BBI.BOOK_ID=BI.BOOK_ID " &
            "where BBI.USER_ID=" & no & "ORDER BY BBI.BORROW_DATE DESC"
        dbCommand = New SqlCommand(strSql, myconn)
        dbCommand.CommandType = System.Data.CommandType.Text
        myconn.Open()
        dataReader = dbCommand.ExecuteReader()
        While (dataReader.Read())
            Dim ds As New DataSet()
            Dim dataAdapter As New SqlDataAdapter(strSql, SqlserverConn)
            dataAdapter.Fill(ds, "BOOK_BORROW_INFO")
            If ds.Tables(0).Rows.Count > 0 Then
                yhsd.DataSource = ds
                yhsd.DataBind()
            Else
                yhsd.DataSource = Nothing
                yhsd.DataBind()
            End If
            dataAdapter.Dispose()
            ds.Dispose()
            SqlserverConn.Close()
        End While     
    End Sub

    ''' <summary>
    ''' '取消按钮跳转到用户排行界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("user_chart.aspx")
    End Sub


    ''' <summary>
    ''' '书籍详情跳转
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub yhsd_SelectedIndexChanging1(sender As Object, e As GridViewSelectEventArgs) Handles yhsd.SelectedIndexChanging
        Dim bookid As String
        '把书的id传给图书详情界面
        bookid = yhsd.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("book_detail.aspx?book_id=" & bookid)
    End Sub
End Class

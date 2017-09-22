Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 显示图片详情
''' 王康
''' </summary>
Partial Class book_pic_list

    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 页面加载函数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '定义数据库连接
        Dim SqlserverConn As SqlConnection
        Dim bCommon As New BusinessCommon
        '取得连接
        SqlserverConn = bCommon.GetConnection()
        Dim strSql As String
        '取得书的编号
        Dim book_id = Request("book_id")
        SqlserverConn = bCommon.GetConnection()
        '打开连接
        SqlserverConn.Open()
        '根据BOOK_ID取出详细信息
        strSql = "SELECT  A.PICTURE1,A.PICTURE2,A.PICTURE3,A.PICTURE4,A.PICTURE5,A.PICTURE6,A.PICTURE7,A.PICTURE8,A.PICTURE9" &
                 " FROM  BOOK_INFO A WHERE BOOK_ID = " & book_id
        '定义语句执行对象
        Dim myCmd = New SqlCommand(strSql, SqlserverConn)
        myCmd.CommandType = System.Data.CommandType.Text
        Dim dataReader As SqlDataReader
        dataReader = myCmd.ExecuteReader()
        '当读取到数据时
        While dataReader.Read
            Dim pic_num As Integer = 0
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<a href='book_detail?book_id=" & book_id & "'><asp:Button ID='return_button' runat='server' Text='返回' BackColor='#820000' BorderColor='#FFCCCC' BorderStyle='Double' ForeColor='White' /></a>")
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<table align='center' width='800px' border='1' cellspacing='2' cellpadding='2'>")
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<tr><td style='text-align: center;align:center'><h1>图片详情</h1></td></tr>")
            For i = 1 To 9
                '若数据库中的图片地址不为空
                Dim pic_url As String = dataReader("PICTURE" & i).ToString
                If pic_url <> "" Then
                    Dim picid As String = CStr(dataReader("PICTURE" & i))
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<tr>")
                    '写出带有超链接的图片
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<td style='text-align: center;align:center'><a href='images/" & picid & "'><img src='images/" & picid & "' width='400px' height='400px'></img></a></td>")
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<td style='text-align: center'><img src='""images/" & CStr(dataReader("PICTURE1")) & "'></img></td>")
                    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<td style='text-align: center'><img src='""images/" & CStr(dataReader("PICTURE" & i)) & "'></img></td>")
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "</tr>")
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<br/>")
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "</table>")
        End While
        '释放资源
        dataReader.Close()
        myCmd.Dispose()
        SqlserverConn.Close()
    End Sub
End Class

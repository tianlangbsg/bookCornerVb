Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 页面：用户排行界面
''' 内容：查看用户排行，可以根据阅读数、加入时间排序，点击详细可查看用户的书单
Partial Class user_chart
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '载入界面，查看根据阅读数排序的用户
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            '连接数据库
            Dim OleDbConn As SqlConnection
            Dim iRow As Integer = 0
            '建立数据对象
            Dim bCommon As New BusinessCommon
            OleDbConn = bCommon.GetConnection()
            Dim strSql As String
            '获取用户排行列表，按阅读数排行排序
            strSql = "SELECT *From(SELECT	ROW_NUMBER() OVER(ORDER BY BORROW_COUNT ASC) AS ID0,*FROM (SELECT UI.USER_ID ," &
                "UI.USER_FULLNAME ,CONVERT(DATE,UI.JOIN_AT) AS JOIN_AT,(SELECT COUNT(*) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID )" &
                "AS BORROW_COUNT,(SELECT MAX(BBI.BORROW_DATE) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) " &
                "AS BORROW_TIME	FROM  USER_INFO UI 	WHERE UI.VALID_FLAG = 0	)X ) J	"
            Dim ds As New DataSet()
            Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
            dataAdapter.Fill(ds, "BOOK_BORROW_INFO")
            '列表显示符合查询条件的用户信息
            If ds.Tables(0).Rows.Count > 0 Then
                phmd.DataSource = ds
                phmd.DataBind()
            End If
            dataAdapter.Dispose()
            ds.Dispose()
        End If
    End Sub

    ''' <summary>
    ''' '点击“详情”超链接之后，传给用户书单界面用户ID和姓名两个参数
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub phmd_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim userid As String
        Dim name As String
        userid = phmd.DataKeys(e.NewSelectedIndex).Value.ToString()
        name = phmd.Rows(e.NewSelectedIndex).Cells(1).Text.ToString()
        Response.Redirect("user_book_list.aspx?USER_ID=" & userid & "&USER_NAME=" & name)
    End Sub

    ''' <summary>
    ''' '阅读数排行按钮，功能和pageload一样
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnread_Click(sender As Object, e As EventArgs) Handles btnread.Click
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        '获取用户排行列表，阅读数排行
        strSql = "SELECT *From(SELECT	ROW_NUMBER() OVER(ORDER BY BORROW_COUNT ASC) AS ID0,*FROM (SELECT UI.USER_ID ," &
            "UI.USER_FULLNAME ,CONVERT(DATE,UI.JOIN_AT) AS JOIN_AT,(SELECT COUNT(*) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID )" &
            "AS BORROW_COUNT,(SELECT MAX(BBI.BORROW_DATE) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) " &
            " AS BORROW_TIME 	FROM  USER_INFO UI 	WHERE UI.VALID_FLAG = 0	)X ) J	"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "BOOK_BORROW_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            phmd.DataSource = ds
            phmd.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '按加入时间升续排列
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnasc_Click(sender As Object, e As EventArgs) Handles btnasc.Click
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        '查询借阅表中用户的所有信息，按加入时间升续排列
        strSql = "SELECT *From(SELECT	ROW_NUMBER() OVER(ORDER BY JOIN_AT ASC) AS ID0,*FROM (SELECT UI.USER_ID ,UI.USER_FULLNAME ,CONVERT(DATE,UI.JOIN_AT) AS JOIN_AT,(SELECT COUNT(*) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) AS BORROW_COUNT,(SELECT MAX(BBI.BORROW_DATE) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) AS BORROW_TIME	FROM  USER_INFO UI 	WHERE UI.VALID_FLAG = 0	)X ) J	"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "BOOK_BORROW_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            phmd.DataSource = ds
            phmd.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '按加入时间降续排列
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndesc_Click(sender As Object, e As EventArgs) Handles btndesc.Click
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        '查询借阅表中用户的所有信息，按加入时间降续排列
        strSql = "SELECT *From(SELECT	ROW_NUMBER() OVER(ORDER BY JOIN_AT DESC) AS ID0,*FROM (SELECT UI.USER_ID ,UI.USER_FULLNAME ,CONVERT(DATE,UI.JOIN_AT) AS JOIN_AT,(SELECT COUNT(*) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) AS BORROW_COUNT,(SELECT MAX(BBI.BORROW_DATE) FROM BOOK_BORROW_INFO BBI	WHERE BBI.USER_ID = UI.USER_ID ) AS BORROW_TIME	FROM  USER_INFO UI 	WHERE UI.VALID_FLAG = 0	)X ) J	"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "BOOK_BORROW_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            phmd.DataSource = ds
            phmd.DataBind()
        Else
            phmd.DataSource = Nothing
            phmd.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '返回到用户管理界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("home_manager.aspx")
    End Sub
End Class

Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' 作者：陈强
''' 网页：管理中心页面
''' 内容：主要是完成各个页面的跳转，并且在本页面显示有关数据
''' </summary>
''' <remarks></remarks>
Partial Class home_manager
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入界面显示每个页面的文字超链接，并显示上架图书数量、有效待审核成员数量、有效成员数量
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '建立数据库连接
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon()
        OleDbConn = bCommon.GetConnection()
        '上架图书数量查询后返回到前台
        Dim strSql As String
        strSql = "select count(*) from BOOK_INFO where DEL_FLAG='0'"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "BOOK_INFO")
        booknumber.Text = bCommon.ExecScalar(strSql)
        '有效待审核成员数量查询后返回到前台
        OleDbConn = bCommon.GetConnection()
        Dim strSql1 As String
        strSql1 = "select count(*) from USER_REVIEW_INFO where REVIEW_FLAG='0'"
        Dim ds1 As New DataSet()
        Dim dataAdapter1 As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds1, "USER_REVIEW_INFO")
        shenhe.Text = bCommon.ExecScalar(strSql1)
        '有效成员数量查询后返回到前台
        OleDbConn = bCommon.GetConnection()
        Dim strSql2 As String
        strSql2 = "select count(*) from USER_INFO"
        Dim ds2 As New DataSet()
        Dim dataAdapter2 As New SqlDataAdapter(strSql2, OleDbConn)
        dataAdapter.Fill(ds2, "USER_INFO")
        seeman.Text = bCommon.ExecScalar(strSql2)
    End Sub

    ''' <summary>
    ''' 点击退出登录放回到登录首页
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Response.Redirect("index.aspx")
    End Sub

End Class

Imports System.Data.SqlClient
Imports System.Data

''' <summary>
''' 作者：陈强
''' 网页：成员名单页面
''' 内容：主要是完成对有效成员和无效成员的查看
''' </summary>
''' <remarks></remarks>
Partial Class user_list
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 载入页面直接显示有效成员名单
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '建立数据库连接
        searchEffMen.BackColor = Drawing.Color.FromName("#0000FF")
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        Dim strSql As String
        OleDbConn = bCommon.GetConnection()
        '写数据库查询语句
        strSql = "select USER_ID,USER_FULLNAME,USER_CODE,JOIN_AT from USER_INFO where VALID_FLAG='0'"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        '填充数据 ，有则填充，无则提示
        dataAdapter.Fill(ds, "USER_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            message.Text = ""
            Effmember.DataSource = ds
            Effmember.DataBind()
        Else
            message.Text = "没有符合条件的数据!"
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 点击有效成员按钮显示有效成员名单
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchEffMen_Click(sender As Object, e As EventArgs) Handles searchEffMen.Click
        '显示Effmember控件
        Effmember.Visible = True
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        Dim strSql As String
        OleDbConn = bCommon.GetConnection()
        '写数据库查询语句
        strSql = "select USER_ID,USER_FULLNAME,USER_CODE,convert(date,[JOIN_AT]) AS JOIN_AT from USER_INFO where VALID_FLAG='0'"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        '填充数据 ，有则填充，无则提示
        dataAdapter.Fill(ds, "USER_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            message.Text = ""
            Effmember.DataSource = ds
            Effmember.DataBind()
        Else
            message.Text = "没有符合条件的数据!"
        End If
        dataAdapter.Dispose()
        ds.Dispose()
        '隐藏Inamember控件
        Inamember.Visible = False
        searchEffMen.BackColor = Drawing.Color.FromName("#0000FF")
        searchEffIna.BackColor = Drawing.Color.FromName("#820000")
    End Sub

    ''' <summary>
    ''' 点击无效成员按钮显示无效成员名单
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub searchEffIna_Click(sender As Object, e As EventArgs) Handles searchEffIna.Click
        '显示Inamember控件
        Inamember.Visible = True
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        Dim strSql As String
        OleDbConn = bCommon.GetConnection()
        '写数据库查询语句
        strSql = "select USER_ID,USER_FULLNAME,USER_CODE,convert(date,[JOIN_AT]) AS JOIN_AT from USER_INFO where VALID_FLAG='1'"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        '填充数据 ，有则填充，无则提示
        dataAdapter.Fill(ds, "USER_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            message.Text = ""
            Inamember.DataSource = ds
            Inamember.DataBind()
        Else
            message.Text = "没有符合条件的数据!"
        End If
        dataAdapter.Dispose()
        ds.Dispose()
        '隐藏Effmember控件
        Effmember.Visible = False
        searchEffIna.BackColor = Drawing.Color.FromName("#0000FF")
        searchEffMen.BackColor = Drawing.Color.FromName("#820000")
    End Sub

    ''' <summary>
    ''' 点击返回按钮跳转到管理中心页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub write_Click(sender As Object, e As EventArgs) Handles write.Click
        Response.Redirect("home_manager.aspx")
    End Sub

    ''' <summary>
    ''' 点击返回按钮带上USER_ID一起跳转到修改有效成员页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Effmember_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim USER_ID As String
        USER_ID = Effmember.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("user_edit.aspx?USER_ID=" & USER_ID & "")
    End Sub

    ''' <summary>
    ''' 点击返回按钮带上USER_ID一起跳转到修改无效成员页面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Inamember_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs)
        Dim USER_ID As String
        USER_ID = Inamember.DataKeys(e.NewSelectedIndex).Value.ToString()
        Response.Redirect("user_edit_enable.aspx?USER_ID=" & USER_ID & "")
    End Sub
End Class

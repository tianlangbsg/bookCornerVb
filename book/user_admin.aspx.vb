Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 界面：管理员管理界面
''' 内容：查看管理员和普通用户信息，可以删除或添加权限
Partial Class user_admin
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '载入界面，显示管理员信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            '检索管理员信息
            Call Bind()
        End If
    End Sub

    ''' <summary>
    ''' '全选
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer
        Dim chk As CheckBox
        For i = 0 To Me.admin.Rows.Count - 1
            chk = CType(Me.admin.Rows(i).FindControl("chkCheck"), CheckBox)
            If (Me.chkAll.Checked) Then
                chk.Checked = True
            Else
                chk.Checked = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' '管理员管理按钮，功能与pageload一样
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnadmin_Click(sender As Object, e As EventArgs) Handles btnadmin.Click
        Call Bind()
    End Sub


    ''' <summary>
    ''' '添加管理员按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnin_Click(sender As Object, e As EventArgs) Handles btnin.Click
        Call Bind1()
    End Sub

    ''' <summary>
    ''' '删除权限按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndel_Click(sender As Object, e As EventArgs) Handles btndel.Click
        Dim list As ArrayList = New ArrayList
        Dim chk As CheckBox
        '多选框判定是否有选择的数据
        For Each item As GridViewRow In admin.Rows
            If item.RowType <> DataControlRowType.Header Then
                chk = item.FindControl("chkCheck")
                If chk.Checked Then
                    Dim str As String = item.Cells(1).Text
                    list.Add(str)
                End If
            End If
        Next
        '如果有选择的数据，执行delete方法
        If (list.Count > 0) Then
            delete(list)
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请选择要删除的数据')</script>")
        End If
    End Sub

    ''' <summary>
    ''' '删除方法
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub delete(ByVal lst As ArrayList)
        Dim bCommon As New BusinessCommon
        Dim SqlDbConn As SqlConnection
        '与数据库连接
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim sqlStr As String
        Dim myCmd As SqlCommand
        '多选循环
        For i = 0 To lst.Count - 1
            '将选择的用户，在用户表中的管理员权限列USER_AUTH_FLAG改为0，更新时间自动获取
            sqlStr = "UPDATE USER_INFO SET USER_AUTH_FLAG = '0',UPDATED_AT = GETDATE() WHERE USER_ID ='" & lst(i).ToString() & "'"
            myCmd = New SqlCommand(sqlStr, SqlDbConn)
            myCmd.ExecuteNonQuery()
        Next
        SqlDbConn.Close()
        '再检索
        Call bind()
    End Sub

    ''' <summary>
    ''' '检索出管理员信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Bind()
        '连接数据库
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        '按钮显示
        btndel.Visible = True
        btntj.Visible = False
        '按钮字体颜色
        btnadmin.ForeColor = Drawing.Color.Blue
        btnin.ForeColor = Drawing.Color.White
        '全选不点击
        chkAll.Checked = False
        '查询管理员信息
        strSql = "SELECT USER_ID,USER_FULLNAME,USER_CODE,CONVERT(DATE,APPLY_AT) AS APPLY_AT,CONVERT(DATE,JOIN_AT) AS JOIN_AT FROM USER_INFO WHERE USER_AUTH_FLAG = '1' AND VALID_FLAG = '0'" &
           "ORDER BY USER_CODE"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        '结果集赋值
        dataAdapter.Fill(ds, "USER_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            admin.DataSource = ds
            admin.DataBind()
        Else
            admin.DataSource = Nothing
            admin.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' 检索普通用户信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Bind1()
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        btndel.Visible = False
        btntj.Visible = True
        btnadmin.ForeColor = Drawing.Color.White
        btnin.ForeColor = Drawing.Color.Blue
        chkAll.Checked = False
        '查询用户表中普通用户信息
        strSql = "SELECT USER_ID,USER_FULLNAME,USER_CODE,CONVERT(DATE,APPLY_AT) AS APPLY_AT,CONVERT(DATE,JOIN_AT) AS JOIN_AT FROM USER_INFO WHERE USER_AUTH_FLAG = '0' AND VALID_FLAG = '0'" &
            "ORDER BY USER_CODE"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "USER_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            admin.DataSource = ds
            admin.DataBind()
        Else
            admin.DataSource = Nothing
            admin.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '添加权限按钮，
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btntj_Click(sender As Object, e As EventArgs) Handles btntj.Click
        Dim list As ArrayList = New ArrayList
        Dim chk As CheckBox
        '多选框
        For Each item As GridViewRow In admin.Rows
            If item.RowType <> DataControlRowType.Header Then
                chk = item.FindControl("chkCheck")
                If chk.Checked Then
                    Dim str As String = item.Cells(1).Text
                    list.Add(str)
                End If
            End If
        Next
        If (list.Count > 0) Then
            insert(list)
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请选择要添加的数据')</script>")
        End If
    End Sub

    ''' <summary>
    ''' '添加方法
    ''' </summary>
    ''' <param name="a"></param>
    ''' <remarks></remarks>
    Private Sub insert(ByVal a As ArrayList)
        Dim bCommon As New BusinessCommon
        Dim SqlDbConn As SqlConnection
        '与数据库连接
        SqlDbConn = bCommon.GetConnection()
        SqlDbConn.Open()
        Dim iRow As Integer = 0
        Dim sqlStr As String
        Dim myCmd As SqlCommand
        For i = 0 To a.Count - 1
            '将选择的用户，在用户表中的管理员权限列USER_AUTH_FLAG改为1，更新时间自动获取
            sqlStr = "UPDATE USER_INFO SET USER_AUTH_FLAG = '1',UPDATED_AT = GETDATE() WHERE USER_ID ='" & a(i).ToString() & "'"
            myCmd = New SqlCommand(sqlStr, SqlDbConn)
            myCmd.ExecuteNonQuery()
        Next
        SqlDbConn.Close()
        '再检索
        Call Bind()
    End Sub

    ''' <summary>
    '''  '返回按钮，返回管理员登录界面
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnback_Click(sender As Object, e As EventArgs) Handles btnback.Click
        Response.Redirect("right_manage.aspx")
    End Sub
End Class

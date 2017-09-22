Imports System.Data.SqlClient
Imports System.Data

''' 作者：单瑶
''' 网页：待审核页面
''' 内容：查看待审核，已经通过审核或者已经被拒绝审核的用户信息。处理审核
Partial Class shenhe
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' '载入界面，显示待审核的用户信息
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Call xin()
        End If
    End Sub

    ''' <summary>
    ''' '待审核按钮，功能和pageload一样
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btndsh_Click(sender As Object, e As EventArgs) Handles btndsh.Click
        Call xin()
    End Sub

    ''' <summary>
    ''' '通过审核按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btntg_Click(sender As Object, e As EventArgs) Handles btntg.Click
        Call Bind()
    End Sub

    ''' <summary>
    ''' '拒绝申请按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnjs_Click(sender As Object, e As EventArgs) Handles btnjs.Click
        Call Bind1()
    End Sub

    ''' <summary>
    ''' '通过审核按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnpass_Click(sender As Object, e As EventArgs) Handles btnpass.Click
        '定义一个数组
        Dim list As ArrayList = New ArrayList
        Dim chk As CheckBox
        '确定被选择的用户
        For Each item As GridViewRow In shmd.Rows
            If item.RowType <> DataControlRowType.Header Then
                chk = item.FindControl("chkCheck")
                '将已经选择的用户放入list列表
                If chk.Checked Then
                    Dim str As String = item.Cells(1).Text
                    list.Add(str)
                End If
            End If
        Next
        '判定list列表中是否存在数据
        If (list.Count > 0) Then
            pass(list)
            '如果数组中没有数据，跳异常
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请选择要审核的数据')</script>")
        End If
    End Sub

    ''' <summary>
    ''' '通过审核方法，更新数据库
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub pass(ByVal lst As ArrayList)
        Dim bCommon As New BusinessCommon
        Dim OleDbConn As SqlConnection
        '与数据链接
        OleDbConn = bCommon.GetConnection()
        OleDbConn.Open()
        Dim iRow As Integer = 0
        Dim sqlStr As String
        Dim myCmd As SqlCommand
        '将数组中传来的ID与数据库中ID匹配，之后实行sql语句，更新数据库
        For i = 0 To lst.Count - 1
            sqlStr = ""
            sqlStr = "UPDATE USER_REVIEW_INFO SET REVIEW_FLAG=1,REVIEW_USER_ID=1 WHERE REVIEW_ID = " & lst(i).ToString()         
            myCmd = New SqlCommand(sqlStr, OleDbConn)
            myCmd.ExecuteNonQuery()
        Next
        OleDbConn.Close()
        '再検索
        Call xin()
    End Sub

    ''' <summary>
    ''' '拒绝申请按钮，与通过按钮一样获取已选中的用户ID，之后实行jujue方法
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnjj_Click(sender As Object, e As EventArgs) Handles btnjj.Click
        Dim list As ArrayList = New ArrayList
        Dim chk As CheckBox
        For Each item As GridViewRow In shmd.Rows
            If item.RowType <> DataControlRowType.Header Then
                chk = item.FindControl("chkCheck")

                If chk.Checked Then
                    Dim str As String = item.Cells(1).Text
                    list.Add(str)
                End If
            End If
        Next
        '如果check被选择，执行jujue方法，如果没被选择弹出对话框
        If (list.Count > 0) Then
            jujue(list)
        Else
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "alert", "<script>alert('请选择要审核的数据')</script>")
        End If
    End Sub

    ''' <summary>
    ''' '拒绝申请方法
    ''' </summary>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub jujue(ByVal lst As ArrayList)
        Dim bCommon As New BusinessCommon
        Dim OleDbConn As SqlConnection
        '与数据??接
        OleDbConn = bCommon.GetConnection()
        OleDbConn.Open()
        Dim iRow As Integer = 0
        Dim sqlStr As String
        Dim myCmd As SqlCommand
        '多选循环
        For i = 0 To lst.Count - 1
            sqlStr = ""
            sqlStr = "UPDATE USER_REVIEW_INFO SET REVIEW_FLAG=2,REVIEW_USER_ID=1 WHERE REVIEW_ID = " & lst(i).ToString()
            myCmd = New SqlCommand(sqlStr, OleDbConn)
            myCmd.ExecuteNonQuery()
        Next
        OleDbConn.Close()
        '再检索
        Call xin()
    End Sub

    ''' <summary>
    ''' '全选按钮
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkAll.CheckedChanged
        Dim i As Integer
        Dim chk As CheckBox
        For i = 0 To Me.shmd.Rows.Count - 1
            chk = CType(Me.shmd.Rows(i).FindControl("chkCheck"), CheckBox)
            If (Me.chkAll.Checked) Then
                chk.Checked = True
            Else
                chk.Checked = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' '检索通过审核的用户名单
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Bind()
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon()
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        btnpass.Visible = False
        btnjj.Visible = False
        chkAll.Visible = False
        strSql = "select REVIEW_ID,OPEN_ID,USER_FULLNAME,USER_CODE,CONVERT(DATE,APPLY_AT) AS APPLY_AT,CONVERT(DATE,REVIEW_AT) AS REVIEW_AT,REVIEW_FLAG from USER_REVIEW_INFO where REVIEW_FLAG=1"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "user_review_info")
        If ds.Tables(0).Rows.Count > 0 Then
            shmd.DataSource = ds
            shmd.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '获取申请被拒绝的用户明单
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Bind1()
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon()
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        btnpass.Visible = False
        btnjj.Visible = False
        chkAll.Visible = False
        'sql语句
        strSql = "select REVIEW_ID,OPEN_ID,USER_FULLNAME,USER_CODE,CONVERT(DATE,APPLY_AT) AS APPLY_AT,CONVERT(DATE,REVIEW_AT) AS REVIEW_AT,REVIEW_FLAG from USER_REVIEW_INFO where REVIEW_FLAG=2"
        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, OleDbConn)
        dataAdapter.Fill(ds, "user_review_info")
        '如果检索之后有数据，表格数据等于检索出的数据
        If ds.Tables(0).Rows.Count > 0 Then
            shmd.DataSource = ds
            shmd.DataBind()
        End If
        dataAdapter.Dispose()
        ds.Dispose()
    End Sub

    ''' <summary>
    ''' '检索待审核用户信息
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub xin()
        '连接数据库
        Dim OleDbConn As SqlConnection
        Dim iRow As Integer = 0
        '建立连接对象
        Dim bCommon As New BusinessCommon
        OleDbConn = bCommon.GetConnection()
        Dim strSql As String
        chkAll.Visible = True
        'sql语句
        strSql = "select REVIEW_ID,OPEN_ID,USER_FULLNAME,USER_CODE,CONVERT(DATE,APPLY_AT) AS APPLY_AT,CONVERT(DATE,REVIEW_AT) AS REVIEW_AT,REVIEW_FLAG from USER_REVIEW_INFO where REVIEW_FLAG=0 "
        Dim ds As New DataSet()
        Dim sda As New SqlDataAdapter(strSql, OleDbConn)
        sda.Fill(ds, "USER_REVIEW_INFO")
        If ds.Tables(0).Rows.Count > 0 Then
            shmd.DataSource = ds
            shmd.DataBind()
            btnpass.Visible = True
            btnjj.Visible = True
        Else
            shmd.DataSource = Nothing
            shmd.DataBind()
            btnpass.Visible = False
            btnjj.Visible = False
        End If
        sda.Dispose()
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


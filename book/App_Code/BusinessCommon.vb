Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class BusinessCommon

    Dim SqlserverConn As SqlConnection
    Dim dbCommand As New SqlCommand
    Dim dataReader As SqlDataReader
    Public Structure empData
        Dim name As String
        Dim sex As String
        Dim dept As String
        Dim years As Integer
        Dim japanese As String
    End Structure

    Public Structure USER_INFOData
        Dim code As String
        Dim name As String
        Dim year As String
    End Structure

    Public Structure book_infoData
        Dim source_type_name As String
        Dim book_created_date As String
        Dim borrow_count As String
        Dim favorite_count As String
        Dim praise As String
        Dim comment_count As String
        Dim detail As String
        Dim imgCount As String
        Dim imgUrl As String

    End Structure

    Public Function getdata3(ByVal bookid As String) As book_infoData
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0
        Dim bCommon As New BusinessCommon()
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        Dim strSql As String
        strSql = " SELECT A.BOOK_NAME,G.SOURCE_TYPE_NAME,A.BOOK_BARCODE,A.BOOK_TYPE_ID," &
       "A.CREATED_AT AS BOOK_CREATED_DATE," &
       "(SELECT COUNT(*) FROM BOOK_BORROW_INFO C WHERE C.BOOK_ID=A.BOOK_ID) AS BORROW_COUNT," &
       "(SELECT COUNT(*) FROM BOOK_FAVORITE_INFO D WHERE D.BOOK_ID=A.BOOK_ID AND D.DEL_FLAG = '1') AS FAVORITE_COUNT," &
       "(SELECT COUNT(*) FROM BOOK_PRAISE_INFO E WHERE E.BOOK_ID=A.BOOK_ID) AS PRAISE_COUNT," &
       "(SELECT COUNT(*) FROM BOOK_COMMENT_INFO F WHERE F.BOOK_ID=A.BOOK_ID AND F.DEL_FLAG = '0') AS COMMENT_COUNT,	" &
       "A.BOOK_INTRO AS detail," &
       "A.BORROW_FLAG AS borrowEnable," &
            "(iif(isnull(A.PICTURE1,'')='',0,1)+" &
            "iif(isnull(A.PICTURE2,'')='',0,1)+" &
            "iif(isnull(A.PICTURE3,'')='',0,1)+" &
            "iif(isnull(A.PICTURE4,'')='',0,1)+" &
            "iif(isnull(A.PICTURE5,'')='',0,1)+" &
            "iif(isnull(A.PICTURE6,'')='',0,1)+" &
            "iif(isnull(A.PICTURE7,'')='',0,1)+" &
            "iif(isnull(A.PICTURE8,'')='',0,1)+" &
            "iif(isnull(A.PICTURE9,'')='',0,1))AS imgCount," &
            " A.PICTURE1 AS imgUrl" &
      " FROM  BOOK_INFO A " &
      "LEFT JOIN  BOOK_SOURCE_TYPE_INFO G  ON A.SOURCE_TYPE_ID = G.SOURCE_TYPE_ID " &
      "WHERE BOOK_ID= " & bookid

        Dim myCmd = New SqlCommand(strSql, SqlserverConn)
        myCmd.CommandType = System.Data.CommandType.Text

        Dim dataReader As SqlDataReader
        dataReader = myCmd.ExecuteReader()
        Dim data As book_infoData = New book_infoData
        While dataReader.Read
            data.source_type_name = CStr(dataReader("SOURCE_TYPE_NAME"))
            data.book_created_date = CStr(dataReader("BOOK_CREATED_DATE"))
            data.borrow_count = CStr(dataReader("BORROW_COUNT"))
            data.favorite_count = CInt(dataReader("FAVORITE_COUNT"))
            data.praise = CStr(dataReader("PRAISE_COUNT"))
            data.comment_count = CStr(dataReader("COMMENT_COUNT"))
            data.detail = CStr(dataReader("detail"))
            data.imgCount = CStr(dataReader("imgCount"))
            data.imgUrl = "images/" + CStr(dataReader("imgUrl"))

        End While
        dataReader.Close()
        myCmd.Dispose()
        SqlserverConn.Close()
        Return data

    End Function

    Public Function getDepta() As ArrayList
        Dim arrList As New ArrayList()

        Dim OleDbConn = GetConnection()

        Dim strSql As String
        strSql = "select distinct BORROW_DATE from BOOK_CORNER.dbo.BOOK_BORROW_INFO order by BORROW_DATE"
        dbCommand = New SqlCommand(strSql, OleDbConn)
        dbCommand.CommandType = System.Data.CommandType.Text

        OleDbConn.Open()
        '?数据??行??并得到?果。ExecuteReader 返回一个DataReader?象
        dataReader = dbCommand.ExecuteReader()
        'SDataReader.Read的?次?用都会从?果集中返回一行。
        While dataReader.Read
            'arrList.Add(dataReader("BORROW_DATEid") & ":" & dataReader("BORROW_DATE"))
            arrList.Add(dataReader("BORROW_DATE"))
        End While

        dataReader.Close()
        dbCommand.Dispose()
        OleDbConn.Close()

        getDepta = arrList
    End Function

    Public Function GetConnection() As SqlConnection
        'OleDb连接oracle数据库
        ' Dim strConn As String = "Provider=MSDAORA;Data Source=ORCL;User ID=test;Password=test;"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString").ToString()
        SqlserverConn = New SqlConnection(strConn)
        Return SqlserverConn
    End Function
    '取得
    Public Function getBookTypeInfo() As ArrayList
        Dim arrList As New ArrayList()

        SqlserverConn = GetConnection()

        Dim strSql As String
        strSql = "select * from BOOK_TYPE_INFO order by BOOK_TYPE_ID"
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = System.Data.CommandType.Text

        SqlserverConn.Open()
        '对数据库进行查询并得到结果。ExecuteReader 返回一个DataReader对象
        Try
            dataReader = dbCommand.ExecuteReader()
        Catch ex As Exception

        End Try

        'SDataReader.Read的每次调用都会从结果集中返回一行。
        While dataReader.Read
            arrList.Add(dataReader("BOOK_TYPE_ID") & ":" & dataReader("BOOK_TYPE_NAME"))
        End While

        dataReader.Close()
        dbCommand.Dispose()
        SqlserverConn.Close()

        Dim typeInfo As ArrayList = arrList
        Return typeInfo
    End Function

    Public Function getRirekiNo() As String
        Dim SqlserverConn As System.Data.SqlClient.SqlConnection
        Dim arrList As New ArrayList()

        SqlserverConn = GetConnection()

        Dim strSql As String
        strSql = "select lpad(rirekiNo.nextval,6,'0') from dual"
        Dim dbCommand As New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = CommandType.Text

        SqlserverConn.Open()

        Dim seq As String
        '执行查询, 并返回查询所返回的结果集中第1行的第1列
        seq = Convert.ToString(dbCommand.ExecuteScalar)
        dbCommand.Dispose()
        SqlserverConn.Close()

        getRirekiNo = seq
    End Function

    Public Function ExecScalar(ByVal strSql As String) As String
        Dim arrList As New ArrayList()

        SqlserverConn = GetConnection()
        'Dim dbCommand As New SqlCommand
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        'dbCommand.Connection = SqlserverConn
        dbCommand.CommandText = strSql
        If dbCommand.Connection.State <> ConnectionState.Open Then
            dbCommand.Connection.Open()
        End If

        ExecScalar = Convert.ToString(dbCommand.ExecuteScalar)
        dbCommand.Connection.Close()
    End Function

    Public Function search(ByVal cardno As String) As DataSet
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0

        Dim bCommon As New BusinessCommon()
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()
        Dim strSql As String

        strSql = "select EMPNO,ENAME,decode(SEX, 0, '男', 1, '女') SEX,decode(DEPTNO, 1, '1部', 2, '2部', 3, '3部', 'その他部門') DEPT,"
        strSql = strSql & " WORKYEARS, JAPANESE,JOB from emp  where del_flg = '0'"
        strSql = strSql & " and EMPNO='" & cardno & "'"
        strSql = strSql & " order by EMPNO"

        Dim ds As New DataSet()
        Dim dataAdapter As New SqlDataAdapter(strSql, SqlserverConn)
        dataAdapter.Fill(ds, "emp")

        dataAdapter.Dispose()
        SqlserverConn.Close()
        Return ds

    End Function

    Public Function getdata(ByVal cardno As String) As empData
        Dim SqlserverConn As SqlConnection
        Dim iRow As Integer = 0

        Dim bCommon As New BusinessCommon()
        SqlserverConn = bCommon.GetConnection()
        SqlserverConn.Open()

        Dim strSql As String
        strSql = "select EMPNO,ENAME,Case SEX When 0 Then  '男' when 1 then '女' else '' end SEX, DEPT.DEPTNM DEPT,"
        strSql = strSql & " WORKYEARS, JAPANESE from EMP left join DEPT on (DEPT.DEPTID=EMP.DEPTNO)  where DEL_FLG = '0'"
        strSql = strSql & " and EMPNO='" & cardno & "'"
        strSql = strSql & " order by EMPNO"

        Dim myCmd = New SqlCommand(strSql, SqlserverConn)
        myCmd.CommandType = System.Data.CommandType.Text

        Dim dataReader As SqlDataReader
        dataReader = myCmd.ExecuteReader()
        Dim data As empData = New empData

        While dataReader.Read

            data.name = CStr(dataReader("ENAME"))
            data.sex = CStr(dataReader("SEX"))
            data.dept = CStr(dataReader("DEPT"))
            data.years = CInt(dataReader("WORKYEARS"))
            data.japanese = CStr(dataReader("JAPANESE"))
        End While

        dataReader.Close()
        myCmd.Dispose()
        SqlserverConn.Close()
        Return data

    End Function

    Public Function getDeptb() As ArrayList
        Dim arrList As New ArrayList()
        SqlserverConn = GetConnection()
        Dim strSql As String
        strSql = "select BOOK_TYPE_NAME from BOOK_CORNER.dbo.BOOK_TYPE_INFO order by BOOK_TYPE_ID"
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = System.Data.CommandType.Text
        SqlserverConn.Open()
        '?数据??行??并得到?果。ExecuteReader 返回一个DataReader?象
        dataReader = dbCommand.ExecuteReader()
        While dataReader.Read
            arrList.Add(dataReader("BOOK_TYPE_NAME"))
        End While
        dataReader.Close()
        dbCommand.Dispose()
        SqlserverConn.Close()
        getDeptb = arrList
    End Function

    ''' <summary>
    ''' ?籍来源
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDeptc() As ArrayList
        Dim arrList As New ArrayList()
        SqlserverConn = GetConnection()
        Dim strSql As String
        strSql = "select SOURCE_TYPE_NAME from BOOK_CORNER.dbo.BOOK_SOURCE_TYPE_INFO order by SOURCE_TYPE_ID"
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = System.Data.CommandType.Text
        SqlserverConn.Open()
        '?数据??行??并得到?果。ExecuteReader 返回一个DataReader?象
        dataReader = dbCommand.ExecuteReader()
        While dataReader.Read
            arrList.Add(dataReader("SOURCE_TYPE_NAME"))
        End While
        dataReader.Close()
        dbCommand.Dispose()
        SqlserverConn.Close()
        getDeptc = arrList
    End Function


    ''' <summary>
    ''' 捐?人
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDeptd() As ArrayList
        Dim arrList As New ArrayList()
        SqlserverConn = GetConnection()
        Dim strSql As String
        strSql = "select distinct CONTRIBUTOR_ID from BOOK_CORNER.dbo.BOOK_INFO where SOURCE_TYPE_ID = 1 order by CONTRIBUTOR_ID"
        dbCommand = New SqlCommand(strSql, SqlserverConn)
        dbCommand.CommandType = System.Data.CommandType.Text
        SqlserverConn.Open()
        '?数据??行??并得到?果。ExecuteReader 返回一个DataReader?象
        dataReader = dbCommand.ExecuteReader()
        While dataReader.Read
            arrList.Add(dataReader("CONTRIBUTOR_ID"))
        End While
        dataReader.Close()
        dbCommand.Dispose()
        SqlserverConn.Close()
        getDeptd = arrList
    End Function

End Class


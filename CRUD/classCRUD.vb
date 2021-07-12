Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Data.DataRow
Public Class classCRUD
    Dim Cmd As New MySql.Data.MySqlClient.MySqlCommand
    Dim Ds As DataSet
    Dim Da As New MySql.Data.MySqlClient.MySqlDataAdapter
    Dim Rd As MySql.Data.MySqlClient.MySqlDataReader
    Dim MyDB As String

    Public connDB As New MySql.Data.MySqlClient.MySqlConnection

    Public Function Koneksi() As Integer
        Try
            Dim strServer As String = "localhost"
            Dim strDbase As String = "db_png" 'Database name
            Dim strUser As String = "root"  'Database user
            Dim strPass As String = ""     'Database password

            If connDB.State <> ConnectionState.Open Then
                connDB.ConnectionString = "server=" & strServer.Trim & ";database=" & strDbase.Trim & ";user=" & strUser.Trim & ";password=" & strPass
                connDB.Open()
            End If
            Return 1
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        End Try
    End Function

    Public Function KondisiAwal() As DataSet
        Da = New MySql.Data.MySqlClient.MySqlDataAdapter("select * from tbl_karyawan", connDB)
        Ds = New DataSet
        Da.Fill(Ds, "tbl_karyawan")
        Return Ds
    End Function
    'CREATE
    Public Function Create(ByVal badge As String, ByVal nama As String, ByVal departement As String, ByVal nohp As Integer)
        Call Koneksi()
        Dim InputData As String = "Insert into tbl_karyawan values ('" & badge & "','" & nama & "','" & departement & "','" & nohp & "')"
        Cmd = New MySql.Data.MySqlClient.MySqlCommand(InputData, connDB)
        Cmd.ExecuteNonQuery()
        MsgBox("Input data berhasil")
        connDB.Close()
    End Function
    'READ
    Public Function Read(ByVal badge As String) As DataTable
        Call Koneksi()
        Dim dt = New DataTable()
        Dim dr As DataRow
        dr = dt.NewRow

        Cmd = New MySql.Data.MySqlClient.MySqlCommand("Select * from tbl_karyawan where badge ='" & badge & "'", connDB)

        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Rd.HasRows Then
            dt.Columns.AddRange(New DataColumn(2) {New DataColumn("nama"), New DataColumn("departemen"), New DataColumn("nohp")})
            dr.Item("nama") = Rd.Item("namakaryawan")
            dr.Item("departemen") = Rd.Item("departemenkaryawan")
            dr.Item("nohp") = Rd.Item("telponkaryawan")
            dt.Rows.Add(dr)
            connDB.Close()
            Return dt
        End If
        connDB.Close()
        Return dt
    End Function
    'UPDATE
    Public Function Update(ByVal badge As String, ByVal nama As String, ByVal departement As String, ByVal nohp As Integer)
        Call Koneksi()
        Dim editdata As String = "update tbl_karyawan set namakaryawan='" & nama & "',departemenkaryawan='" & departement & "', telponkaryawan='" & nohp & "' where badge= '" & badge & "'"
        Cmd = New MySql.Data.MySqlClient.MySqlCommand(editdata, connDB)
        Cmd.ExecuteNonQuery()
        MsgBox("edit data berhasil")
        connDB.Close()
    End Function
    'DELETE
    Public Function Delete(ByVal badge As String)
        Call Koneksi()
        Dim HapusData As String = "Delete From tbl_karyawan where badge = '" & badge & "'"
        Cmd = New MySql.Data.MySqlClient.MySqlCommand(HapusData, connDB)
        Cmd.ExecuteNonQuery()
        connDB.Close()
        Return MsgBox("Hapus data berhasil")
    End Function
End Class

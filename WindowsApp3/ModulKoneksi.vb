Imports MySql.Data.MySqlClient

Module ModulKoneksi
    Public conn As MySqlConnection
    Public DA As MySqlDataAdapter
    Public DR As MySqlDataReader
    Public cmd As MySqlCommand
    Public DS As DataSet

    Public Sub KoneksiDB()
        Dim mysqlConn As String
        mysqlConn = "Server = localhost; Port=3306; Database=percobaan; Uid=root; Pwd="
        conn = New MySqlConnection(mysqlConn)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub

End Module

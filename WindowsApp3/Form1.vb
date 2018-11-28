Imports MySql.Data.MySqlClient

Public Class Form1
    Public thisTime, thisDate, number As String
    Public userArr(50), uDateArr(50), uTimeArr(50) As String
    Public drag As Boolean
    Public mousex, mousey As Integer
    Public reminderTime, reminderText As String
    Public remind_user As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Timer2.Enabled = True

        Call KoneksiDB()
        Call TampilRemindMe()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim waktu_user_ts As TimeSpan
        Dim waktu_user As String
        Dim tanggal_user As String

        thisTime = Format(Now, "HH:mm:ss")
        thisDate = Format(Now, "dd/MM/yyyy")
        timeGlobal.Text = thisTime
        dateGlobal.Text = Format(Now, "ddd") + ", " + thisDate

        Try
            For counter As Integer = 0 To DataGridView1.Rows.Count
                remind_user = DataGridView1.Rows(counter).Cells(1).Value
                tanggal_user = DataGridView1.Rows(counter).Cells(2).Value
                waktu_user_ts = DataGridView1.Rows(counter).Cells(3).Value
                waktu_user = waktu_user_ts.ToString

                If thisTime = waktu_user And thisDate = tanggal_user Then
                    reminderText = remind_user
                    Form2.Show()
                    Call HapusRemind()
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Sub HapusRemind()
        Try
            Call KoneksiDB()
            Dim intruksi_hapus = "DELETE FROM RemindTest where remind_me = '" & remind_user & "' "
            cmd = New MySqlCommand(intruksi_hapus, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub

    Sub AturDGV()
        Try
            DataGridView1.Columns(0).Width = 30
            DataGridView1.Columns(1).Width = 150
            DataGridView1.Columns(0).HeaderText = "No"
            DataGridView1.Columns(1).HeaderText = "Remind ME"
            DataGridView1.Columns(2).HeaderText = "Date"
            DataGridView1.Columns(3).HeaderText = "Time"
        Catch ex As Exception
        End Try
    End Sub

    Sub TampilRemindMe()
        DA = New MySqlDataAdapter("SELECT id, remind_me, date, time FROM RemindTest", conn)
        DS = New DataSet
        DA.Fill(DS, "RemindTest")
        DataGridView1.DataSource = DS.Tables("RemindTest")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim intruksi_database As String
            intruksi_database = "INSERT INTO RemindTest VALUES ('','" & TextBox1.Text & "', '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "', '" & DateTimePicker2.Value.ToString("HH:mm:ss") & "')"
            cmd = New MySqlCommand(intruksi_database, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Insert Data Success")
        Catch ex As Exception
            MsgBox("Insert Data Failed")
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Call KoneksiDB()
            Dim intruksi_update = "UPDATE RemindTest Set remind_me = '" & TextBox1.Text & "',date = '" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "', time= '" & DateTimePicker2.Value.ToString("HH:mm:ss") & "' Where id = '" & TextBox2.Text & "'"
            cmd = New MySqlCommand(intruksi_update, conn)
            cmd.ExecuteNonQuery()
            MsgBox("Update Data Success")
        Catch ex As Exception
            MsgBox("Update Data Failed")
        End Try
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Call TampilRemindMe()
        Call AturDGV()
    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Call KoneksiDB()
            Dim intruksi_hapus = "DELETE FROM RemindTest where id = '" & TextBox2.Text & "' "
            cmd = New MySqlCommand(intruksi_hapus, conn)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Top = Cursor.Position.Y - mousey
            Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Cursor.Position.X - Left 'Sets variable mousex
        mousey = Cursor.Position.Y - Top 'Sets variable mousey
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False 'Sets drag to false, so the form does not move according to the code in MouseMove
    End Sub
End Class

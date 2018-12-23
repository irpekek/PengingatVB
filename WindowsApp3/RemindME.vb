Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public Class RemindME
    Public thisTime, thisDate, thisDates As String
    Public drag As Boolean
    Public mousex, mousey As Integer
    Public reminderTime, reminderText, suara As String
    Public remind_user As String
    Public warnaSM As String
    Public webClient As New WebClient


    Public Function PostData(ByRef URL As String, ByRef POST As String, ByRef Cookies As CookieContainer) As String
        Dim request As HttpWebRequest
        Dim response As HttpWebResponse
        request = CType(WebRequest.Create(URL), HttpWebRequest)
        request.ContentType = "application/x-www-form-urlencoded"
        request.ContentLength = POST.Length
        request.Method = "POST"
        request.AllowAutoRedirect = False
        Dim requestStream As Stream = request.GetRequestStream()
        Dim postBytes As Byte() = Encoding.ASCII.GetBytes(POST)
        requestStream.Write(postBytes, 0, postBytes.Length)
        requestStream.Close()
        response = CType(request.GetResponse(), HttpWebResponse)
        Return New StreamReader(response.GetResponseStream()).ReadToEnd()
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Call minta()
        warnaSM = "light"
    End Sub

    Public Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim waktu_user As String
        Dim tanggal_user As String
        Dim id_data As String

        thisTime = Format(Now, "HH:mm:ss")
        thisDate = Format(Now, "dd/MM/yyyy")
        thisDates = Format(Now, "yyyy-MM-dd")
        timeGlobal.Text = thisTime
        dateGlobal.Text = Format(Now, "ddd") + ", " + thisDate

        Try
            For counter As Integer = 0 To DataGridView1.Rows.Count
                id_data = DataGridView1.Rows(counter).Cells(0).Value
                remind_user = DataGridView1.Rows(counter).Cells(1).Value
                tanggal_user = DataGridView1.Rows(counter).Cells(2).Value
                waktu_user = DataGridView1.Rows(counter).Cells(3).Value

                If thisTime = waktu_user And thisDates = tanggal_user Then
                    reminderText = remind_user
                    NotifME.Show()
                    DataGridView1.Rows.Clear()
                    Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&hapus&id=" & id_data)
                    DataGridView1.Rows.Clear()
                    Call minta()
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    Sub AturDGV()
        Try
            DataGridView1.Columns(0).Width = 30
            DataGridView1.Columns(1).Width = 220
            DataGridView1.Columns(2).Width = 85
            'DataGridView1.Columns(3).Width = 50
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles add_btn.Click
        Try
            If TextBox1.Text = "" Then
                MsgBox("Masukan Teks Reminder")
            Else
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&masuk&username=" & UserAuth.user_name & "&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
                MsgBox("Add Remind Success")
                DataGridView1.Rows.Clear()
                TextBox1.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Add Remind Failed")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles update_btn.Click

        Try
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MsgBox("Masukan Teks Reminder Dan Nomor")
            Else
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&update&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss") & "&id=" & TextBox2.Text)
                MsgBox("Update Remind Success")
                DataGridView1.Rows.Clear()
                TextBox1.Clear()
                TextBox2.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Update Remind Failed")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'MsgBox(PostData("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php", "?token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"), New CookieContainer))
        'MsgBox(PostData("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php", "token=9807&minta&id=1", New CookieContainer))
        Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
    End Sub

    Public Sub minta()
        Call AturDGV()
        Dim uriString As String = "http://unnamed48.ccug.gunadarma.ac.id/softskill/data2.php?minta&token=9807&username=" & UserAuth.user_name
        Dim uri As New Uri(uriString)

        'buat http request
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = "GET"

        'get http response
        Dim response As HttpWebResponse = request.GetResponse()

        'read http response
        Dim read = New StreamReader(response.GetResponseStream())
        Dim raw As String = read.ReadToEnd()

        Dim result = JsonConvert.DeserializeObject(raw)
        Dim path = "data[*]"

        Dim jResults = Newtonsoft.Json.Linq.JToken.Parse(raw)
        Dim query = jResults.SelectTokens(path)
        Dim count = query.Count() - 1
        For counter As Integer = 0 To count
            Dim idnya = result("data")(counter)("id")
            Dim remindnya = result("data")(counter)("remind_me")
            Dim tanggalnya = result("data")(counter)("tanggal")
            Dim waktunya = result("data")(counter)("waktu")
            Dim rnum As Integer = DataGridView1.Rows.Add()
            DataGridView1.Rows.Item(rnum).Cells("Column5").Value = idnya
            DataGridView1.Rows.Item(rnum).Cells("Column6").Value = remindnya
            DataGridView1.Rows.Item(rnum).Cells("Column7").Value = tanggalnya
            DataGridView1.Rows.Item(rnum).Cells("Column8").Value = waktunya
            DataGridView1.Update()
        Next
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        UserAuth.Show()
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles delete_btn.Click
        Try
            If TextBox2.Text = "" Then
                MsgBox("Masukan nomor yang akan di hapus")
            Else
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&hapus&id=" & TextBox2.Text)
                MsgBox("Delete Remind Success")
                DataGridView1.Rows.Clear()
                TextBox2.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Delete Remind Failed")
        End Try
    End Sub

    Private Sub UP_Click(sender As Object, e As EventArgs) Handles UP.Click
        If warnaSM = "light" Then
            If SlideMenu.Height = 29 Then
                SlideMenu.Height = 99
                SlideMenu.BackColor = Color.FromArgb(22, 102, 177)
                UP.ForeColor = Color.White
                timeGlobal.Visible = False
                dateGlobal.Visible = False
            Else
                SlideMenu.Height = 29
                SlideMenu.BackColor = Color.Transparent
                UP.ForeColor = Color.DodgerBlue
                timeGlobal.Visible = True
                dateGlobal.Visible = True
            End If
        Else
            If SlideMenu.Height = 29 Then
                SlideMenu.Height = 99
                SlideMenu.BackColor = Color.FromArgb(31, 31, 31)
                UP.ForeColor = Color.White
                timeGlobal.Visible = False
                dateGlobal.Visible = False
            Else
                SlideMenu.Height = 29
                SlideMenu.BackColor = Color.Transparent
                UP.ForeColor = Color.DodgerBlue
                timeGlobal.Visible = True
                dateGlobal.Visible = True
            End If
        End If


    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Top = Cursor.Position.Y - mousey
            Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        suara = "audio1"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        suara = "audio2"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        suara = "audio3"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        suara = "audio4"
    End Sub

    Private Sub Button5_MouseHover(sender As Object, e As EventArgs) Handles Button5.MouseHover
        My.Computer.Audio.Play(My.Resources.Argon, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub Button5_MouseLeave(sender As Object, e As EventArgs) Handles Button5.MouseLeave
        My.Computer.Audio.Stop()
    End Sub

    Private Sub Button6_MouseHover(sender As Object, e As EventArgs) Handles Button6.MouseHover
        My.Computer.Audio.Play(My.Resources.CyanAlarm, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub Button6_MouseLeave(sender As Object, e As EventArgs) Handles Button6.MouseLeave
        My.Computer.Audio.Stop()
    End Sub

    Private Sub Button7_MouseHover(sender As Object, e As EventArgs) Handles Button7.MouseHover
        My.Computer.Audio.Play(My.Resources.Siren, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub Button7_MouseLeave(sender As Object, e As EventArgs) Handles Button7.MouseLeave
        My.Computer.Audio.Stop()
    End Sub

    Private Sub Button8_MouseHover(sender As Object, e As EventArgs) Handles Button8.MouseHover
        My.Computer.Audio.Play(My.Resources.Platinum, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub Button8_MouseLeave(sender As Object, e As EventArgs) Handles Button8.MouseLeave
        My.Computer.Audio.Stop()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.BackColor = Color.White
        RectangleShape3.BackColor = Color.FromArgb(22, 102, 177)
        RectangleShape3.BorderColor = Color.FromArgb(22, 102, 177)
        Label1.BackColor = Color.FromArgb(22, 102, 177)
        Minimize_btn.BackColor = Color.FromArgb(22, 102, 177)
        Close_btn.BackColor = Color.FromArgb(22, 102, 177)
        TextBox1.BackColor = Color.White
        TextBox1.ForeColor = Color.Black
        TextBox2.BackColor = Color.White
        TextBox2.ForeColor = Color.Black
        RectangleShape1.BackColor = Color.White
        RectangleShape1.BorderColor = Color.FromArgb(22, 102, 177)
        RectangleShape4.BackColor = Color.White
        RectangleShape4.BorderColor = Color.FromArgb(22, 102, 177)
        Label3.ForeColor = Color.FromArgb(22, 102, 177)
        Label4.ForeColor = Color.FromArgb(22, 102, 177)
        RectangleShape2.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape2.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape5.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape5.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape6.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape6.BorderColor = Color.FromArgb(78, 184, 206)
        add_btn.BackColor = Color.FromArgb(78, 184, 206)
        delete_btn.BackColor = Color.FromArgb(78, 184, 206)
        update_btn.BackColor = Color.FromArgb(78, 184, 206)
        add_btn.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        delete_btn.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        update_btn.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(242, 242, 242)
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(242, 242, 242)
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black
        DataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black
        timeGlobal.ForeColor = Color.Black
        dateGlobal.ForeColor = Color.Black
        Button5.BackColor = Color.FromArgb(78, 184, 206)
        Button6.BackColor = Color.FromArgb(78, 184, 206)
        Button7.BackColor = Color.FromArgb(78, 184, 206)
        Button8.BackColor = Color.FromArgb(78, 184, 206)
        Button9.BackColor = Color.FromArgb(78, 184, 206)
        Button10.BackColor = Color.FromArgb(78, 184, 206)
        Button5.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        Button6.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        Button7.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        Button8.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        Button9.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        Button10.FlatAppearance.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape7.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape8.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape9.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape10.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape11.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape12.BackColor = Color.FromArgb(78, 184, 206)
        RectangleShape7.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape8.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape9.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape10.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape11.BorderColor = Color.FromArgb(78, 184, 206)
        RectangleShape12.BorderColor = Color.FromArgb(78, 184, 206)
        SlideMenu.BackColor = Color.FromArgb(22, 102, 177)
        DataGridView1.BackgroundColor = Color.White
        warnaSM = "light"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.BackColor = Color.FromArgb(0, 0, 0)
        RectangleShape3.BackColor = Color.FromArgb(31, 31, 31)
        RectangleShape3.BorderColor = Color.FromArgb(31, 31, 31)
        Label1.BackColor = Color.FromArgb(31, 31, 31)
        Minimize_btn.BackColor = Color.FromArgb(31, 31, 31)
        Close_btn.BackColor = Color.FromArgb(31, 31, 31)
        TextBox1.BackColor = Color.FromArgb(18, 18, 18)
        TextBox1.ForeColor = Color.FromArgb(199, 196, 196)
        TextBox2.BackColor = Color.FromArgb(18, 18, 18)
        TextBox2.ForeColor = Color.FromArgb(199, 196, 196)
        RectangleShape1.BackColor = Color.FromArgb(18, 18, 18)
        RectangleShape1.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape4.BackColor = Color.FromArgb(18, 18, 18)
        RectangleShape4.BorderColor = Color.FromArgb(255, 65, 129)
        Label3.ForeColor = Color.FromArgb(255, 65, 129)
        Label4.ForeColor = Color.FromArgb(255, 65, 129)
        RectangleShape2.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape2.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape5.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape5.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape6.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape6.BorderColor = Color.FromArgb(255, 65, 129)
        add_btn.BackColor = Color.FromArgb(255, 65, 129)
        delete_btn.BackColor = Color.FromArgb(255, 65, 129)
        update_btn.BackColor = Color.FromArgb(255, 65, 129)
        add_btn.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        delete_btn.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        update_btn.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(18, 18, 18)
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(18, 18, 18)
        DataGridView1.DefaultCellStyle.SelectionForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Black
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.Black
        DataGridView1.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White
        timeGlobal.ForeColor = Color.White
        dateGlobal.ForeColor = Color.White
        Button5.BackColor = Color.FromArgb(255, 65, 129)
        Button6.BackColor = Color.FromArgb(255, 65, 129)
        Button7.BackColor = Color.FromArgb(255, 65, 129)
        Button8.BackColor = Color.FromArgb(255, 65, 129)
        Button9.BackColor = Color.FromArgb(255, 65, 129)
        Button10.BackColor = Color.FromArgb(255, 65, 129)
        Button5.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        Button6.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        Button7.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        Button8.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        Button9.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        Button10.FlatAppearance.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape7.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape8.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape9.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape10.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape11.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape12.BackColor = Color.FromArgb(255, 65, 129)
        RectangleShape7.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape8.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape9.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape10.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape11.BorderColor = Color.FromArgb(255, 65, 129)
        RectangleShape12.BorderColor = Color.FromArgb(255, 65, 129)
        SlideMenu.BackColor = Color.FromArgb(31, 31, 31)
        warnaSM = "dark"
        DataGridView1.BackgroundColor = Color.Black
    End Sub

    Private Sub Minimize_btn_Click(sender As Object, e As EventArgs) Handles Minimize_btn.Click
        WindowState = FormWindowState.Minimized
        If WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            NotifyIcon1.BalloonTipIcon = ToolTipIcon.Info
            NotifyIcon1.BalloonTipText = "Your Reminder"
            NotifyIcon1.ShowBalloonTip(500)
            ShowInTaskbar = False
        End If
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        ShowInTaskbar = True
        Me.WindowState = FormWindowState.Normal
        NotifyIcon1.Visible = False
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

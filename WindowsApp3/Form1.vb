Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form1
    Public thisTime, thisDate, thisDates As String
    Public drag As Boolean
    Public mousex, mousey As Integer
    Public reminderTime, reminderText, suara As String
    Public remind_user As String


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
        Call pembuats()
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
                    Form2.Show()
                    DataGridView1.Rows.Clear()
                    Dim webClient As New WebClient
                    Dim results As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&hapus&id=" & id_data)
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
            DataGridView1.Columns(1).Width = 200
            DataGridView1.Columns(2).Width = 80
            DataGridView1.Columns(3).Width = 80
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Then
                MsgBox("Masukan Teks Reminder")
            Else
                Dim webClient As New WebClient
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
                MsgBox("Insert Data Success")
                DataGridView1.Rows.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Insert Data Failed")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try
            If TextBox1.Text = "" And TextBox2.Text = "" Then
                MsgBox("Masukan Teks Reminder Dan Nomor")
            Else
                Dim webClient As New WebClient
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&update&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss") & "&id=" & TextBox2.Text)
                MsgBox("Update Data Success")
                DataGridView1.Rows.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Update Data Failed")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'MsgBox(PostData("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php", "?token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"), New CookieContainer))
        'MsgBox(PostData("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php", "token=9807&minta&id=1", New CookieContainer))
        'GetData("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php??token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
        'Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php??token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&masuk&remind=" & TextBox1.Text & "&tanggal=" & DateTimePicker1.Value.ToString("yyyy-MM-dd") & "&waktu=" & DateTimePicker2.Value.ToString("HH:mm:ss"))
        MsgBox("SEND SUKSES CUK")
    End Sub

    Public Sub minta()
        Call AturDGV()
        Dim uriString As String = "http://unnamed48.ccug.gunadarma.ac.id/softskill/data2.php?minta&token=9807"
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
        Dim idnya(count), remindnya(count), tanggalnya(count), waktunya(count) As String
        For counter As Integer = 0 To count
            idnya(counter) = result("data")(counter)("id")
            TextBox3.Text = idnya(counter)
            remindnya(counter) = result("data")(counter)("remind_me")
            tanggalnya(counter) = result("data")(counter)("tanggal")
            waktunya(counter) = result("data")(counter)("waktu")
            Dim rnum As Integer = DataGridView1.Rows.Add()
            DataGridView1.Rows.Item(rnum).Cells("Column5").Value = idnya(counter)
            DataGridView1.Rows.Item(rnum).Cells("Column6").Value = remindnya(counter)
            DataGridView1.Rows.Item(rnum).Cells("Column7").Value = tanggalnya(counter)
            DataGridView1.Rows.Item(rnum).Cells("Column8").Value = waktunya(counter)
            DataGridView1.Update()
            TextBox3.Text += idnya(counter) & "|" & remindnya(counter) & "|" & tanggalnya(counter) & "|" & waktunya(counter) & vbNewLine
        Next
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If TextBox2.Text = "" Then
                MsgBox("Masukan nomor yang akan di hapus")
            Else
                Dim webClient As New WebClient
                Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/data.php?token=9807&hapus&id=" & TextBox2.Text)
                MsgBox("Delete Data Success")
                DataGridView1.Rows.Clear()
                Call minta()
            End If
        Catch ex As Exception
            MsgBox("Delete Data Failed")
        End Try
    End Sub

    Private Sub UP_Click(sender As Object, e As EventArgs) Handles UP.Click
        If SlideMenu.Height = 29 Then
            SlideMenu.Height = 256
            SlideMenu.BackColor = Color.FromArgb(22, 102, 177)
            timeGlobal.ForeColor = Color.White
            dateGlobal.ForeColor = Color.White
            UP.ForeColor = Color.White
        Else
            SlideMenu.Height = 29
            SlideMenu.BackColor = Color.Transparent
            UP.ForeColor = Color.DodgerBlue
            timeGlobal.ForeColor = Color.Black
            dateGlobal.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Top = Cursor.Position.Y - mousey
            Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Audio2_Click(sender As Object, e As EventArgs) Handles Audio2.Click
        suara = "audio2"
    End Sub

    Private Sub Audio3_Click(sender As Object, e As EventArgs) Handles Audio3.Click
        suara = "audio3"
    End Sub

    Private Sub RectangleShape3_MouseUp(sender As Object, e As MouseEventArgs) Handles RectangleShape3.MouseUp
        drag = False 'Sets drag to false, so the form does not move according to the code in MouseMove
    End Sub

    Private Sub RectangleShape3_MouseDown(sender As Object, e As MouseEventArgs) Handles RectangleShape3.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Cursor.Position.X - Left 'Sets variable mousex
        mousey = Cursor.Position.Y - Top 'Sets variable mousey
    End Sub

    Private Sub RectangleShape3_MouseMove(sender As Object, e As MouseEventArgs) Handles RectangleShape3.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Top = Cursor.Position.Y - mousey
            Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Public Sub Audio1_Click(sender As Object, e As EventArgs) Handles Audio1.Click
        suara = "audio1"
    End Sub

    Private Sub Minimize_btn_Click(sender As Object, e As EventArgs) Handles Minimize_btn.Click
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

    Private Sub pembuats()
        Pembuat.Text = vbNewLine & "CREATOR" & vbNewLine & vbNewLine & "Sandy Laksana" & vbNewLine & "Gupy Wantoro" & vbNewLine & "Satria" & vbNewLine & "Rizka A." & vbNewLine & "Frida"
    End Sub
End Class

Imports System.IO
Imports System.Net
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Public Class Form3
    Public user_name As String
    Public drag As Boolean
    Public mousex, mousey As Integer

    Private Sub Form3_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        'If drag is set to true then move the form accordingly.
        If drag Then
            Top = Cursor.Position.Y - mousey
            Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form3_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Cursor.Position.X - Left 'Sets variable mousex
        mousey = Cursor.Position.Y - Top 'Sets variable mousey
    End Sub

    Private Sub Form3_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False 'Sets drag to false, so the form does not move according to the code in MouseMove
    End Sub

    Private Sub signup_btn_Click(sender As Object, e As EventArgs) Handles signup_btn.Click
        If user_text.Text = "" Or pass_text.Text = "" Then
            error_text.Text = "input username or password"
            error_text.Visible = True
        Else
            Dim webClient As New WebClient
            Dim result As String = webClient.DownloadString("http://unnamed48.ccug.gunadarma.ac.id/softskill/user.php?token=9807&buat&username=" & user_text.Text & "&password=" & GetHash(pass_text.Text))
            error_text.Text = "sign up success"
            error_text.Visible = True
        End If
    End Sub

    Shared Function GetHash(theInput As String) As String

        Using hasher As MD5 = MD5.Create()    ' create hash object

            ' Convert to byte array and get hash
            Dim dbytes As Byte() = hasher.ComputeHash(Encoding.UTF8.GetBytes(theInput))

            ' sb to create string from bytes
            Dim sBuilder As New StringBuilder()

            ' convert byte data to hex string
            For n As Integer = 0 To dbytes.Length - 1
                sBuilder.Append(dbytes(n).ToString("X2"))
            Next n

            Return sBuilder.ToString()
        End Using

    End Function

    Private Sub login_btn_Click(sender As Object, e As EventArgs) Handles login_btn.Click
        Dim uriString As String = "http://unnamed48.ccug.gunadarma.ac.id/softskill/user.php?cocokin&token=9807&username=" & user_text.Text & "&password=" & GetHash(pass_text.Text)
        Dim uri As New Uri(uriString)

        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = "GET"

        Dim response As HttpWebResponse = request.GetResponse()

        Dim read = New StreamReader(response.GetResponseStream())
        Dim raw As String = read.ReadToEnd()

        Dim result = JsonConvert.DeserializeObject(raw)

        If user_text.Text = "" Or pass_text.Text = "" Then
            error_text.Text = "input username or password"
            error_text.Visible = True
        Else
            If result.item("message") = "true" Then
                user_name = user_text.Text
                error_text.Visible = False
                Form1.Show()
                user_text.Clear()
                pass_text.Clear()
                Me.Hide()
            Else
                error_text.Text = "username and password do not match"
                error_text.Visible = True
                user_text.Clear()
                pass_text.Clear()
                user_text.Select()
            End If
        End If
    End Sub

    Private Sub Close_btn_Click(sender As Object, e As EventArgs) Handles Close_btn.Click
        Close()
    End Sub

    Private Sub Minimize_btn_Click(sender As Object, e As EventArgs) Handles Minimize_btn.Click
        WindowState = FormWindowState.Minimized
    End Sub
End Class
Public Class Form2

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = Form1.reminderText
        Label2.Text = Form1.thisTime
        If Form1.suara = "audio1" Then
            My.Computer.Audio.Play(My.Resources.Argon, AudioPlayMode.BackgroundLoop)
        ElseIf Form1.suara = "audio2" Then
            My.Computer.Audio.Play(My.Resources.CyanAlarm, AudioPlayMode.BackgroundLoop)
        ElseIf Form1.suara = "audio3" Then
            My.Computer.Audio.Play(My.Resources.Siren, AudioPlayMode.BackgroundLoop)
        Else
            My.Computer.Audio.Play(My.Resources.Platinum, AudioPlayMode.BackgroundLoop)
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Close()
        My.Computer.Audio.Stop()
    End Sub

    Private Sub RectangleShape1_Click(sender As Object, e As EventArgs) Handles RectangleShape1.Click

    End Sub
End Class
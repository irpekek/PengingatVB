Public Class NotifME

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Label1.Text = RemindME.reminderText
        Label2.Text = RemindME.thisTime
        If RemindME.suara = "audio1" Then
            My.Computer.Audio.Play(My.Resources.Argon, AudioPlayMode.BackgroundLoop)
        ElseIf RemindME.suara = "audio2" Then
            My.Computer.Audio.Play(My.Resources.CyanAlarm, AudioPlayMode.BackgroundLoop)
        ElseIf RemindME.suara = "audio3" Then
            My.Computer.Audio.Play(My.Resources.Siren, AudioPlayMode.BackgroundLoop)
        Else
            My.Computer.Audio.Play(My.Resources.Platinum, AudioPlayMode.BackgroundLoop)
        End If
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Close()
        My.Computer.Audio.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If RemindME.warnaSM = "light" Then
            Label1.BackColor = Color.White
            Label1.ForeColor = Color.Black
            Label2.BackColor = Color.White
            Label2.ForeColor = Color.Black
            Label4.ForeColor = Color.White
            Label4.BackColor = Color.FromArgb(22, 102, 177)
            RectangleShape1.BackColor = Color.White
            RectangleShape1.BorderColor = Color.White
            Me.BackColor = Color.FromArgb(22, 102, 177)
        Else
            Label1.BackColor = Color.FromArgb(31, 31, 31)
            Label1.ForeColor = Color.White
            Label2.BackColor = Color.FromArgb(31, 31, 31)
            Label2.ForeColor = Color.White
            Label4.ForeColor = Color.White
            Label4.BackColor = Color.Black
            RectangleShape1.BackColor = Color.FromArgb(31, 31, 31)
            RectangleShape1.BorderColor = Color.FromArgb(31, 31, 31)
            Me.BackColor = Color.Black
        End If
    End Sub
End Class
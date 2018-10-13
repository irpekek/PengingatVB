Public Class Form2

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = Form1.reminderText
        Label2.Text = Form1.thisTime
        My.Computer.Audio.Play(My.Resources.Siren, AudioPlayMode.Background)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Close()
    End Sub
End Class
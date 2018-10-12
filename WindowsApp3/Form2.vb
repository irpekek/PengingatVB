Public Class Form2

    Public labelAlarmNya As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        labelAlarm.Text = labelAlarmNya
        My.Computer.Audio.Play(My.Resources.Siren, AudioPlayMode.Background)
    End Sub
End Class
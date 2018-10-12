Public Class Form1
    Public thisTime, thisDate As String
    Public userDate, userTime As String
    Dim userArr(50) As String
    Public uDateArr(50) As String
    Public uTimeArr(50) As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        thisTime = Format(Now, "HH:mm:ss")
        thisDate = Format(Now, "dd/MM/yy")
        timeGlobal.Text = thisTime
        dateGlobal.Text = Format(Now, "ddd") + ", " + thisDate

        For i As Integer = 0 To 50
            If thisTime = uTimeArr(i) And thisDate = uDateArr(i) Then
                Form2.labelAlarmNya = TextBox1.Text
                Form2.Show()
            End If
        Next


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.Text <> "" Then
            For i As Integer = 0 To 50
                If userArr(i) = "" Then
                    userArr(i) = TextBox1.Text
                    uDateArr(i) = DateTimePicker1.Value.ToString("dd/MM/yy")
                    uTimeArr(i) = DateTimePicker2.Value.ToString("HH:mm:ss")
                    ListBox1.Items.Add(userArr(i))
                    ListBox1.Items.Add("Date :" + uDateArr(i) + Chr(9) + "Time :" + uTimeArr(i))
                    ListBox1.Items.Add("---------------------------------------------------------------------")
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles timeGlobal.Click

    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub
End Class

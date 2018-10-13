Public Class Form1
    Public thisTime, thisDate As String
    Public userDate, userTime As String
    Dim userArr(50) As String
    Public uDateArr(50) As String
    Public uTimeArr(50) As String
    Public number As String

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
        Dim i As Integer

        If TextBox1.Text <> "" Then
            i = 0
            For i = 0 To 50
                If userArr(i) = "" Then
                    userArr(i) = TextBox1.Text
                    uDateArr(i) = DateTimePicker1.Value.ToString("dd/MM/yy")
                    uTimeArr(i) = DateTimePicker2.Value.ToString("HH:mm:ss")
                    ListBox1.Items.Add(userArr(i) + Chr(9) + "Date :" + uDateArr(i) + Chr(9) + "Time :" + uTimeArr(i))
                    TextBox1.Text = ""
                    Exit Sub
                End If
            Next
        End If

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles timeGlobal.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim userRemove As Integer

        If TextBox2.Text = "" Then
            MsgBox("Input some value")
        Else
            userRemove = CInt(TextBox2.Text)
            If ListBox1.Items.Count >= 0 And userRemove <= ListBox1.Items.Count Then
                ListBox1.Items.RemoveAt(userRemove - 1)
                TextBox2.Text = ""
            End If
        End If
    End Sub

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.timeGlobal = New System.Windows.Forms.Label()
        Me.dateGlobal = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'timeGlobal
        '
        Me.timeGlobal.AutoSize = True
        Me.timeGlobal.Location = New System.Drawing.Point(310, 9)
        Me.timeGlobal.Name = "timeGlobal"
        Me.timeGlobal.Size = New System.Drawing.Size(56, 13)
        Me.timeGlobal.TabIndex = 0
        Me.timeGlobal.Text = "timeGlobal"
        '
        'dateGlobal
        '
        Me.dateGlobal.AutoSize = True
        Me.dateGlobal.Location = New System.Drawing.Point(12, 9)
        Me.dateGlobal.Name = "dateGlobal"
        Me.dateGlobal.Size = New System.Drawing.Size(58, 13)
        Me.dateGlobal.TabIndex = 3
        Me.dateGlobal.Text = "dateGlobal"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(11, 35)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(271, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(292, 35)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 20)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Add"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(292, 61)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(57, 20)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Remove"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(11, 61)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(130, 20)
        Me.DateTimePicker1.TabIndex = 18
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker2.Location = New System.Drawing.Point(148, 61)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.ShowUpDown = True
        Me.DateTimePicker2.Size = New System.Drawing.Size(72, 20)
        Me.DateTimePicker2.TabIndex = 19
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(226, 61)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(56, 20)
        Me.TextBox2.TabIndex = 25
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.HorizontalScrollbar = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 107)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListBox1.Size = New System.Drawing.Size(337, 108)
        Me.ListBox1.TabIndex = 24
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 240)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.dateGlobal)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.timeGlobal)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents timeGlobal As Label
    Friend WithEvents dateGlobal As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ListBox1 As ListBox
End Class

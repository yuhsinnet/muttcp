<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
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

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TGIP1 = New System.Windows.Forms.TextBox()
        Me.TGPort1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BindPort1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.MonitorText = New System.Windows.Forms.TextBox()
        Me.SendTestButton = New System.Windows.Forms.Button()
        Me.SendTextBox = New System.Windows.Forms.TextBox()
        Me.CheckTextBox = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GetQ = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TGIP1
        '
        Me.TGIP1.Location = New System.Drawing.Point(282, 29)
        Me.TGIP1.Name = "TGIP1"
        Me.TGIP1.Size = New System.Drawing.Size(100, 22)
        Me.TGIP1.TabIndex = 0
        '
        'TGPort1
        '
        Me.TGPort1.Location = New System.Drawing.Point(282, 57)
        Me.TGPort1.Name = "TGPort1"
        Me.TGPort1.Size = New System.Drawing.Size(100, 22)
        Me.TGPort1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(237, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "目標IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(218, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "目標PORT"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(388, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "<-->"
        '
        'BindPort1
        '
        Me.BindPort1.Location = New System.Drawing.Point(483, 45)
        Me.BindPort1.Name = "BindPort1"
        Me.BindPort1.Size = New System.Drawing.Size(100, 22)
        Me.BindPort1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(419, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "綁定PORT"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(187, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 12)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "<-->"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(128, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "目標設備"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(589, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(25, 12)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "<-->"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(620, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "連入設備"
        '
        'MonitorText
        '
        Me.MonitorText.Location = New System.Drawing.Point(130, 99)
        Me.MonitorText.Multiline = True
        Me.MonitorText.Name = "MonitorText"
        Me.MonitorText.Size = New System.Drawing.Size(543, 292)
        Me.MonitorText.TabIndex = 11
        '
        'SendTestButton
        '
        Me.SendTestButton.Location = New System.Drawing.Point(493, 397)
        Me.SendTestButton.Name = "SendTestButton"
        Me.SendTestButton.Size = New System.Drawing.Size(75, 23)
        Me.SendTestButton.TabIndex = 12
        Me.SendTestButton.Text = "SEND Test"
        Me.SendTestButton.UseVisualStyleBackColor = True
        '
        'SendTextBox
        '
        Me.SendTextBox.Location = New System.Drawing.Point(130, 397)
        Me.SendTextBox.Name = "SendTextBox"
        Me.SendTextBox.Size = New System.Drawing.Size(309, 22)
        Me.SendTextBox.TabIndex = 13
        '
        'CheckTextBox
        '
        Me.CheckTextBox.Location = New System.Drawing.Point(445, 397)
        Me.CheckTextBox.Name = "CheckTextBox"
        Me.CheckTextBox.Size = New System.Drawing.Size(42, 22)
        Me.CheckTextBox.TabIndex = 14
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(745, 12)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(196, 379)
        Me.TextBox1.TabIndex = 15
        '
        'GetQ
        '
        Me.GetQ.Location = New System.Drawing.Point(745, 396)
        Me.GetQ.Name = "GetQ"
        Me.GetQ.Size = New System.Drawing.Size(75, 23)
        Me.GetQ.TabIndex = 16
        Me.GetQ.Text = "Get Q"
        Me.GetQ.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 450)
        Me.Controls.Add(Me.GetQ)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.CheckTextBox)
        Me.Controls.Add(Me.SendTextBox)
        Me.Controls.Add(Me.SendTestButton)
        Me.Controls.Add(Me.MonitorText)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BindPort1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TGPort1)
        Me.Controls.Add(Me.TGIP1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TGIP1 As TextBox
    Friend WithEvents TGPort1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents BindPort1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents MonitorText As TextBox
    Friend WithEvents SendTestButton As Button
    Friend WithEvents SendTextBox As TextBox
    Friend WithEvents CheckTextBox As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GetQ As Button
End Class

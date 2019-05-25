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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TGPort = New System.Windows.Forms.TextBox()
        Me.MYPort = New System.Windows.Forms.TextBox()
        Me.AutoMode = New System.Windows.Forms.CheckBox()
        Me.Start_Button = New System.Windows.Forms.Button()
        Me.STOP_Button = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("新細明體", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Label1.Location = New System.Drawing.Point(63, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(456, 34)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "目標設備端口  <->    本幾端口"
        '
        'TGPort
        '
        Me.TGPort.Font = New System.Drawing.Font("新細明體", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.TGPort.Location = New System.Drawing.Point(69, 114)
        Me.TGPort.Name = "TGPort"
        Me.TGPort.Size = New System.Drawing.Size(201, 47)
        Me.TGPort.TabIndex = 1
        '
        'MYPort
        '
        Me.MYPort.Font = New System.Drawing.Font("新細明體", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.MYPort.Location = New System.Drawing.Point(362, 114)
        Me.MYPort.Name = "MYPort"
        Me.MYPort.Size = New System.Drawing.Size(201, 47)
        Me.MYPort.TabIndex = 2
        '
        'AutoMode
        '
        Me.AutoMode.AutoSize = True
        Me.AutoMode.Font = New System.Drawing.Font("新細明體", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.AutoMode.Location = New System.Drawing.Point(580, 44)
        Me.AutoMode.Name = "AutoMode"
        Me.AutoMode.Size = New System.Drawing.Size(173, 28)
        Me.AutoMode.TabIndex = 3
        Me.AutoMode.Text = "自動啟動模式"
        Me.AutoMode.UseVisualStyleBackColor = True
        '
        'Start_Button
        '
        Me.Start_Button.Font = New System.Drawing.Font("新細明體", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.Start_Button.Location = New System.Drawing.Point(580, 114)
        Me.Start_Button.Name = "Start_Button"
        Me.Start_Button.Size = New System.Drawing.Size(124, 47)
        Me.Start_Button.TabIndex = 4
        Me.Start_Button.Text = "啟動"
        Me.Start_Button.UseVisualStyleBackColor = True
        '
        'STOP_Button
        '
        Me.STOP_Button.Font = New System.Drawing.Font("新細明體", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(136, Byte))
        Me.STOP_Button.Location = New System.Drawing.Point(580, 167)
        Me.STOP_Button.Name = "STOP_Button"
        Me.STOP_Button.Size = New System.Drawing.Size(124, 47)
        Me.STOP_Button.TabIndex = 5
        Me.STOP_Button.Text = "暫停"
        Me.STOP_Button.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.STOP_Button)
        Me.Controls.Add(Me.Start_Button)
        Me.Controls.Add(Me.AutoMode)
        Me.Controls.Add(Me.MYPort)
        Me.Controls.Add(Me.TGPort)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "雙伺服多工"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TGPort As TextBox
    Friend WithEvents MYPort As TextBox
    Friend WithEvents AutoMode As CheckBox
    Friend WithEvents Start_Button As Button
    Friend WithEvents STOP_Button As Button
End Class

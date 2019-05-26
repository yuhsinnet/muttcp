Imports System.ComponentModel

Public Class Form1


    WithEvents DulSer As MTTCP

    Private Sub AutoMode_CheckedChanged(sender As Object, e As EventArgs) Handles AutoMode.CheckedChanged
        SaveSetting()

    End Sub

    Private Sub SaveSetting()
        My.Settings.TGP = TGPort.Text
        My.Settings.MP = MYPort.Text
        My.Settings.AutoStart = AutoMode.Checked

        My.Settings.Save()
    End Sub

    Private Sub Start_Button_Click(sender As Object, e As EventArgs) Handles Start_Button.Click
        StartService()

    End Sub

    Private Sub StartService()
        STOP_Button.Enabled = True
        Start_Button.Enabled = False

        DulSer = New MTTCP(CInt(TGPort.Text), CInt(MYPort.Text))
    End Sub

    Private Sub STOP_Button_Click(sender As Object, e As EventArgs) Handles STOP_Button.Click

        STOP_Button.Enabled = False
        Start_Button.Enabled = True
        DulSer.Close()

    End Sub

    Sub PrintText(Text As String) Handles DulSer.PrintText

        SetText(Text & Format(Now, "hh時mm分ss.fff秒") & vbCrLf)
        'GetQu()


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        STOP_Button.Enabled = False


        TGPort.Text = My.Settings.TGP
        MYPort.Text = My.Settings.MP
        AutoMode.Checked = My.Settings.AutoStart


        If AutoMode.Checked Then

            StartService()

        End If


    End Sub

    Delegate Sub SetTextCallback([text] As String)
    ' This method demonstrates a pattern for making thread-safe
    ' calls on a Windows Forms control. 
    '
    ' If the calling thread is different from the thread that
    ' created the TextBox control, this method creates a
    ' SetTextCallback and calls itself asynchronously using the
    ' Invoke method.
    '
    ' If the calling thread is the same as the thread that created
    ' the TextBox control, the Text property is set directly. 
    Private Sub SetText(ByVal [text] As String)

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.MonitorText.InvokeRequired Then
            Dim d As New SetTextCallback(AddressOf SetText)
            Me.Invoke(d, New Object() {[text]})
        Else
            Me.MonitorText.AppendText([text] & vbCrLf)
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        SaveSetting()



    End Sub
End Class

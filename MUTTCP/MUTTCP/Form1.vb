Public Class Form1

    WithEvents ServiceA As MTTCP

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SendTextBox.Text = "014EABCDEFG"


        TGIP1.Text = "192.168.0.129"
        TGPort1.Text = "1588"
        BindPort1.Text = "8888"

        ServiceA = New MTTCP(TGIP1.Text, CInt(TGPort1.Text), CInt(BindPort1.Text))

        'ServiceA.Stss(TGIP1.Text, CInt(TGPort1.Text), CInt(BindPort1.Text))
    End Sub

    Sub PrintText(Text As String) Handles ServiceA.PrintText

        SetText(Text & Format(Now, "hh時mm分ss.fff秒"))

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


    Private Sub TextBox2_DoubleClick(sender As Object, e As EventArgs) Handles CheckTextBox.DoubleClick

        CheckTextBox.Text = CheckSum(SendTextBox.Text)

    End Sub

    Public Function CheckSum(ByVal chkstr As String) As String
        Dim Sum As Integer
        Dim n As Integer
        Dim CheckString As String
        CheckString = chkstr + Chr(2)
        CheckString = UCase(CheckString)
        For n = 1 To Len(CheckString)
            Sum = Sum + Asc(Mid$(CheckString, n, 1))
        Next n
        CheckSum = Strings.Right(Hex(Sum), 2)
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles SendTestButton.Click
        ServiceA.TCPc_Write(Chr(2) & SendTextBox.Text & CheckTextBox.Text & Chr(3))

    End Sub


End Class

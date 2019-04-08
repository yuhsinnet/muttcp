
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Timers

Public Class MTTCP

    Public Delegate Sub PrintTextDG(ByVal Text As String)
    Public Event PrintText As PrintTextDG


    Dim TCPc As New TcpClient
    Dim TCPc_Stream As NetworkStream

    Dim sendTimerIsDown As Boolean
    Public Sub New(TGIP As String, TGPort As Integer, BindPort As Integer)

        '初始化連線
        TCPc.BeginConnect(TGIP, TGPort, AddressOf TCPc_Connect_ACB, "")


    End Sub

    Sub Stss(TGIP As String, TGPort As Integer, BindPort As Integer)

        '初始化連線
        TCPc.BeginConnect(TGIP, TGPort, AddressOf TCPc_Connect_ACB, "")






    End Sub

    Private Sub TCPc_Connect_ACB(ar As IAsyncResult)


        If TCPc.Connected Then

            RaiseEvent PrintText("connect >.< ")
            TCPc_Stream = TCPc.GetStream
        Else
            RaiseEvent PrintText("Faile >.< ")

        End If

    End Sub

    Sub TCPSendTimeDown()

        sendTimerIsDown = False

    End Sub
    Public Function TCPc_Write(SendData() As Byte) As Byte()
        '
        ' SendData:
        ' 以位元組陣列的方式傳輸數據
        TCPc_Stream.Write(SendData, 0, SendData.Length)

        '
        '
        Dim TCPSendTime As New Timers.Timer(200)
        AddHandler TCPSendTime.Elapsed, AddressOf TCPSendTimeDown
        TCPSendTime.AutoReset = False
        TCPSendTime.Start()
        sendTimerIsDown = True
        '
        '

        Dim Read_buf(1024) As Byte
        While sendTimerIsDown

            Threading.Thread.Sleep(1)

            If TCPc.Available > 3 Then
                ' UdpClient.Receive blocks until a message is received from a remote host.
                TCPc_Stream.Read(Read_buf, 0, Read_buf.Length)

                Exit While

            End If


        End While
        Return Read_buf

    End Function

    Public Function TCPc_Write(SendData As String) As String
        '
        ' SendData:
        ' 以字串的方式傳輸數據

        Dim SendBuf As Byte() = Text.ASCIIEncoding.ASCII.GetBytes(SendData)

        TCPc_Stream.Write(SendBuf, 0, SendBuf.Length)

        '
        '
        Dim TCPSendTime As New Timers.Timer(200)
        AddHandler TCPSendTime.Elapsed, AddressOf TCPSendTimeDown
        TCPSendTime.AutoReset = False
        TCPSendTime.Start()
        sendTimerIsDown = True
        '
        '

        Dim Read_buf(1024) As Byte
        While sendTimerIsDown

            Threading.Thread.Sleep(1)

            If TCPc.Available > 3 Then
                ' UdpClient.Receive blocks until a message is received from a remote host.
                TCPc_Stream.Read(Read_buf, 0, Read_buf.Length)

                Exit While

            End If


        End While
        Dim ReturnBuf As String = System.Text.Encoding.ASCII.GetString(Read_buf)

        Return ReturnBuf.Remove(ReturnBuf.IndexOf(vbNullChar))
    End Function

End Class

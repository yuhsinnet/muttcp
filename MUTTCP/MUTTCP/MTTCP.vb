
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Timers

Public Class MTTCP

    Public Delegate Sub PrintTextDG(ByVal Text As String)
    Public Event PrintText As PrintTextDG

    Const ClientNumber As Integer = 3

    Dim TCPc As New TcpClient

    Dim TCPc_Stream As NetworkStream

    Dim TCPs As TcpListener

    Dim TCPsc(ClientNumber) As TcpClient
    Dim TCPscThread(ClientNumber) As Thread

    Public TCPsQueue As New Queue
    Public Class TCPsQueueCSS

        Public index As Integer
        Public str As String

    End Class
    Dim sendTimerIsDown As Boolean
    Public Sub New(TGIP As String, TGPort As Integer, BindPort As Integer)

        '初始化客戶端連線
        TCPc.BeginConnect(TGIP, TGPort, AddressOf TCPc_Connect_ACB, "")

        Dim TCPsThread As New Thread(AddressOf StartTCPsListen)
        TCPsThread.IsBackground = True
        TCPsThread.Start(BindPort)

        Dim CoreThread As New Thread(AddressOf CorePress)
        CoreThread.IsBackground = True
        CoreThread.Start()



    End Sub

    Private Sub CorePress()

        While True

            If TCPsQueue.Count > 0 Then


                Dim te As TCPsQueueCSS = TCPsQueue.Dequeue()
                Dim send_buf As String

                send_buf = TCPc_Write(te.str)

                Dim send_Bytes() = Text.Encoding.ASCII.GetBytes(send_buf)
                TCPsc(te.index).GetStream.Write(send_Bytes, 0, send_Bytes.Length)
                RaiseEvent PrintText("TCPsc is Transmit  >.< " & "  TCPsc(" & te.index & ")     DATA:(" & send_buf & ")")
            Else

                Thread.Sleep(1)

            End If


        End While


    End Sub

    Private Sub StartTCPsListen(ByVal state As Object)
        Dim iPort As Integer
        iPort = CType(state, Integer)

        TCPs = New TcpListener(IPAddress.Any, iPort)
        TCPs.Start(TCPsc.Length)

        Do
            For index = 0 To (TCPsc.Length - 1)

                If TCPsc(index) Is Nothing Then


                    TCPsc(index) = TCPs.AcceptTcpClient
                    TCPscThread(index) = New Thread(AddressOf TCPscReceive)
                    TCPscThread(index).IsBackground = True
                    TCPscThread(index).Start(index)


                    RaiseEvent PrintText("TCPsc is connect >.< " & "  TCPsc(" & index & ")     ")



                    Exit For

                End If

            Next



        Loop




    End Sub

    Private Sub TCPscReceive(ByVal state As Object)

        Dim index As Integer = CType(state, Integer)

        Dim Read_str As String


        While True

            Try
                If TCPsc(index).Available > 3 Then

                    Dim Read_buf(1024) As Byte
                    TCPsc(index).GetStream.Read(Read_buf, 0, Read_buf.Length)
                    Read_str = System.Text.Encoding.ASCII.GetString(Read_buf)

                    Dim Read_str_remov As String
                    Read_str_remov = RemoveNullChar(Read_str)


                    Dim temp As New TCPsQueueCSS
                    temp.index = index
                    temp.str = Read_str_remov

                    TCPsQueue.Enqueue(temp)

                    RaiseEvent PrintText("TCPsc is Receive >.< " & "  TCPsc(" & index & ")     DATA:(" & Read_str_remov & ")")

                Else

                    Thread.Sleep(1)

                End If
            Catch ex As Exception

                TCPsc(index).Close()
                TCPsc(index) = Nothing
                RaiseEvent PrintText("TCPsc is Disconnect >.< " & "  TCPsc(" & index & ")")
                Exit While
            End Try






        End While

        TCPscThread(index).Abort()
        TCPscThread(index) = Nothing
    End Sub

    Private Shared Function RemoveNullChar(In_str As String) As String


        Return In_str.Remove(In_str.IndexOf(vbNullChar))

    End Function

    Private Sub TCPc_Connect_ACB(ar As IAsyncResult)


        If TCPc.Connected Then

            RaiseEvent PrintText("TCPc is connect >.< ")
            TCPc_Stream = TCPc.GetStream
        Else
            RaiseEvent PrintText("TCPc is Faile >.< ")

        End If

    End Sub
    Sub TCPcSendTimeDown()

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
        AddHandler TCPSendTime.Elapsed, AddressOf TCPcSendTimeDown
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
        AddHandler TCPSendTime.Elapsed, AddressOf TCPcSendTimeDown
        TCPSendTime.AutoReset = False
        TCPSendTime.Start()
        sendTimerIsDown = True
        '
        '

        Dim Read_buf(2048) As Byte
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

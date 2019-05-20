
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
    Dim TCPscIdleTime(ClientNumber) As Integer

    Public TCPsQueue As New Queue
    Public Class TCPsQueueCSS

        Public index As Integer
        Public str As String

    End Class
    Dim sendTime As Integer
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

                RaiseEvent PrintText("Q Get  >.< ")
                Dim te As TCPsQueueCSS = TCPsQueue.Dequeue()
                Dim send_buf As String

                send_buf = TCPc_Write(te.str)

                Dim send_Bytes() = Text.Encoding.ASCII.GetBytes(send_buf)
                TCPsc(te.index).GetStream.Write(send_Bytes, 0, send_Bytes.Length)
                RaiseEvent PrintText("TCPsc is Transmit  >.< " & "  TCPsc(" & te.index & ")     DATA:(" & send_buf & ")")
                Thread.Sleep(10)
            Else

                Thread.Sleep(10)

            End If


        End While


    End Sub

    Private Sub StartTCPsListen(ByVal state As Object)
        Dim iPort As Integer
        iPort = CType(state, Integer)

        TCPs = New TcpListener(IPAddress.Any, iPort)
        TCPs.Start()

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

        Const UPTime As Integer = 200 'x 100ms

        Dim index As Integer = CType(state, Integer)

        Dim Read_str As String

        Dim TCPSendTime As New System.Threading.Timer(AddressOf TCPscSendTimeUp, index, 0, 100)


        While TCPscIdleTime(index) < UPTime

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
                    TCPscIdleTime(index) = 0
                Else

                    Thread.Sleep(1)

                End If
            Catch ex As Exception



                Exit While
            End Try






        End While

        RaiseEvent PrintText("TCPsc is Disconnect >.< " & "  TCPsc(" & index & ")")
        TCPSendTime.Dispose()
        TCPsc(index).Close()
        TCPsc(index) = Nothing
        TCPscThread(index).Abort()
        TCPscThread(index) = Nothing

    End Sub

    Private Sub TCPscSendTimeUp(state As Object)

        Dim index As Integer = CInt(state)

        TCPscIdleTime(index) = TCPscIdleTime(index) + 1


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
    Private Sub TCPcSendTimeUp(state As Object)

        sendTime += 1

    End Sub
    Public Function TCPc_Write(SendData As String) As String
        '
        ' SendData:
        ' 以字串的方式傳輸數據

        Const UPTime As Integer = 200 'x 100ms

        Dim SendBuf As Byte() = Text.ASCIIEncoding.ASCII.GetBytes(SendData)

        TCPc_Stream.Write(SendBuf, 0, SendBuf.Length)

        '
        '
        Dim TCPScendTime As New Threading.Timer(AddressOf TCPcSendTimeUp, vbNull, 0, 100) '100ms
        sendTime = 0

        '
        '
        Dim ReadIndex As Integer = 0

        Dim ReturnBuf As String
        Dim ReadLen As Integer
        Dim DetETX As Integer
        Dim Read_buf(1024) As Byte
        While sendTime < UPTime
            Thread.Sleep(5)



            ReadLen = TCPc.Available
            TCPc_Stream.Read(Read_buf, ReadIndex, ReadLen)


            For DetETX = 0 To ReadIndex + ReadLen + 10

                If Read_buf(DetETX) = 3 Then
                    sendTime = 0

                    ReturnBuf = System.Text.Encoding.ASCII.GetString(Read_buf).Remove(DetETX + 1)
                    Array.Clear(Read_buf, 0, Read_buf.Length)
                    Exit While

                End If

            Next


            ReadIndex = ReadLen

        End While



        Return ReturnBuf


    End Function









End Class

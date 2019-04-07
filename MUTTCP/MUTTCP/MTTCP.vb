
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading


Public Class MTTCP

    Public Delegate Sub PrintTextDG(ByVal Text As String)
    Public Event PrintText As PrintTextDG


    Dim TCPc As New TcpClient
    Dim TCPc_Stream As NetworkStream


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
    '
    ' SendData:
    ' 以字串的方式傳輸數據
    Public Sub TCPc_Write(SendData As String)

        Dim SendBuf() As Byte = Text.Encoding.ASCII.GetBytes(SendData)
        TCPc_Stream.Write(SendBuf, 0, SendBuf.Length)


    End Sub
    '
    ' SendData:
    ' 以位元組陣列的方式傳輸數據
    Public Sub TCPc_Write(SendData() As Byte)

        TCPc_Stream.Write(SendData, 0, SendData.Length)

    End Sub


End Class

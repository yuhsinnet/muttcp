using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace OneMore
{





    class Mut
    {



        class TCP_Q
        {

            public int index;
            public string str;


        }

        public delegate void PDugHandler(string str);
        public event PDugHandler PDug;



        const int ClientLen = 3;

        TcpClient Tcp_C;
        TcpListener Tcp_S;

        TcpClient[] Tcp_SC = new TcpClient[ClientLen];



        public Mut(string TGIP, int TGPort, int BindPort)
        {


            Start(TGIP, TGPort, BindPort);




        }
        public Mut()
        {

        }

        public void Start(string TGIP, int TGPort, int BindPort)
        {
            Tcp_C = new TcpClient(TGIP, TGPort);





            Thread TCP_S_thread = new Thread(StartListen);
            TCP_S_thread.IsBackground = true;
            TCP_S_thread.Start(BindPort);
            TCP_S_thread.Name = "TCP_S_thread";
        }

        private void StartListen(object portobj)
        {
            int BindPort;

            try
            {
                BindPort = (int)portobj;
            }
            catch (Exception)
            {

                BindPort = 8888;
            }

            IPEndPoint _serverSocketEP = new IPEndPoint(IPAddress.Any, BindPort);
            Tcp_S = new TcpListener(_serverSocketEP);
            Tcp_S.Start();





            while (true)
            {

                try
                {

                    for (int index = 0; index < Tcp_SC.Length; index++)
                    {

                        if (Tcp_SC[index]?.Connected ?? true)
                        {
                            Tcp_SC[index] = Tcp_S.AcceptTcpClient();

                            if (Tcp_SC[index].Connected)
                            {



                                Thread thread = new Thread(TCP_SC_Revice);
                                thread.IsBackground = true;
                                thread.Start(index);
                                thread.Name = Tcp_SC[index].Client.RemoteEndPoint.ToString();

                                PDug("form: " + Tcp_SC[index].Client.RemoteEndPoint.ToString() + "connect");
                            }
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }



            }


        }

        private void TCP_SC_Revice(object indexobj)
        {
            int index = (int)indexobj;

            byte[] RX_buf = new byte[1024];
            string RX_str;


            while (true)
            {

                Tcp_SC[index].GetStream().Read(RX_buf, 0, RX_buf.Length);
                RX_str = Encoding.ASCII.GetString(RX_buf).Replace("\0", string.Empty);
                PDug(RX_str);


            } 




        }
    }
}

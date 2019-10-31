using System;
using System.Collections;
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
        Queue queue;


        public delegate void PDugHandler(string str);
        public event PDugHandler PDug;



        const int ClientLen = 3;
        /// <summary>
        /// 連向目表裝置。EX:PLC Port0。
        /// </summary>
        TcpClient Tcp_C;
        /// <summary>
        /// 伺服器。
        /// </summary>
        TcpListener Tcp_S;
        /// <summary>
        /// 對應客戶端。
        /// </summary>
        TcpClient[] Tcp_SC = new TcpClient[ClientLen];



        public Mut(string TGIP, int TGPort, int BindPort)
        {


            Start(TGIP, TGPort, BindPort);




        }
        public Mut()
        {

        }
        /// <summary>
        /// 開始主要服務。
        /// </summary>
        /// <param name="TGIP">目標IP or DNS</param>
        /// <param name="TGPort">目標Port 號</param>
        /// <param name="BindPort">綁定本機阜號</param>
        public void Start(string TGIP, int TGPort, int BindPort)
        {
            Tcp_C = new TcpClient(TGIP, TGPort);

            queue = new Queue();



            Thread TCP_S_thread = new Thread(StartListen);
            TCP_S_thread.IsBackground = true;
            TCP_S_thread.Start(BindPort);
            TCP_S_thread.Name = "TCP_S_thread";

            Thread _Q_thread = new Thread(new ThreadStart(Q_core));
            _Q_thread.IsBackground = true;
            _Q_thread.Start();
            _Q_thread.Name = "_Q_core_thread";
        }

        private void Q_core()
        {

            while (true)
            {

                if (queue.Count > 0)
                {

                    string str;
                    int index;
                    TCP_Q _Q = (TCP_Q)queue.Dequeue();

                    str = _Q.str;
                    index = _Q.index;

                    string RX;
                    try
                    {
                        RX = WriteToPLCV2(str, Tcp_C);
                        TCT_SC_Send(index, RX);

                    }
                    catch (Exception)
                    {


                        PDug("form: Tcp_SC[" + index + "] IP:" + Tcp_SC[index].Client.RemoteEndPoint.ToString() + "disConnect");
                        if (Tcp_SC[index]?.Connected ?? true)
                        {
                            Tcp_SC[index].GetStream().Close();
                            Tcp_SC[index].Close();
                        }



                    }





                }
                else Thread.Sleep(5);


            }
        }

        private void TCT_SC_Send(int index, string RX)
        {
            Tcp_SC[index].SendTimeout = 200;
            byte[] RX_buf = Encoding.ASCII.GetBytes(RX);
            Tcp_SC[index].GetStream().Write(RX_buf, 0, RX_buf.Length);
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

                                PDug("form: Tcp_SC[" + index + "] IP:" + Tcp_SC[index].Client.RemoteEndPoint.ToString() + "connect");
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
            System.Diagnostics.Debug.Print(string.Format("Start Process index: {0}", index.ToString()));

            while (true)
            {

                if (Tcp_SC[index]?.Connected ?? false)
                {

                    try
                    {
                        Array.Clear(RX_buf, 0, RX_buf.Length);

                        Tcp_SC[index].GetStream().Read(RX_buf, 0, RX_buf.Length);
                        RX_str = Encoding.ASCII.GetString(RX_buf).Trim('\0');//Replace("\0", string.Empty);

                        TCP_Q _Q = new TCP_Q
                        {
                            index = index,
                            str = RX_str
                        };

                        queue.Enqueue(_Q);



                        /*
                        PDug(string.Format("form {0} Data Len {1}",
                            Tcp_SC[index].Client.RemoteEndPoint.ToString(), //{0}
                            RX_str.Length.ToString()//{1}
                            
                            ));
                        */

                        Tcp_SC[index].GetStream().Write(new byte[1], 0, 1);
                    }
                    catch (Exception)
                    {
                        break;

                    }

                }
                else break;



            }




        }


        #region Fatek_Core

        /// <summary>
        /// 新的PLC讀寫核心，新增無資料重送及CRC 檢驗功能。
        /// </summary>
        /// <param name="cmd">指令，不含控制字元。</param>
        /// <param name="tcp">TCPClient 類別</param>
        /// <returns></returns>
        public string WriteToPLCV2(string cmd, TcpClient tcp)
        {
            byte[] sendBuf = System.Text.Encoding.ASCII.GetBytes(cmd);
            int trytimes = 0;
            string Str;
        ReTry:
            if (trytimes++ >= 60) throw new Exception("overtrytime");


            //DeBug_P(String.Format("TryTimes: {0} 次", trytimes.ToString()));


            bool closed;
            try
            {
                //使用Peek測試連線是否仍存在
                if (tcp.Connected && tcp.Client.Poll(0, SelectMode.SelectRead))
                    closed = tcp.Client.Receive(new byte[1], SocketFlags.Peek) == 0;
            }
            catch (SocketException se)
            {
                closed = true;
            }


            try
            {
                //tcp.Client.Receive(new byte[1], SocketFlags.Peek);
                if (tcp?.Connected ?? false && !closed)
                {


                    tcp.GetStream().Write(sendBuf, 0, sendBuf.Length);

                    int waittimes = 0;
                    while (!tcp.GetStream().DataAvailable)
                    {



                        if (waittimes++ > 30)
                        {
                            waittimes = 0;

                            throw new Exception("overwattime");
                        }
                        System.Threading.Thread.Sleep(5);
                    }

                    int ReadIndex = 0;

                    int ReadLen;
                    int DetETX = 0;
                    byte[] Read_buf = new byte[1024];

                    while (true)
                    {
                        Thread.Sleep(5);


                        ReadLen = tcp.Available;
                        tcp.GetStream().Read(Read_buf, ReadIndex, ReadLen);

                        for (DetETX = 0; DetETX < (ReadIndex + ReadLen); DetETX++)
                        {

                            if (Read_buf[DetETX] == 3)
                            {
                                Str = Encoding.ASCII.GetString(Read_buf).Trim('\0');
                                Array.Clear(Read_buf, 0, Read_buf.Length);
                                goto exitwhile;

                            }


                        }

                        ReadIndex = ReadLen;

                    }
                exitwhile:


                    if (FATEKCRC_check(cmd, Str)) return Str;
                    else throw new Exception("CRCerror");



                }
                else throw new SocketException((int)SocketError.SocketError);
            }
            catch (Exception e)
            {
                if (e.Message == "overwattime" | e.Message == "CRCerror")
                {
                    //System.Diagnostics.Debug.Print(e.Message);
                    goto ReTry;
                }


                tcp.Close();

                throw;

            }
        }
        /// <summary>
        /// 檢查LCR 是否正確。
        /// </summary>
        /// <param name="TXcmd">傳送出的資料</param>
        /// <param name="RXcmd">收到的資料</param>
        /// <returns></returns>
        bool FATEKCRC_check(string TXcmd, string RXcmd)
        {
            try
            {
                string PLC_BK_Site = RXcmd.Substring(1, 2);
                string PLC_BK_cmd = RXcmd.Substring(3, 2);
                string PLC_BK_CLR = RXcmd.Substring(RXcmd.Length - 3, 2);

                string PLC_In_Site = TXcmd.Substring(1, 2);
                string PLC_In_cmd = TXcmd.Substring(3, 2);
                string PLC_CLR = FATEKCheckSum(RXcmd.Substring(1, RXcmd.Length - 4));

                bool Site_check = PLC_BK_Site == PLC_In_Site;
                bool Cmd_check = PLC_BK_cmd == PLC_In_cmd;
                bool CLR_check = PLC_CLR == PLC_BK_CLR;

                bool check = Site_check & Cmd_check & CLR_check;

                if (check) return true;
                else
                {



                    System.Diagnostics.Debug.Print(string.Format("Site_check :{0} PLC site:{1} RX site:{2}\n" +
                                                                "Cmd_check :{3} PLC Cmd:{4} RX Cmd:{5}\n" +
                                                                "CLR_check :{6} PLC CLR:{7} RX CLR:{8}",
                                                                Site_check.ToString(), PLC_In_Site, PLC_BK_Site,
                                                                Cmd_check.ToString(), PLC_In_cmd, PLC_BK_cmd,
                                                                CLR_check.ToString(), PLC_CLR, PLC_BK_CLR
                                                    ));

                    return false;
                }


            }
            catch (Exception)
            {

                return false;
                throw;
            }


        }
        /// <summary>
        /// 永宏PLC LCR
        /// </summary>
        /// <param name="chkstr">傳入參數，不含控制字元。</param>
        /// <returns>檢查碼</returns>
        public string FATEKCheckSum(string chkstr)
        {

            char[] str = chkstr.ToCharArray();
            int sum = 2;

            foreach (char c in str) sum += Convert.ToInt32(c);

            string HEX = Convert.ToString(sum, 16);

            return HEX.Substring(HEX.Length - 2, 2).ToUpper();

        }

        #endregion


    }
}

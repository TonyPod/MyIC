using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imserver
{
    class Server
    {
        private string ipAddr = Settings1.Default["IPAddr"].ToString();
        private int port = int.Parse(Settings1.Default["Port"].ToString());
        private Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Dictionary<string, UserThread> OnlineUsers { get; set; }
        public Dictionary<string, List<Msg>> OfflineMsgs { get; set; }

        public Server()
        {
            OnlineUsers = new Dictionary<string, UserThread>();
            OfflineMsgs = new Dictionary<string, List<Msg>>();
        }

        /// <summary>
        /// 服务器开始监听
        /// </summary>
        public void Start() 
        {
            try
            {
                Socket client = null;
                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(ipAddr), port);
                listener.Bind(localEP);

                //没有接手处理或正在进行的连接的上限
                listener.Listen(10);

                while (true)
                {
                    client = listener.Accept();
                    Thread clientThread = new Thread(new ThreadStart(new UserThread(client, this).Runnable));
                    clientThread.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    
}

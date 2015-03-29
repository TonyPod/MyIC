using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MyServer
{
    class Program
    {
        private static string IPADDR = Settings1.Default["IPADDR"].ToString();
        private static int PORT = int.Parse(Settings1.Default["PORT"].ToString());

        public static void Main()
        {
            //string HostName = Dns.GetHostName(); //得到主机名 
            //IPHostEntry IpEntry = Dns.GetHostEntry(HostName); //得到主机IP 
            //string strIPAddr = IpEntry.AddressList[0].ToString();

            IPAddress ip = IPAddress.Parse(IPADDR); //把ip地址字符串转换为IPAddress              
            IPEndPoint ipep = new IPEndPoint(ip, PORT);//用指定的端口和ip  

            //if (!Directory.Exists(PATHNAME))
            //{
            //    Directory.CreateDirectory(PATHNAME);
            //}

            //indexFile = 1;

            try
            {
                TcpListener listener = new TcpListener(ipep);
                listener.Start();
                while (true)
                {
                    //当有可用的客户端连接尝试时执行，并返回一个新的socket                  
                    TcpClient tcpClient = listener.AcceptTcpClient();

                    //分配文件名称
                    //string fileName = PATHNAME + indexFile++ + POSTFIX;

                    Console.WriteLine("{0} New Client Connected", DateTime.Now.ToString());

                    //创建消息服务线程对象，并把连接socket赋于ClientThread                     
                    ClientThread newclient = new ClientThread(tcpClient);    
                 
                    //把ClientThread 类的ClientService方法委托给线程 
                    Thread newthread = new Thread(new ThreadStart(newclient.StartReceive));   
                  
                    // 启动消息服务线程                    
                    newthread.Start();
                }
            }
            catch
            {
                Console.Write("Connection has encountered a problem!");//连接中断或者连接失败                 
            }
        }
    }


}

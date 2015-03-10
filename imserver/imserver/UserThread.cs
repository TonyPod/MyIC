using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace imserver
{
    class UserThread
    {
        int rcvCount = 0;
        byte[] buffer = new byte[1024];
        private Socket socket;
        private string data;
        private Encoding defaultEncoding = Encoding.UTF8;
        Handler handler = null;
        Server server = null;
        public UserThread(Socket socket, Server server)
        {
            this.socket = socket;
            this.socket.SendTimeout = 2000;
            this.server = server;
            this.handler = new Handler(server.OnlineUsers, server.OfflineMsgs, this);
        }

        public void Runnable()
        {
            //Thread thread = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        try
            //        {
            //            byte[] tmp = new byte[1];
            //            int a = socket.Send(tmp, 0, 0);
            //            Console.WriteLine(a.ToString());
            //        }
            //        catch (SocketException e)
            //        {
            //            // 10035 == WSAEWOULDBLOCK
            //            if (e.NativeErrorCode.Equals(10035))
            //                Console.WriteLine("Still Connected, but the Send would block");
            //            else
            //            {
            //                Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
            //            }
            //        }
            //        Thread.Sleep(2000);
            //    }
            //});
            
            //thread.Start();
            while (true)
            {
                try
                {
                    rcvCount = this.socket.Receive(buffer, buffer.Length, 0);
                    if (rcvCount != 0)
                    {
                        data = defaultEncoding.GetString(buffer, 0, rcvCount);
                        Console.WriteLine(data + "\n");
                        handler.handle(data);
                    }
                    else 
                    {
                        if (handler.username != null)
                            server.OnlineUsers.Remove(handler.username);
                        // 10035 == WSAEWOULDBLOCK
                        break;
                    }
                }
                 catch (SocketException e)
                {
                    if (handler.username != null)
                        server.OnlineUsers.Remove(handler.username);
                    // 10035 == WSAEWOULDBLOCK
                    if (e.NativeErrorCode.Equals(10035))
                        Console.WriteLine("Still Connected, but the Send would block");
                    else
                    {
                        Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                    }
                    break;
                }
                
                    //连接断开时在线用户中清除该用户
            }
        }

        public bool SendToClient(string data)
        {
            try
            {
                byte[] databytes = defaultEncoding.GetBytes(data);
                socket.Send(databytes, SocketFlags.None);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

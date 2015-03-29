using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace sendImg
{
	class SendImg
	{
        public static void SendImage(String fileName, IPEndPoint remoteEP,int times = 1)
        {
            if (!File.Exists(fileName)) 
                return;

            //读取文件内容到缓冲区
            FileStream fs = File.Open(fileName, FileMode.Open);
            long fileLen = fs.Length;
            byte[] fileBytes = new byte[fileLen];
            fs.Read(fileBytes, 0, fileBytes.Length);
            fs.Close();
        
            //建立TCP连接
            TcpClient client = new TcpClient();
            client.Connect(remoteEP);

            NetworkStream ns = client.GetStream();

            for (int i = 0; i < times; i++)
            {
                //发送文件长度
                JObject jObj = new JObject();
                jObj.Add("Username", "365");
                jObj.Add("Filename", Guid.NewGuid().ToString() + Path.GetExtension(fileName));
                jObj.Add("Filelen", fileLen);

                byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
                ns.Write(buf, 0, buf.Length);

                //发送文件内容
                ns.Write(fileBytes, 0, fileBytes.Length);

                //接收服务器返回信息
                byte[] receivedBytes = new byte[1024];
                ns = client.GetStream();
                while (!ns.DataAvailable) ;
                int readCount = ns.Read(receivedBytes, 0, receivedBytes.Length);
                string jsonStr = Encoding.Default.GetString(receivedBytes, 0, readCount);
                JObject jsonObj = JObject.Parse(jsonStr);
                Console.WriteLine(jsonStr);
            }
        }
	}
}

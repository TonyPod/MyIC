using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyServer
{
    class Util
    {
        [DllImport("ToolKit.dll", ExactSpelling = true, EntryPoint = "?analyze@@YG?AU_Illnesses@@PBD0@Z", CallingConvention = CallingConvention.StdCall)]
        public static extern Illnesses analyze(string fileName, string outputFileName);

        [StructLayout(LayoutKind.Sequential)]
        public struct Illnesses
        {
            public int nbTeeth;
            public int status;
            public IntPtr illnesses;
        }

        private static string LogFileName = Settings1.Default["logFileName"].ToString();

        /// <summary>
        /// 将错误信息写入日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteToLog(string log)
        {
            File.AppendAllText(LogFileName, log);
        }

        private static string serverIP = Settings1.Default["IPADDR"].ToString();
        private static int IMPort = int.Parse(Settings1.Default["IMPORT"].ToString());

        /// <summary>
        /// 异步发送信息到即时通讯服务器端口中
        /// </summary>
        /// <param name="msg"></param>
        internal static void SendMsgAsync(Msg msg)
        {
            new Thread(() =>
            {
                IPEndPoint imEP = new IPEndPoint(IPAddress.Parse(serverIP), IMPort);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(imEP);
                socket.LingerState = new LingerOption(false, 1);

                JObject jObj = new JObject();
                jObj.Add("type", "1");
                jObj.Add("data", JsonConvert.SerializeObject(msg));

                byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
                socket.Send(buf, 0, buf.Length, SocketFlags.None);

            }).Start();
        }
    }
}

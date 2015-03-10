using IMClient_WinForm.Properties;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IMClient_WinForm
{
    class MyIMClient
    {
        private static string serverIP = Settings.Default["ServerIP"].ToString();
        private static int serverPort = int.Parse(Settings.Default["ServerPort"].ToString());
        //private static TcpClient client;
        public static Socket client;

        public static bool Connected { get { return client.Connected; } }
        public static List<Msg> OfflineMsgs { get; set; }

        public static string Username { get; set; }

        public static bool Connect()
        {
            try
            {
                IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //client = new TcpClient();
                client.Connect(serverEP);
                client.LingerState = new LingerOption(false,1);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Login(string username)
        {
            Username = username;

            //首次登录
            JObject jObj = new JObject();
            jObj.Add("type", "0");
            jObj.Add("data", username);

            byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
            client.Send(buf, 0, buf.Length, SocketFlags.None);

            //等待返回信息
            byte[] bufRcv = new byte[1024];
            int rcvCount = client.Receive(bufRcv, 0, bufRcv.Length, SocketFlags.None);
            string rcvStr = Encoding.UTF8.GetString(bufRcv, 0, rcvCount);
            if (rcvStr != "")
            {
                JObject jObjRcv = JObject.Parse(rcvStr);
                string state = (string)jObjRcv["state"];
                if ("ok".Equals(state))
                {
                    int count = (int)jObjRcv["count"];
                    if (count == 0)
                    {
                        OfflineMsgs = null;
                    }
                    else
                    {
                        OfflineMsgs = new List<Msg>();
                        JArray jMsgs = JArray.Parse(jObjRcv["content"].ToString());
                        for (int i = 0; i < count; i++)
                        {
                            Msg msg = JsonConvert.DeserializeObject<Msg>(jMsgs[i].ToString());
                            OfflineMsgs.Add(msg);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        public static bool SendMsg(Msg msg)
        {
            string msgJson = JsonConvert.SerializeObject(msg);

            JObject jObj = new JObject();
            jObj.Add("type", 1);
            jObj.Add("data", msgJson);

            byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
            client.Send(buf, 0, buf.Length, SocketFlags.None);

            return true;
        }

        public static void Close()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}

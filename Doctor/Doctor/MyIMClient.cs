using Doctor;
using Doctor.Model;
using Doctor.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DoctorClient 
{
    class MyIMClient
    {
        private static string serverIP = Settings.Default["ServerIP"].ToString();
        private static int serverPort = int.Parse(Settings.Default["ServerPort"].ToString());
        private static int rcvCount;
        private static byte[] buf = new byte[2048];
        public static Socket client;

        //离线信息更改事件
        public delegate void OfflineMsgChangedHandler(OfflineMsgEventArgs e);
        public static event OfflineMsgChangedHandler OfflineMsgChanged;
        protected static void OnOfflineMsgsChanged(OfflineMsgEventArgs e)
        {
            if (OfflineMsgChanged != null)
            {
                OfflineMsgChanged(e);
            }
        }

        public delegate void ContactsChangedHandler(ContactsEventArgs e);
        public static event ContactsChangedHandler ContactsChanged;
        protected static void OnContactsChanged(ContactsEventArgs e)
        {
            if (ContactsChanged != null)
            {
                ContactsChanged(e);
            }
        }

        //后台的处理收到数据的线程
        private static Thread threadRcv;

        public static bool Connected { get { return client == null ? false : client.Connected; } }

        //联系人和消息列表
        public static List<Msg> UnreadMsgs = new List<Msg>();
        public static List<Msg> ReadMsgs = new List<Msg>();
        public static Dictionary<string, HashSet<string>> Contacts = new Dictionary<string, HashSet<string>>();

        //默认的两个分组
        public const string FAMILIAR = "好友";
        public const string UNFAMILIAR = "陌生人";

        //缓存文件名
        public static string unreadMsgsFileName = Environment.CurrentDirectory + "\\UnreadMsgs.json";
        public static string readMsgsFileName = Environment.CurrentDirectory + "\\ReadMsgs.json";
        public static string contactsFileName = Environment.CurrentDirectory + "\\Contacts.dat";

        public static string Username { get; set; }

        public static bool Connect()
        {
            if (Connected)
            {
                return true;
            }

            try
            {
                IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //client = new TcpClient();
                client.Connect(serverEP);
                client.LingerState = new LingerOption(false, 1);

                //线程监听TCP连接是否有数据传来
                threadRcv = new Thread(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(200);
                        try
                        {
                            rcvCount = MyIMClient.client.Receive(buf, 0, buf.Length, SocketFlags.None);
                            string rcvStr = Encoding.UTF8.GetString(buf, 0, rcvCount);
                            //居然可能会两个同时发过来"{"count":.....}{"count":.....}"
                            if (!string.IsNullOrEmpty(rcvStr))
                            {
                                foreach (var json in SplitIllegalJson(rcvStr))
                                {
                                    JObject jObjRcv = JObject.Parse(json);
                                    string state = (string)jObjRcv["state"];
                                    if ("ok".Equals(state))
                                    {
                                        int count = (int)jObjRcv["count"];
                                        if (count != 0)
                                        {
                                            JArray jMsgs = JArray.Parse(jObjRcv["content"].ToString());
                                            List<Msg> newMsgs = new List<Msg>();
                                            for (int i = 0; i < count; i++)
                                            {
                                                Msg msg = JsonConvert.DeserializeObject<Msg>(jMsgs[i].ToString());
                                                Contacts[UNFAMILIAR].Add(msg.From);
                                                newMsgs.Add(msg);
                                            }
                                            UnreadMsgs.AddRange(newMsgs);
                                            OnOfflineMsgsChanged(new OfflineMsgEventArgs() { NewMsgs = newMsgs, Reason = OfflineMsgEventArgs.EnumReason.MsgAdded });
                                        }
                                    }
                                }
                            }
                        }
                        catch (SocketException)
                        {
                            //MessageBox.Show("连接中断");
                            // 10035 == WSAEWOULDBLOCK
                            //if (a.NativeErrorCode.Equals(10035))
                            //    Console.WriteLine("Still Connected, but the Send would block");
                            //else
                            //{
                            //    Console.WriteLine("Disconnected: error code {0}!", a.NativeErrorCode);
                            //}
                            break;
                        }
                    }
                });
                threadRcv.Start();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 加载联系人
        /// </summary>
        public static void LoadContacts()
        {
            //先添加默认的两个
            if (!Contacts.ContainsKey(FAMILIAR))
            {
                Contacts.Add(FAMILIAR, new HashSet<string>());
            }
            if (!Contacts.ContainsKey(UNFAMILIAR))
            {
                Contacts.Add(UNFAMILIAR, new HashSet<string>());
            }

            //查看本地是否有联系人的缓存
            if (File.Exists(contactsFileName))
            {
                var lines = File.ReadLines(contactsFileName);
                foreach (var line in lines)
                {
                    string[] strs = line.Split(' ');
                    if (!Contacts.ContainsKey(strs[0]))
                    {
                        Contacts.Add(strs[0], new HashSet<string>());
                    }
                    Contacts[strs[0]].Add(strs[1]);
                }
            }
        }

        /// <summary>
        /// 发送登录信息
        /// </summary>
        /// <param name="username">用户名</param>
        public static void Login(string username)
        {
            Username = username;

            //首次登录
            JObject jObj = new JObject();
            jObj.Add("type", "0");
            jObj.Add("data", username);

            byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
            client.Send(buf, 0, buf.Length, SocketFlags.None);
        }

        public static bool SendMsg(Msg msg)
        {
            try
            {
                string msgJson = JsonConvert.SerializeObject(msg);

                JObject jObj = new JObject();
                jObj.Add("type", 1);
                jObj.Add("data", msgJson);

                byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
                client.Send(buf, 0, buf.Length, SocketFlags.None);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void Close()
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();

            threadRcv.Abort();
        }

        /// <summary>
        /// 清除所有的离线信息（相当于标记为已读或加入到已读列表中）
        /// </summary>
        /// <param name="patientName"></param>
        public static void RemoveAllMsgs(string patientName)
        {
            UnreadMsgs.RemoveAll((msg) => 
            {
                //添加到已读列表中
                ReadMsgs.Add(msg);
                return msg.From.Equals(patientName); 
            });

            OnOfflineMsgsChanged(new OfflineMsgEventArgs() { Reason = OfflineMsgEventArgs.EnumReason.MsgRead, User = patientName });
        }

        /// <summary>
        /// 因为TCP连接可能合并数据包，居然可能会发过来"{"count":.....}{"count":.....}" 
        /// 如果是这样，必须拆开
        /// </summary>
        /// <param name="json">不合法JSON</param>
        /// <returns>拆开的JSON OBJECT</returns>
        private static List<string> SplitIllegalJson(string json)
        {
            List<string> list = new List<string>();
            //从前向后找配对的{}
            int i, j = 0;
            int left = 0;
            for (i = 0; i < json.Length; i++)
            {
                if (json[i] == '{')
                {
                    if (j == 0)
                    {
                        left = i;
                    }
                    j++;
                }
                else if (json[i] == '}')
                {
                    j--;
                    if (j == 0)
                    {
                        list.Add(json.Substring(left, i - left + 1));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 将指定的联系人加入到指定组中
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupName"></param>
        internal static void ChangeGroup(string username, string groupName)
        {
            //先找到联系人并删除
            RemoveContact(username);

            //再添加到指定组
            AddContact(username, groupName);
        }

        /// <summary>
        /// 从联系人中删除指定联系人
        /// </summary>
        /// <param name="username"></param>
        internal static void RemoveContact(string username)
        {
            foreach (var contacts in Contacts)
            {
                if (contacts.Value.Contains(username))
                {
                    contacts.Value.Remove(username);
                    OnContactsChanged(new ContactsEventArgs()
                    {
                        Reason = ContactsEventArgs.EnumReason.Removed,
                        RemovedContact = username
                    });
                    return;
                }
            }
        }

        /// <summary>
        /// 缓存联系人到本地
        /// </summary>
        internal static void SaveContacts()
        {
            List<string> lines = new List<string>();
            foreach (var contacts in MyIMClient.Contacts)
            {
                foreach (var contact in contacts.Value)
                {
                    lines.Add(contacts.Key + " " + contact);
                }
            }
            File.WriteAllLines(MyIMClient.contactsFileName, lines);
        }

        /// <summary>
        /// 缓存消息到本地
        /// </summary>
        public static void SaveMsgs()
        {
            List<string> lines = new List<string>();
            //已读消息
            foreach (var msg in ReadMsgs)
            {
                lines.Add(JsonConvert.SerializeObject(msg));
            }
            File.WriteAllLines(readMsgsFileName, lines);

            lines.Clear();
            //未读消息
            foreach (var msg in UnreadMsgs)
            {
                lines.Add(JsonConvert.SerializeObject(msg));
            }
            File.WriteAllLines(unreadMsgsFileName, lines);
        }

        /// <summary>
        /// 加载消息到内存
        /// </summary>
        public static void LoadMsgs()
        {
            string[] lines = File.ReadAllLines(readMsgsFileName);
            foreach(var line in lines)
            {
                Msg msg = JsonConvert.DeserializeObject<Msg>(line);
                ReadMsgs.Add(msg);
            }

            lines = File.ReadAllLines(unreadMsgsFileName);
            foreach (var line in lines)
            {
                Msg msg = JsonConvert.DeserializeObject<Msg>(line);
                UnreadMsgs.Add(msg);
            }
        }

        /// <summary>
        /// 添加用户到指定组
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupName"></param>
        public static void AddContact(string username, string groupName)
        {
            Contacts[groupName].Add(username);
            OnContactsChanged(new ContactsEventArgs() 
            { 
                Reason = ContactsEventArgs.EnumReason.Added, 
                NewContact = username,
                Group = groupName
            });
        }

        /// <summary>
        /// 检查是否包含指定用户
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool HasContact(string username)
        {
            foreach (var contacts in Contacts)
            {
                if(contacts.Value.Contains(username))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

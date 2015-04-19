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
using System.Threading.Tasks;
using System.Windows.Forms;

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

        //联系人更改事件
        public delegate void ContactsChangedHandler(ContactsEventArgs e);
        public static event ContactsChangedHandler ContactsChanged;
        protected static void OnContactsChanged(ContactsEventArgs e)
        {
            if (ContactsChanged != null)
            {
                ContactsChanged(e);
            }
        }

        //private static Timer timer;

        //即时通讯服务器连接成功事件
        public delegate void ConnectionEstablishedHandler();
        public static event ConnectionEstablishedHandler ConnectionEstablished;
        protected static void OnConnectionEstablished()
        {
            //TCP连接建立自动登录
            Login(LoginStatus.UserInfo.Name);

            ////定时发送空消息监测网络状态
            //timer = new Timer(new TimerCallback((obj) =>
            //{
            //    SendEmptyMsg();
            //}), null, 0, 1000 * 5);

            if (ConnectionEstablished != null)
            {
                ConnectionEstablished();
            }
        }

        //连接中事件
        public delegate void ConnectingHandler(ConnectingEventArgs e);
        public static event ConnectingHandler Connecting;
        protected static void OnConnecting(ConnectingEventArgs e)
        {
            if (Connecting != null)
            {
                Connecting(e);
            }
        }

        //即时通讯服务器连接失败事件
        public delegate void ConnectionSuspendedHandler(ConnectionSuspendedEventArgs e);
        public static event ConnectionSuspendedHandler ConnectionSuspended;
        protected static void OnConnectionSuspended(ConnectionSuspendedEventArgs e)
        {
            if (ConnectionSuspended != null)
            {
                ConnectionSuspended(e);
            }
        }

        //即时通讯服务器连接手动结束事件
        public delegate void ConnectionClosedHandler();
        public static event ConnectionClosedHandler ConnectionClosed;
        protected static void OnConnectionClose()
        {
            ////结束定时发送空消息
            //if(timer != null)
            //{
            //    timer.Dispose();
            //}

            if (ConnectionClosed != null)
            {
                ConnectionClosed();
            }
        }

        //后台的处理收到数据的线程
        private static Thread threadRcv;

        public static bool Connected { get { return client == null ? false : client.Connected; } }


        //联系人和消息列表
        public static List<Msg> UnreadMsgs = new List<Msg>();
        public static List<Msg> ReadMsgs = new List<Msg>();
        public static List<Msg> SentMsgs = new List<Msg>();
        public static Dictionary<string, HashSet<string>> Contacts = new Dictionary<string, HashSet<string>>();

        //默认的两个分组
        public const string FAMILIAR = "friend";
        public const string UNFAMILIAR = "stranger";

        public static string userCacheFolder = null;

        //缓存文件名
        public static string UnreadMsgsFileName { get { return userCacheFolder + "UnreadMsgs.json"; } }
        public static string ReadMsgsFileName { get { return userCacheFolder + "ReadMsgs.json"; } }
        public static string SentMsgsFileName { get { return userCacheFolder + "SentMsgs.json"; } }
        public static string ContactsFileName { get { return userCacheFolder + "Contacts.dat"; } }

        public static string Username { get; set; }

        private static CancellationTokenSource tokenSource = new CancellationTokenSource();

        private static Task<bool> task;

        public static bool IsConnecting { get { return task == null ? false : task.Status == TaskStatus.Running; } }
        /// <summary>
        /// 异步连接
        /// </summary>
        /// <param name="maxAttempts">最大尝试次数</param>
        /// <returns></returns>
        public static Task<bool> ConnectAsync(int maxAttempts = 10)
        {
            if (IsConnecting)
            {
                return task;
            }

            tokenSource = new CancellationTokenSource();
            task = Task.Factory.StartNew(() =>
            {
                if (Connected)
                {
                    return true;
                }

                for (int j = 0; j < maxAttempts; j++)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    try
                    {
                        //尝试连接
                        OnConnecting(new ConnectingEventArgs() { CurTime = j + 1});

                        //建立TCP连接
                        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        client.Connect(serverEP);
                        client.LingerState = new LingerOption(false, 1);

                        if (client.Connected)
                        {
                            OnConnectionEstablished();
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
                                        if (tokenSource.IsCancellationRequested)
                                        {
                                            OnConnectionSuspended(new ConnectionSuspendedEventArgs(false));
                                        }
                                        else
                                        {
                                            OnConnectionSuspended(new ConnectionSuspendedEventArgs(true));
                                        }
                                        break;
                                    }
                                }
                            });
                            threadRcv.Start();
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        OnConnectionSuspended(new ConnectionSuspendedEventArgs(false));
                        continue;
                    }
                }
                OnConnectionSuspended(new ConnectionSuspendedEventArgs(false));
                return false;
            }, tokenSource.Token);
            return task;
        }

        public static void LoadCache()
        {
            //缓存目录
            userCacheFolder = Environment.CurrentDirectory + "\\Cache\\" + LoginStatus.UserInfo.Name + "\\";

            //创建缓存目录
            if (!Directory.Exists(userCacheFolder))
            {
                Directory.CreateDirectory(userCacheFolder);
            }

            LoadContacts();
            LoadMsgs();
        }

        /// <summary>
        /// 加载联系人
        /// </summary>
        private static void LoadContacts()
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
            if (File.Exists(ContactsFileName))
            {
                var lines = File.ReadLines(ContactsFileName);
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
                jObj.Add("type", "1");
                jObj.Add("data", msgJson);

                byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
                client.Send(buf, 0, buf.Length, SocketFlags.None);

                SentMsgs.Add(msg);

                return true;
            }
            catch (Exception)
            {
                OnConnectionSuspended(new ConnectionSuspendedEventArgs(false));
                return false;
            }
        }
        
        /// <summary>
        /// 关闭连接并保存
        /// </summary>
        public static void Close()
        {
            tokenSource.Cancel();

            if (Connected)
            {
                SaveContacts();
                SaveMsgs();
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                if (threadRcv != null)
                {
                    threadRcv.Abort();
                }
            }

            ClearCache();
            OnConnectionClose();
        }

        private static void ClearCache()
        {
            foreach (var item in Contacts)
            {
                item.Value.Clear();
            }

            UnreadMsgs.Clear();
            ReadMsgs.Clear();
            SentMsgs.Clear();
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
            File.WriteAllLines(MyIMClient.ContactsFileName, lines);
        }

        /// <summary>
        /// 缓存消息到本地
        /// </summary>
        private static void SaveMsgs()
        {
            List<string> lines = new List<string>();
            //已读消息
            foreach (var msg in ReadMsgs)
            {
                lines.Add(JsonConvert.SerializeObject(msg));
            }
            File.WriteAllLines(ReadMsgsFileName, lines);

            lines.Clear();
            //未读消息
            foreach (var msg in UnreadMsgs)
            {
                lines.Add(JsonConvert.SerializeObject(msg));
            }
            File.WriteAllLines(UnreadMsgsFileName, lines);

            //发送消息
            lines.Clear();
            foreach (var msg in SentMsgs)
            {
                lines.Add(JsonConvert.SerializeObject(msg));
            }
            File.WriteAllLines(SentMsgsFileName, lines);
        }

        /// <summary>
        /// 加载消息到内存
        /// </summary>
        private static void LoadMsgs()
        {
            string[] lines = null;
            if (File.Exists(ReadMsgsFileName))
            {
                lines = File.ReadAllLines(ReadMsgsFileName);
                foreach(var line in lines)
                {
                    Msg msg = JsonConvert.DeserializeObject<Msg>(line);
                    ReadMsgs.Add(msg);
                }
            }

            if (File.Exists(UnreadMsgsFileName))
            {
                lines = File.ReadAllLines(UnreadMsgsFileName);
                foreach (var line in lines)
                {
                    Msg msg = JsonConvert.DeserializeObject<Msg>(line);
                    UnreadMsgs.Add(msg);
                }
            }

            if (File.Exists(SentMsgsFileName))
            {
                lines = File.ReadAllLines(SentMsgsFileName);
                foreach (var line in lines)
                {
                    Msg msg = JsonConvert.DeserializeObject<Msg>(line);
                    SentMsgs.Add(msg);
                }
            }
        }

        /// <summary>
        /// 添加用户到指定组
        /// </summary>
        /// <param name="username"></param>
        /// <param name="groupName"></param>
        public static void AddContact(string username, string groupName = UNFAMILIAR)
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

        public static void SendEmptyMsg()
        {
            Msg msg = new Msg(Username, "", "");
            SendMsg(msg);
        }
    }
}

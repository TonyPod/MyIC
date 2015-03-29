using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Net.Sockets;
using Model;
using Newtonsoft.Json;

namespace imserver
{
    class Handler
    {
        Dictionary<string, UserThread> onlineUsers;
        Dictionary<string, List<Msg>> offlineMsgs;
        public string username = null;
        public string usertype = null;
        UserThread userThread = null;
        public Handler(Dictionary<string, UserThread> a, Dictionary<string, List<Msg>> offline_list, UserThread c)
        {
            this.userThread = c;
            this.onlineUsers = a;
            this.offlineMsgs = offline_list;
        }

        /// <summary>
        /// 如何处理发过来的消息
        /// </summary>
        /// <param name="rcvStr">消息</param>
        public void handle(string rcvStr)
        {
            var rcvJson = JObject.Parse(rcvStr);
            //使用ToString()或强制转换区别：如果rcvJson["type"]的类型是int，强制转换成string就会出错
            string instruction = rcvJson["type"].ToString();

            if ("1".Equals(instruction))
            {
                //已登录，发送信息
                string data = (string)rcvJson["data"];
                SendMsg(data);
            }
            else if ("0".Equals(instruction))
            {
                username = rcvJson["data"].ToString();
                if (!onlineUsers.ContainsKey(username))
                {
                    //首次登陆，返回离线信息
                    onlineUsers.Add(username, userThread);
                }
                int count;
                JObject jObj = new JObject();
                jObj.Add("state", "ok");
                string content = GetMsgs(username, out count);
                jObj.Add("count", count);
                jObj.Add("content", content);
                onlineUsers[username].SendToClient(jObj.ToString());                   
            }
        }

        /// <summary>
        /// 取得用户名对应的离线信息的JSONArray并返回
        /// </summary>
        /// <param name="username"></param>
        public string GetMsgs(string username, out int count)
        {
            if (is_exist(username))
            {
                lock (offlineMsgs)
                {
                    if (offlineMsgs.ContainsKey(username))
                    {
                        //返回离线信息
                        count = offlineMsgs[username].Count;
                        JArray jMsgs = new JArray();
                        for (int i = 0; i < offlineMsgs[username].Count; i++)
                        {
                            jMsgs.Add(JsonConvert.SerializeObject(offlineMsgs[username][i]));
                        }

                        //删除缓存的离线信息
                        offlineMsgs.Remove(username);
                        return jMsgs.ToString();
                    }
                }
            }
            count = 0;
            return null;
        }

        /// <summary>
        /// 发送信息（如果收信人不在线则保存至离线消息）
        /// </summary>
        /// <param name="data">信息内容字符串</param>
        public void SendMsg(string data)
        {
            Msg msg = JsonConvert.DeserializeObject<Msg>(data);
            string receiver = msg.To;

            if (onlineUsers.ContainsKey(receiver))
            {
                JObject jObj = new JObject();
                jObj.Add("state", "ok");
                jObj.Add("count", 1);
                JArray jArr = new JArray();
                jArr.Add(data);
                jObj.Add("content", jArr);
                onlineUsers[receiver].SendToClient(jObj.ToString());
            }
            else
            {
                if (is_exist(receiver))
                {
                    lock (offlineMsgs)
                    {
                        if (offlineMsgs.ContainsKey(receiver))
                        {
                            offlineMsgs[receiver].Add(msg);
                        }
                        else
                        {
                            List<Msg> msgs = new List<Msg>();
                            msgs.Add(msg);
                            offlineMsgs.Add(receiver, msgs);
                        }
                    }
                }
            }
        }

        private bool is_exist(string name)
        {
            return true;
            //SqlConnection conn = new SqlConnection("Database=poi;Data Source=127.0.0.1;User Id=newuser;Password=8883027;CharSet=gbk;port=3306");
            //string str = "select * from user where user_id='" + name + "';";
            //SqlCommand sqlcommand = new SqlCommand();
            //sqlcommand.CommandText = str;
            //sqlcommand.Connection = conn;
            //int number = sqlcommand.ExecuteNonQuery();
            //if (number != 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}

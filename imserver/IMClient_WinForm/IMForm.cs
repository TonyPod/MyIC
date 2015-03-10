using IMClient_WinForm.Properties;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IMClient_WinForm
{
    public partial class IMForm : Form
    {
        int rcvCount;
        private delegate void FlushClient();
        private byte[] buf = new byte[2048];
        private StringBuilder builder = new StringBuilder();
        public IMForm()
        {
            InitializeComponent();
            
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            string from = tb_from.Text.Trim();
            string to = tb_to.Text.Trim();
            string content = tb_content.Text;
            Msg msg = new Msg(from, to, content);
            MyIMClient.SendMsg(msg);

            builder.AppendFormat("{0} {1}", msg.Time, msg.From).AppendLine();
            builder.AppendLine(msg.Content);
            builder.AppendLine();
            tb_received.Text = builder.ToString();;
        }

        private void IMForm_Load(object sender, EventArgs e)
        {
            tb_from.Text = MyIMClient.Username;

            //是否有离线消息
            if (MyIMClient.OfflineMsgs != null)
            {
                foreach (var msg in MyIMClient.OfflineMsgs)
                {
                    builder.AppendFormat("{0} {1}", msg.Time, msg.From).AppendLine();
                    builder.AppendLine(msg.Content);
                    builder.AppendLine();
                }
                tb_received.Text = builder.ToString();
            }

            //线程监听TCP连接是否有数据传来
            Thread thread = new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(200);
                    try 
                    {
                        rcvCount = MyIMClient.client.Receive(buf, 0, buf.Length, SocketFlags.None);
                        ThreadFunction();                    
                    }
                    catch (SocketException a)
                    {
                        // 10035 == WSAEWOULDBLOCK
                        if (a.NativeErrorCode.Equals(10035))
                            Console.WriteLine("Still Connected, but the Send would block");
                        else
                        {
                            Console.WriteLine("Disconnected: error code {0}!", a.NativeErrorCode);
                        }
                        break;
                    }
                }
            });
            thread.Start();
        }

        private void ThreadFunction()
        {
            if (this.tb_received.InvokeRequired)
            {
                FlushClient fc = new FlushClient(ThreadFunction);
                this.Invoke(fc);
            }
            else
            {
                //解析数据并显示
                string rcvStr = Encoding.UTF8.GetString(buf, 0, rcvCount);
                JObject jObj = JObject.Parse(rcvStr);
                Msg msg = JsonConvert.DeserializeObject<Msg>(jObj["content"][0].ToString());

                builder.AppendFormat("{0} {1}", msg.Time, msg.From).AppendLine();
                builder.AppendLine(msg.Content);
                builder.AppendLine();

                this.tb_received.Text = builder.ToString();
            }
        }

        private void IMForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyIMClient.Close();
        }
    }
}

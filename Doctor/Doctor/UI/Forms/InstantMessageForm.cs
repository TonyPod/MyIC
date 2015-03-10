using Doctor.Model;
using DoctorClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.Forms
{
    public partial class InstantMessageForm : Form
    {
        private string patientName;
        private StringBuilder builder;

        public InstantMessageForm(string patientName)
        {
            InitializeComponent();
            this.patientName = patientName;
            this.Text = patientName;
        }

        /// <summary>
        /// 点击事件：发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            string content = tb_input.Text;
            Msg msg = new Msg(LoginStatus.UserInfo.Name, patientName, content);
            if(MyIMClient.SendMsg(msg))
            {
                builder.AppendFormat("{0} {1}", msg.Time, msg.From).AppendLine();
                builder.AppendLine(msg.Content);
                builder.AppendLine();

                tb_history.Text = builder.ToString();
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstantMessageForm_Load(object sender, EventArgs e)
        {
            //删除示例历史消息
            tb_history.Clear();

            //显示离线的消息
            var msgs = (from msg in MyIMClient.ReadMsgs
                       where msg.From.Equals(patientName)
                        select msg).Union<Msg>(from msg in MyIMClient.UnreadMsgs
                                               where msg.From.Equals(patientName)
                                               select msg);

            builder = new StringBuilder();
            foreach (var msg in msgs)
            {
                builder.AppendFormat("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From).AppendLine();
                builder.AppendLine(msg.Content);
                builder.AppendLine();
            }
            tb_history.Text = builder.ToString();

            //移出离线消息
            MyIMClient.RemoveAllMsgs(patientName);

            //离线信息改变则刷新
            MyIMClient.OfflineMsgChanged += (a) => { RefreshHistory(a); };
        }

        private void RefreshHistory(OfflineMsgEventArgs e)
        {
            if (this.tb_history.InvokeRequired)
            {
                MyIMClient.OfflineMsgChangedHandler handler = new MyIMClient.OfflineMsgChangedHandler(RefreshHistory);
                this.Invoke(handler, e);
            }
            else
            {
                if (e.Reason == OfflineMsgEventArgs.EnumReason.MsgAdded)
                {
                    //解析数据并显示
                    foreach (var msg in e.NewMsgs)
                    {
                        builder.AppendFormat("{0} {1}", msg.Time, msg.From).AppendLine();
                        builder.AppendLine(msg.Content);
                        builder.AppendLine();
                    }

                    this.tb_history.Text = builder.ToString();
                }
            }
        }

        ///// <summary>
        ///// 退出窗口时删除所有与该用户通讯的离线信息
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void InstantMessageForm_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    //移出离线消息
        //    MyIMClient.RemoveAllMsgs(patientName);
        //}
    }
}

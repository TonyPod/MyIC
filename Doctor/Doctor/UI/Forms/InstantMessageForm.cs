using Doctor.Model;
using Doctor.Properties;
using Doctor.UI.Forms;
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
        private const int MAXLEN_MSG = 250;
        public string PatientName { get { return patientName; } }
        private string patientName;

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
            SendMsg();
        }

        private void ShowMsg(Msg msg)
        {
            tb_history.AppendText(string.Format("{0} {1}", msg.Time, msg.From) + Environment.NewLine);
            tb_history.AppendText(msg.Content + Environment.NewLine);
            tb_history.AppendText(Environment.NewLine);

            tb_history.ScrollToCaret();
        }

        private void SendMsg()
        {
            string content = tb_input.Text;
            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            if (content.Length > MAXLEN_MSG)
            {
                MessageBox.Show("文本过长，请分条发送");
                tb_input.Focus();
                tb_input.SelectAll();
                return;
            }

            Msg msg = new Msg(LoginStatus.UserInfo.Name, patientName, content);
            if (MyIMClient.SendMsg(msg))
            {
                ShowMsg(msg);

                tb_input.Text = "";
                tb_input.Focus();
            }
            else
            {
                MessageBox.Show("发送失败");
            }
        }

        /// <summary>
        /// 初始化文字
        /// </summary>
        private void InitLanguage()
        {
            btn_send.Text = ResourceCulture.GetString("send");
            btn_selfCheck.Text = ResourceCulture.GetString("view_self_check");
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstantMessageForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            //删除示例历史消息
            tb_history.Clear();

            //显示历史消息（已读+已发送）
            var msgs = (from msg in MyIMClient.ReadMsgs
                        where msg.From.Equals(patientName)
                        select msg).Union<Msg>(from msg in MyIMClient.SentMsgs
                                               where msg.To.Equals(patientName)
                                               select msg).OrderBy(c => c.Time);

            StringBuilder builder = new StringBuilder();
            foreach (var msg in msgs)
            {
                builder.AppendFormat("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From).AppendLine();
                builder.AppendLine(msg.Content);
                builder.AppendLine();
            }

            if (msgs.Count() > 0)
            {
                builder.AppendLine();
                builder.AppendLine("----------历史消息----------");
                builder.AppendLine();
            }

            var unreadMsgs = from msg in MyIMClient.UnreadMsgs
                             where msg.From.Equals(patientName)
                             select msg;

            foreach (var msg in unreadMsgs)
            {
                builder.AppendFormat("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From).AppendLine();
                builder.AppendLine(msg.Content);
                builder.AppendLine();
            }

            tb_history.AppendText(builder.ToString());
            tb_history.ScrollToCaret();

            //移出离线消息
            MyIMClient.RemoveAllMsgs(patientName);

            //离线信息改变则刷新
            MyIMClient.OfflineMsgChanged += MyIMClient_OfflineMsgChanged;

            tb_input.Focus();
        }

        private void MyIMClient_OfflineMsgChanged(OfflineMsgEventArgs e)
        {
            RefreshHistory(e);
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
                        ShowMsg(msg);
                    }
                }
            }
        }

        /// <summary>
        /// 输入框获得焦点后按下键盘后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!(e.Control ^ Settings.Default.CtrlSendMsg))
                {
                    SendMsg();
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "menuItem_ctrl":
                    Settings.Default.CtrlSendMsg = true;
                    Settings.Default.Save();
                    break;
                case "menuItem_enter":
                    Settings.Default.CtrlSendMsg = false;
                    Settings.Default.Save();
                    break;
            }
        }

        private void btn_sendOptions_Click(object sender, EventArgs e)
        {
            contextMenuStrip.Show((Control)sender, 0, 0);
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            //Ctrl+Enter发送信息右键菜单初始化
            if (Settings.Default.CtrlSendMsg)
            {
                menuItem_ctrl.Checked = true;
                menuItem_enter.Checked = false;
            }
            else
            {
                menuItem_ctrl.Checked = false;
                menuItem_enter.Checked = true;
            }
        }

        private void InstantMessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyIMClient.OfflineMsgChanged -= MyIMClient_OfflineMsgChanged;
        }

        /// <summary>
        /// 点击事件：查看该用户的自检信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selfCheck_Click(object sender, EventArgs e)
        {
            SelfCheckListForm form = new SelfCheckListForm(patientName);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
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

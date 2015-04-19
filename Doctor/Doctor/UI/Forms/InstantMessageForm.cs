using Doctor.Model;
using Doctor.Properties;
using Doctor.UI.Forms;
using Doctor.Util;
using DoctorClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.Forms
{
    public partial class InstantMessageForm : Form
    {
        private static Font MsgContentFont = new Font("宋体", 9);
        private static Font MsgRcvFont = new Font("宋体", 9);
        private static Font MsgSendFont = new Font("宋体", 9);

        private static Color MsgContentColor = Color.FromArgb(255, 255, 255);
        private static Color MsgRcvColor = Color.FromArgb(203, 203, 203);
        private static Color MsgSendColor = Color.FromArgb(245, 185, 153);

        private const int MAXLEN_MSG = 250;
        public string PatientName { get { return patientName; } }
        private string patientName;

        private RTFDoc rtf = new RTFDoc();

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
            //InsertMsg(msg);
            if (msg.To.Equals(patientName))
            {
                rtf.InsertMsg(msg, RTFDoc.MsgTypeEnum.Send);
            }
            else
            {
                rtf.InsertMsg(msg, RTFDoc.MsgTypeEnum.Rcv);
            }
            tb_history.Rtf = rtf.ToString();

            tb_history.SelectionStart = tb_history.TextLength;
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
                MyMessageBox.Show(ResourceCulture.GetString("msg_too_long"));
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
                MyMessageBox.Show(ResourceCulture.GetString("send_failed"));
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

            //StringBuilder builder = new StringBuilder();
            foreach (var msg in msgs)
            {
                if (msg.To.Equals(patientName))
                {
                    rtf.InsertMsg(msg, RTFDoc.MsgTypeEnum.Send);
                }
                else
                {
                    rtf.InsertMsg(msg, RTFDoc.MsgTypeEnum.Rcv);
                }

                //InsertMsg(msg);

                //builder.AppendFormat("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From).AppendLine();
                //builder.AppendLine(msg.Content);
                //builder.AppendLine();
            }

            if (msgs.Count() > 0)
            {
                tb_history.AppendLine();
                tb_history.AppendLine("----------" + ResourceCulture.GetString("history_msg") + "----------");
                tb_history.AppendLine();
            }

            var unreadMsgs = from msg in MyIMClient.UnreadMsgs
                             where msg.From.Equals(patientName)
                             select msg;

            foreach (var msg in unreadMsgs)
            {
                rtf.InsertMsg(msg, RTFDoc.MsgTypeEnum.Rcv);
                //InsertMsg(msg);
            }

            tb_history.Rtf = rtf.ToString();

            //滚动到最后
            tb_history.SelectionStart = tb_history.TextLength;
            tb_history.ScrollToCaret();

            //移出离线消息
            MyIMClient.RemoveAllMsgs(patientName);

            //离线信息改变则刷新
            MyIMClient.OfflineMsgChanged += MyIMClient_OfflineMsgChanged;

            //点击超链接事件
            tb_history.LinkClicked += tb_history_LinkClicked;

            //界面语言改变事件
            ResourceCulture.LanguageChanged += ResourceCulture_LanguageChanged;

            tb_input.Focus();
        }

        void ResourceCulture_LanguageChanged(EventArgs e)
        {
            InitLanguage();
        }

        void tb_history_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            long record_id;
            string recordStr = e.LinkText.Substring(e.LinkText.LastIndexOf('/') + 1);
            if (long.TryParse(recordStr, out record_id))
            {
                SelfCheckDetailForm form = new SelfCheckDetailForm(record_id);
                form.StartPosition = FormStartPosition.CenterParent;
                form.Show();
            }
        }

        //private void InsertMsg(Msg msg)
        //{
        //    if (msg.From.Equals(LoginStatus.UserInfo.Name))
        //    {
        //        //收到信息
        //        tb_history.AppendLine(string.Format("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From),
        //            MsgRcvFont, MsgRcvColor, HorizontalAlignment.Left);
        //        tb_history.AppendLine(msg.Content, MsgContentFont, MsgContentColor, HorizontalAlignment.Left);
        //    }
        //    else
        //    {
        //        //发送信息
        //        tb_history.AppendLine(string.Format("{0} {1}", msg.Time.ToLocalTime().ToString(), msg.From),
        //            MsgSendFont, MsgSendColor, HorizontalAlignment.Left);
        //        tb_history.AppendLine(msg.Content, MsgContentFont, MsgContentColor, HorizontalAlignment.Left);

        //    }

        //    tb_history.AppendLine();
        //}

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

            ResourceCulture.LanguageChanged -= ResourceCulture_LanguageChanged;
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

        private void picBox_close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private Point mPoint = new Point();

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            FlowLayoutPanel panel = sender as FlowLayoutPanel;

            mPoint.X = e.X + panel.Left;
            mPoint.Y = e.Y + panel.Top;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point newPoint = MousePosition;
                newPoint.Offset(-mPoint.X, -mPoint.Y);
                Location = newPoint;
            }
        }

        private void picBox_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

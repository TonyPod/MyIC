using DoctorClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.UI.Forms
{
    public partial class AddContactForm : Form
    {
        private delegate void FlushClient();
        public AddContactForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化文字
        /// </summary>
        private void InitLanguage()
        {
            //标题栏文字
            this.Text = ResourceCulture.GetString("AddContactForm_text");

            btn_add.Text = ResourceCulture.GetString("btn_add");
            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");

            lbl_group.Text = ResourceCulture.GetString("group");
            lbl_username.Text = ResourceCulture.GetString("username");
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContactForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            cb_groups.Items.AddRange(MyIMClient.Contacts.Keys.ToArray());
        }

        private void AddContact()
        {
            if (string.IsNullOrEmpty(tb_username.Text.Trim()))
            {
                MyMessageBox.Show(ResourceCulture.GetString("please_input_username"));
                cb_groups.Focus();
                return;
            }

            if (cb_groups.SelectedIndex == -1)
            {
                MyMessageBox.Show(ResourceCulture.GetString("please_choose_group"));
                cb_groups.Focus();
                return;
            }

            //检查是否添加自己
            string username = tb_username.Text.Trim();
            if (username.Equals(LoginStatus.UserInfo.Name))
            {
                MyMessageBox.Show(ResourceCulture.GetString("cannot_add_yourself"));
                return;
            }

            //检查是否已经添加
            if (MyIMClient.HasContact(username))
            {
                MyMessageBox.Show(ResourceCulture.GetString("user_already_added"));
                return;
            }

            //发送检查用户名存在的请求
            new Thread(() =>
            {
                JObject jObj = new JObject();
                jObj.Add("username", username);
                string result = HttpHelper.ConnectionForResult("CheckUsernameExistHandler.ashx", jObj.ToString());
                this.Invoke(new FlushClient(() =>
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        MyMessageBox.Show(ResourceCulture.GetString("network_error"));
                        return;
                    }

                    //返回信息解析
                    JObject jObjRes = JObject.Parse(result);
                    string state = (string)jObjRes["state"];
                    if ("not exist".Equals(state))
                    {
                        MyMessageBox.Show(ResourceCulture.GetString("username_not_exist"));
                        tb_username.Focus();
                        return;
                    }
                    else
                    {
                        string groupName = cb_groups.SelectedItem.ToString();
                        MyIMClient.AddContact(username, groupName);
                        this.Close();
                    }         
                }));
            }).Start();
            
        }

        /// <summary>
        /// 点击添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
        {
            AddContact();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddContact();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void picBox_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

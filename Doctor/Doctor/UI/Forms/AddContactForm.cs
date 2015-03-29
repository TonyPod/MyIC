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
                MessageBox.Show("请输入用户名");
                cb_groups.Focus();
                return;
            }

            if (cb_groups.SelectedIndex == -1)
            {
                MessageBox.Show("请选择分组");
                cb_groups.Focus();
                return;
            }

            //检查是否添加自己
            string username = tb_username.Text.Trim();
            if (username.Equals(LoginStatus.UserInfo.Name))
            {
                MessageBox.Show("不能添加自己");
                return;
            }

            //检查是否已经添加
            if (MyIMClient.HasContact(username))
            {
                MessageBox.Show("已添加该用户");
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
                        MessageBox.Show("网络故障");
                        return;
                    }

                    //返回信息解析
                    JObject jObjRes = JObject.Parse(result);
                    string state = (string)jObjRes["state"];
                    if ("not exist".Equals(state))
                    {
                        MessageBox.Show("用户名不存在");
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
    }
}

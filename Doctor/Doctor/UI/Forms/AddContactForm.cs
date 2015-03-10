using DoctorClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doctor.UI.Forms
{
    public partial class AddContactForm : Form
    {
        public AddContactForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContactForm_Load(object sender, EventArgs e)
        {
            cb_groups.Items.AddRange(MyIMClient.Contacts.Keys.ToArray());
        }

        /// <summary>
        /// 点击添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, EventArgs e)
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

            //发送检查用户名重复的请求
            JObject jObj = new JObject();
            jObj.Add("username", username);
            string result = HttpHelper.ConnectionForResult("CheckUsernameExistHandler.ashx", jObj.ToString());
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("网络故障");
                return;
            }

            //返回信息解析
            JObject jObjRes = JObject.Parse(result);
            string state = (string)jObjRes["state"];
            if ("exist".Equals(state))
            {
                MessageBox.Show("用户名不存在");
                tb_username.Focus();
                return;
            }
            else
            {
                string groupName = cb_groups.SelectedItem.ToString();
                MyIMClient.AddContact(username, groupName);
                MessageBox.Show("添加成功");
                this.Close();
            }
        }
    }
}

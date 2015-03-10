using Doctor.Model;
using Doctor.Properties;
using Doctor.Util;
using DoctorClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Doctor.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 点击事件：取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击事件：忘记密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void link_forgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        /// <summary>
        /// 点击事件：登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_login_Click(object sender, EventArgs e)
        {
            JObject jObj = new JObject();

            string username = tb_username.Text.Trim();
            string password = tb_password.Text;

            jObj.Add("username", username);
            jObj.Add("password", MD5.GetMD5(password));

            string urlStr = "LoginHandler.ashx";

            this.Cursor = Cursors.WaitCursor;
            string result = HttpHelper.ConnectionForResult(urlStr, jObj.ToString());
            this.Cursor = Cursors.Default;

            //连接失败
            if (null == result)
	        {
		        MessageBox.Show("连接失败");
                return;
	        }

            //连接成功，分析状态码
            JObject jObjResult = JObject.Parse(result);
            string state = (string)jObjResult.Property("state");
            switch (state)
            {
                case "username not exist":
                    MessageBox.Show("用户名不存在");
                    break;
                case "success":
                    //得到医生信息
                    string content = (string)jObjResult.Property("content");
                    DoctorModel doctorModel = JsonConvert.DeserializeObject<DoctorModel>(content);

                    //保存到全局数据
                    LoginStatus.SaveLoginStatus(doctorModel);
                    this.Close();

                    break;
                case "password error":
                    MessageBox.Show("密码不正确");
                    break;
                default:
                    MessageBox.Show("服务器返回数据异常");
                    return;
            }
        }

        /// <summary>
        /// 窗口载入时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //用户名输入控件显示上次登录用户名
            tb_username.Text = Settings.Default.LastUserName;
        }
    }
}

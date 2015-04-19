using Doctor.Model;
using Doctor.Properties;
using Doctor.UI.Forms;
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
using System.Threading;
using System.Threading.Tasks;
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

        private delegate void FlushCursorHandler(Cursor cursor);
        private void FlushCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                FlushCursorHandler handler = new FlushCursorHandler(FlushCursor);
                this.Invoke(handler, cursor);
            }
            else
            {
                this.Cursor = cursor;
            }
        }

        private delegate void SafeCloseHandler();
        private void SafeClose()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SafeCloseHandler(SafeClose));
            }
            else
            {
                this.Close();
            }
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
            Login();
        }

        //正在连接
        private Task<string> task;

        private void Login()
        {
            JObject jObj = new JObject();

            string username = tb_username.Text.Trim();
            string password = tb_password.Text;

            //检查输入是否为空
            if (string.IsNullOrEmpty(username))
            {
                MyMessageBox.Show(ResourceCulture.GetString("please_input_username"));
                tb_username.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MyMessageBox.Show(ResourceCulture.GetString("please_input_password"));
                tb_password.Focus();
                return;
            }

            //检查是否正在连接
            if (task != null && task.Status == TaskStatus.Running)
            {
                return;
            }

            //组装JSON字符串
            jObj.Add("username", username);
            jObj.Add("password", MD5.GetMD5(password));

            string urlStr = "LoginHandler.ashx";

            FlushCursor(Cursors.WaitCursor);

            //将用户名和密码放入JSON字符串中并发送HTTP请求，取得返回结果并分析（异步）
            task = HttpHelper.ConnectionForResultAsync(urlStr, jObj.ToString());

            task.ContinueWith((curTask) =>
            {
                //光标切换回正常
                FlushCursor(Cursors.Default);

                //取得Task的返回结果
                string result = curTask.Result;

                //没有任何响应：连接失败
                if (string.IsNullOrEmpty(result))
                {
                    MyMessageBox.Show(ResourceCulture.GetString("connect_timeout"));
                    return;
                }

                //连接成功，分析状态码
                JObject jObjResult = JObject.Parse(result);
                string state = (string)jObjResult.Property("state");
                switch (state)
                {
                    case "username not exist":
                        MyMessageBox.Show(ResourceCulture.GetString("username_not_exist"));
                        break;
                    case "success":
                        //得到医生信息
                        string content = (string)jObjResult.Property("content");
                        DoctorModel doctorModel = JsonConvert.DeserializeObject<DoctorModel>(content);

                        //保存到全局数据
                        LoginStatus.SaveLoginStatus(doctorModel);
                        this.SafeClose();

                        break;
                    case "password error":
                        MyMessageBox.Show(ResourceCulture.GetString("password_error"));
                        break;
                    default:
                        MyMessageBox.Show(ResourceCulture.GetString("data_error"));
                        return;
                } 
            });
        }

        /// <summary>
        /// 初始化文字
        /// </summary>
        private void InitLanguage()
        {
            //标题栏文字
            this.Text = ResourceCulture.GetString("LoginForm_text");

            lbl_password.Text = ResourceCulture.GetString("password");
            lbl_username.Text = ResourceCulture.GetString("username");

            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");
            btn_login.Text = ResourceCulture.GetString("btn_login");
        }

        /// <summary>
        /// 窗口载入时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            //用户名输入控件显示上次登录用户名
            tb_username.Text = Settings.Default.LastUserName;
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
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

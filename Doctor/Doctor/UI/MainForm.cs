using Doctor.Forms;
using Doctor.Panels;
using Doctor.Properties;
using Doctor.UI.Forms;
using Doctor.Util;
using DoctorClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Doctor
{
    public partial class MainForm : Form
    {
        //子窗体
        private SelfInfoForm selfInfoForm;
        private ContactsForm contactsForm;
        private SelfCheckForm selfCheckForm;

        private delegate void FlushClient();
        private delegate void LogoutSafeHandler();
        private delegate void LoginSafeHandler();
        private delegate void FlushConnectingClient(ConnectingEventArgs e);

        public MainForm()
        {
            InitializeComponent();
        }

        private void GenSubFormAndShow()
        {
            selfInfoForm = new SelfInfoForm();
            selfInfoForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            selfInfoForm.TopLevel = false;

            contactsForm = new ContactsForm();
            contactsForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            contactsForm.TopLevel = false;

            selfCheckForm = new SelfCheckForm();
            selfCheckForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            selfCheckForm.TopLevel = false;

            contactsForm.Show();
            selfInfoForm.Show();
            selfCheckForm.Show();
        }

        /// <summary>
        /// 切换文字
        /// </summary>
        private void InitLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FlushClient(InitLanguage));
            }
            else
            {
                lbl_about.Text = ResourceCulture.GetString("about");
                lbl_contacts.Text = ResourceCulture.GetString("contacts");
                lbl_selfCheck.Text = ResourceCulture.GetString("self_check");
                lbl_selfInfo.Text = ResourceCulture.GetString("self_info");

                lbl_settings.Text = ResourceCulture.GetString("settings");
                lbl_logout.Text = ResourceCulture.GetString("logout");
                lbl_register.Text = ResourceCulture.GetString("register");
                lbl_quit.Text = ResourceCulture.GetString("quit");
            }
        }

        /// <summary>
        /// 窗口载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //设置语言环境
            ResourceCulture.SetCurrentCulture(Settings.Default.Language);

            //初始化窗口语言
            InitLanguage();

            //窗口标题栏
            this.Text = ResourceCulture.GetString("MainForm_text");

            //载入时为用户登出状态
            LogoutSafe();

            //注册登录信息改变时事件
            LoginStatus.LoginStatusChanged += LoginStatus_LoginStatusChanged;

            //地理信息改变事件
            LoginStatus.IPLocationChanged += LoginStatus_IPLocationChanged;
            
            MyIMClient.ConnectionClosed += MyIMClient_ConnectionClosed;

            //语言改变事件
            ResourceCulture.LanguageChanged += ResourceCulture_LanguageChanged;

            //每5分钟更新一下IP位置信息
            LoginStatus.RefreshIP();
            Timer timer = new Timer();
            timer.Enabled = true;
            //timer.Interval = 5 * 60 * 1000;
            timer.Interval = 5 * 1000;
            timer.Tick += (a, b) => { LoginStatus.RefreshIP(); };
            timer.Start();

            lbl_loc.Text = "无法获取地理位置";
        }

        private void ResourceCulture_LanguageChanged(EventArgs e)
        {
            InitLanguage();
        }

        private void MyIMClient_ConnectionSuspended()
        {
            if (lbl_imStatus.InvokeRequired)
            {
                this.Invoke(new FlushClient(MyIMClient_ConnectionSuspended));
            }
            else
            {
                lbl_imStatus.Text = "即时通讯连接失败";
            }
        }

        private void MyIMClient_ConnectionEstablished()
        {
            if (lbl_imStatus.InvokeRequired)
            {
                this.Invoke(new FlushClient(MyIMClient_ConnectionEstablished));
            }
            else
            {
                lbl_imStatus.Text = "即时通讯正常";
            }
        }

        /// <summary>
        /// IP地址信息改变时触发
        /// </summary>
        /// <param name="e"></param>
        private void LoginStatus_IPLocationChanged(EventArgs e)
        {
            ChangeIPLabel();
        }

        private void ChangeIPLabel()
        {
            if (this.InvokeRequired)
            {
                FlushClient client = new FlushClient(ChangeIPLabel);
                this.Invoke(client);
            }
            else
            {
                if (LoginStatus.UserIP != null)
                {
                    lbl_loc.Text = LoginStatus.UserIP.ToString() + "的用户";
                }
                else
                {
                    lbl_loc.Text = "无法获取地理位置";
                }
            }
        }

        private void LoginSafe()
        {
            if (this.InvokeRequired)
            {
                LoginSafeHandler handler = new LoginSafeHandler(LoginSafe);
                this.Invoke(handler);
            }
            else
            {
                //状态栏显示
                lbl_status.Text = LoginStatus.UserInfo.Name;

                //左侧按钮启用
                panel_selfCheck.Enabled = true;
                panel_contacts.Enabled = true;
                panel_selfInfo.Enabled = true;
                panel_logout.Enabled = true;
                picBox_login.Enabled = false;

                //picBox_check.Image = Resources.自检信息2;
                //picBox_message.Image = Resources.联系人2;
                //picBox_selfInfo.Image = Resources.个人信息2;
                //picBox_logout.Image = Resources.注销1;
                //picBox_login.Image = Resources.登陆1;

                //右侧Panel清除所有
                panel.Controls.Clear();

                //将登录名存入设置文件中
                Settings.Default.LastUserName = LoginStatus.UserInfo.Name;
                Settings.Default.Save();

                //注册即时通讯服务器连接成功、失败的事件（要放在GenSubFormAndShow()前面，因为在GenSubFormAndShow
                //中就开始连接服务器了）
                MyIMClient.ConnectionEstablished += MyIMClient_ConnectionEstablished;
                MyIMClient.ConnectionSuspended += MyIMClient_ConnectionSuspended;
                MyIMClient.Connecting += MyIMClient_Connecting;

                //显示Panel窗体
                GenSubFormAndShow();
            }
        }

        private void MyIMClient_ConnectionClosed()
        {
            if (lbl_imStatus.InvokeRequired)
            {
                this.Invoke(new FlushClient(MyIMClient_ConnectionClosed));
            }
            else
            {
                lbl_imStatus.Text = "即时通讯未连接";
            }
        }

        private void MyIMClient_Connecting(ConnectingEventArgs e)
        {
            if (lbl_imStatus.InvokeRequired)
            {
                this.Invoke(new FlushConnectingClient(MyIMClient_Connecting), e);
            }
            else
            {
                lbl_imStatus.Text = string.Format("即时通讯连接失败，尝试第{0}次重连", e.CurTime);
            }
        }


        private void LogoutSafe()
        {
            if (this.InvokeRequired)
            {
                LogoutSafeHandler handler = new LogoutSafeHandler(LogoutSafe);
                this.Invoke(handler);
            }
            else
            {
                //状态栏显示
                lbl_status.Text = "请登录";

                //左侧按钮禁用
                panel_selfCheck.Enabled = false;
                panel_contacts.Enabled = false;
                panel_selfInfo.Enabled = false;
                panel_logout.Enabled = false;
                picBox_login.Enabled = true;

                PanelNotClicked();

                //picBox_check.Enabled = false;
                //picBox_message.Enabled = false;
                //picBox_selfInfo.Enabled = false;
                //picBox_logout.Enabled = false;
                //picBox_login.Enabled = true;

                //picBox_check.Image = Resources.查看自检_灰_;
                //picBox_message.Image = Resources.联系人_灰_;
                ////LoginStatus.UserInfo = new Model.DoctorModel() { Name = "tony" };
                //picBox_selfInfo.Image = Resources.个人信息_灰_;
                //picBox_logout.Image = Resources.注销_灰_;
                //picBox_login.Image = Resources.登录;

                //右侧Panel清除所有
                panel.Controls.Clear();

                if (selfInfoForm != null)
                {
                    selfCheckForm.Close();
                    selfInfoForm.Close();
                    contactsForm.Close();
                }

                MyIMClient.ConnectionEstablished -= MyIMClient_ConnectionEstablished;
                MyIMClient.ConnectionSuspended -= MyIMClient_ConnectionSuspended;
                MyIMClient.Connecting -= MyIMClient_Connecting;

            }
        }

        /// <summary>
        /// 登录信息改变时事件
        /// </summary>
        /// <param name="e"></param>
        private void LoginStatus_LoginStatusChanged(EventArgs e)
        {
            if (LoginStatus.UserInfo != null)
            {
                //登录状态
                LoginSafe();
            }
            else
            {
                //登出状态
                LogoutSafe();
            }
        }

        /// <summary>
        /// 点击事件：登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_login_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        /// <summary>
        /// 点击事件：注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_register_Click(object sender, EventArgs e)
        {
            NewRegisterForm form = new NewRegisterForm();
            //RegisterForm form = new RegisterForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        /// <summary>
        /// 点击事件：注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_logout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要注销吗？", "注销", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //清除保存的登录状态
                LoginStatus.Clear();

                //关闭即时通讯
                MyIMClient.Close();

                //清除panel中的内容
                panel.Controls.Clear();
            }
        }

        /// <summary>
        /// 点击事件：个人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_selfInfo_Click(object sender, EventArgs e)
        {
            //SelfInfoForm form = new SelfInfoForm();
            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //form.TopLevel = false;
            //this.panel.Controls.Clear();
            //this.panel.Controls.Add(form);
            //form.Show();

            Panel panel = sender as Panel;
            PanelNotClicked();
            panel_selfInfo.BackColor = Color.FromArgb(126, 131, 126);

            this.panel.Controls.Clear();
            this.panel.Controls.Add(selfInfoForm);
        }

        /// <summary>
        /// 使右上角的按钮显示没有点击的状态
        /// </summary>
        private void PanelNotClicked()
        {
            foreach (Panel panel in flowLayoutPanel1.Controls)
            {
                panel.BackColor = flowLayoutPanel1.BackColor;
            }
        }

        /// <summary>
        /// 点击事件：查看自检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_check_Click(object sender, EventArgs e)
        {
            PanelNotClicked();
            panel_selfCheck.BackColor = Color.FromArgb(126, 131, 126);

            this.panel.Controls.Clear();
            this.panel.Controls.Add(selfCheckForm);
            selfCheckForm.RefreshSelfChecks();
        }

        /// <summary>
        /// 点击事件：即时通讯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_message_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            PanelNotClicked();
            panel_contacts.BackColor = Color.FromArgb(126, 131, 126);
            this.panel.Controls.Clear();
            this.panel.Controls.Add(contactsForm);

            if (!MyIMClient.Connected)
            {
                MyIMClient.ConnectAsync();
            }
        }

        /// <summary>
        /// 点击事件：显示帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_help_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        ///// <summary>
        ///// 窗体将要关闭时触发事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    if (MessageBox.Show("确定要退出吗？", "退出", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
        //    {
        //        e.Cancel = true;
        //    } 
        //}

        private void picBox_settings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private Point mPoint = new Point();

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            Control com = sender as Control;

            mPoint.X = e.X + com.Left;
            mPoint.Y = e.Y + com.Top;
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

        /// <summary>
        /// 点击事件：退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picBox_exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "退出", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

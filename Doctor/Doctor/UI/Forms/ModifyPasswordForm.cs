using Doctor.UI.Forms;
using Doctor.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.Forms
{
    public partial class ModifyPasswordForm : Form
    {
        private delegate void FlushClient();

        public ModifyPasswordForm()
        {
            InitializeComponent();
        }

        private void Confirm()
        {
            //1.检查旧密码不为空
            string oldPwd = tb_oldPassword.Text;
            if (string.IsNullOrEmpty(oldPwd))
            {
                tb_oldPassword.Focus();
                return;
            }

            //2.检查新密码不为空
            string newPwd = tb_newPassword.Text;
            if (string.IsNullOrEmpty(newPwd))
            {
                tb_newPassword.Focus();
                return;
            }

            //3.检查两次输入密码是否相符
            string newPwdAgain = tb_newPasswordAgain.Text;
            if (!newPwdAgain.Equals(newPwd))
            {
                MyMessageBox.Show(ResourceCulture.GetString("password_do_not_match"));
                tb_newPasswordAgain.Focus();
                return;
            }

            JObject jObj = new JObject();
            jObj.Add("doc_id", LoginStatus.UserInfo.Doc_id);
            jObj.Add("old_password", MD5.GetMD5(oldPwd));
            jObj.Add("new_password", MD5.GetMD5(newPwd));

            new Thread(() =>
            {
                this.Cursor = Cursors.WaitCursor;
                //将JSON数组发送给指定URL并分析返回字符串
                string result = HttpHelper.ConnectionForResult("ModifyPasswordHandler.ashx", jObj.ToString());
                this.Cursor = Cursors.Default;

                this.Invoke(new FlushClient(() =>
                {
                    //连接失败
                    if (null == result)
                    {
                        MyMessageBox.Show(ResourceCulture.GetString("network_error"));
                        return;
                    }

                    JObject jResponseObj = JObject.Parse(result);
                    string state = (string)jResponseObj.Property("state");

                    switch (state)
                    {
                        case "success":
                            MyMessageBox.Show(ResourceCulture.GetString("modify_complete"));
                            this.Close();
                            break;
                        case "password error":
                            MyMessageBox.Show(ResourceCulture.GetString("password_error"));
                            tb_oldPassword.Focus();
                            break;
                        case "failed":
                            MyMessageBox.Show(ResourceCulture.GetString("modify_failed"));
                            break;
                        default:
                            break;
                    }
                }));
            }).Start();
        }

        /// <summary>
        /// 点击事件：确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirm_Click(object sender, EventArgs e)
        {
            Confirm();
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

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Confirm();
            }
        }

        private void ModifyPasswordForm_Load(object sender, EventArgs e)
        {
            //标题栏文字
            this.Text = ResourceCulture.GetString("ModifyPasswordForm_text");

            InitLanguage();
        }

        private void InitLanguage()
        {
            btn_confirm.Text = ResourceCulture.GetString("btn_confirm");
            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");

            label1.Text = ResourceCulture.GetString("old_password");
            label2.Text = ResourceCulture.GetString("new_password");
            label3.Text = ResourceCulture.GetString("new_password_again");
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

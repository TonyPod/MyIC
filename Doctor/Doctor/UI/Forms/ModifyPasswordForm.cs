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
                MessageBox.Show("请输入旧密码");
                tb_oldPassword.Focus();
                return;
            }

            //2.检查新密码不为空
            string newPwd = tb_newPassword.Text;
            if (string.IsNullOrEmpty(newPwd))
            {
                MessageBox.Show("请输入新密码");
                tb_newPassword.Focus();
                return;
            }

            //3.检查两次输入密码是否相符
            string newPwdAgain = tb_newPasswordAgain.Text;
            if (!newPwdAgain.Equals(newPwd))
            {
                MessageBox.Show("两次输入密码不相符");
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
                        MessageBox.Show("连接失败");
                        return;
                    }

                    JObject jResponseObj = JObject.Parse(result);
                    string state = (string)jResponseObj.Property("state");

                    switch (state)
                    {
                        case "success":
                            MessageBox.Show("修改成功");
                            this.Close();
                            break;
                        case "password error":
                            MessageBox.Show("密码错误");
                            tb_oldPassword.Focus();
                            break;
                        case "failed":
                            MessageBox.Show("修改密码失败");
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
        }
    }
}

using Doctor.Forms;
using Doctor.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Doctor.Panels
{
    public partial class SelfInfoForm : Form
    {
        /// <summary>
        /// 窗体：医生个人信息
        /// </summary>
        public SelfInfoForm()
        {
            InitializeComponent();
        }

        private delegate void FlushClient();
        private delegate void FlushImageHandler(PictureBox picBox, string photoPath);
        private delegate void FlushFormCursorHandler(Cursor cursor);
        private delegate void FlushLabelHandler(Label label, string text);
        private delegate void FlushLabelAndColorHandler(Label label, string text, Color color);

        private void FlushLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                FlushLabelHandler handler = new FlushLabelHandler(FlushLabel);
                this.Invoke(handler, label, text);
            }
            else
            {
                label.Text = text;
            }
        }

        private void FlushLabelAndColor(Label label, string text, Color color)
        {
            if (label.InvokeRequired)
            {
                FlushLabelAndColorHandler handler = new FlushLabelAndColorHandler(FlushLabelAndColor);
                this.Invoke(handler, label, text, color);
            }
            else
            {
                label.Text = text;
                label.ForeColor = color;
                if (!label.Visible)
                {
                    label.Visible = true;
                }
            }
        }

        private void FlushFormCursor(Cursor cursor)
        {
            if (this.InvokeRequired)
            {
                FlushFormCursorHandler handler = new FlushFormCursorHandler(FlushFormCursor);
                this.Invoke(handler, cursor);
            }
            else
            {
                this.Cursor = cursor;
            }
        }

        private void FlushImage(PictureBox picBox, string photoPath)
        {
            if (picBox.InvokeRequired)
            {
                FlushImageHandler handler = new FlushImageHandler(FlushImage);
                this.Invoke(handler, picBox, photoPath);
            }
            else
            {
                Image image = Image.FromFile(photoPath);
                //Image newImage = KiResizeImage(image, picBox.Width, picBox.Height);
                picBox.Image = image;
                //picBox.BackgroundImage = image;
            }
        }

        /// <summary>
        /// 点击事件：修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void link_modifyPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModifyPasswordForm form = new ModifyPasswordForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void SelfInfoForm_Load(object sender, EventArgs e)
        {
            //国际化
            InitLanguage();

            //清空显示的数据
            lbl_hospital.Text = "";
            lbl_username.Text = "";
            lbl_realname.Text = "";
            lbl_license.Text = "";

            ResourceCulture.LanguageChanged += ResourceCulture_LanguageChanged;

            new Thread(() =>
            {
                FlushFormCursor(Cursors.WaitCursor);
                if (null != LoginStatus.UserInfo && !string.IsNullOrEmpty(LoginStatus.UserInfo.PhotoPath))
                {
                    DoctorModel userInfo = LoginStatus.UserInfo;

                    //下载并显示个人照片
                    string photoPath = Path.Combine(GeneralHelper.DownloadPicFolder, userInfo.PhotoPath);
                    if (!File.Exists(photoPath))
                    {
                        if (HttpHelper.DownloadFile("PicDownloadHandler.ashx", userInfo.PhotoPath))
                        {
                            FlushImage(picBox_photo, photoPath);
                        }
                    }
                    else
                    {
                        FlushImage(picBox_photo, photoPath);
                    }

                    //下载并显示医师证
                    string licensePath = Path.Combine(GeneralHelper.DownloadPicFolder, userInfo.LicensePath);
                    if (!File.Exists(licensePath))
                    {
                        if (HttpHelper.DownloadFile("PicDownloadHandler.ashx", userInfo.LicensePath))
                        {
                            FlushImage(picBox_license, licensePath);
                        }
                    }
                    else
                    {
                        FlushImage(picBox_license, licensePath);
                    }

                    //其他UI控件显示
                    FlushLabel(lbl_username, userInfo.Name);

                    if (userInfo.Hospital_id == null)
                    {
                        this.Invoke(new FlushClient(() => { lbl_hospital.Text = ResourceCulture.GetString("not_written"); }));
                    }
                    else
                    {
                        string responseStr = HttpHelper.ConnectionForResult("HospitalInfoHandler.ashx", userInfo.Hospital_id.ToString());
                        if (!string.IsNullOrEmpty(responseStr))
                        {
                            HospitalModel hospital = JsonConvert.DeserializeObject<HospitalModel>(responseStr);
                            FlushLabel(lbl_hospital, hospital.Name);
                        }
                        else
                        {
                            FlushLabel(lbl_hospital, "");
                        }
                    }

                    FlushLabel(lbl_license, (userInfo.LicenseNo == null ? ResourceCulture.GetString("not_written") : userInfo.LicenseNo));
                    FlushLabel(lbl_realname, (userInfo.RealName == null ? ResourceCulture.GetString("not_written") : userInfo.RealName));

                    //认证与否
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new FlushClient(() => 
                        {
                            if (userInfo.IfAuth)
                            {
                                lbl_ifAuth.Text = ResourceCulture.GetString("have_auth");
                                lbl_ifAuth.ForeColor = Color.FromArgb(245, 252, 255);
                            }
                            else
                            {
                                lbl_ifAuth.Text = ResourceCulture.GetString("not_auth");
                                lbl_ifAuth.ForeColor = Color.FromArgb(232, 161, 123);
                            }
                        }));
                    }
                }

                FlushFormCursor(Cursors.Default);
            }).Start();
        }

        void ResourceCulture_LanguageChanged(EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            groupBox1.Text = ResourceCulture.GetString("basic_info");
            groupBox2.Text = ResourceCulture.GetString("auth_info");

            link_modifyPassword.Text = ResourceCulture.GetString("modify_password");
            label4.Text = ResourceCulture.GetString("real_name");
            label3.Text = ResourceCulture.GetString("hospital_in");
            label2.Text = ResourceCulture.GetString("license_no");
            label6.Text = ResourceCulture.GetString("license_photo");

            lbl_photo.Text = ResourceCulture.GetString("self_photo");
            lbl_ifAuth.Text = ResourceCulture.GetString("not_auth");
        }

        private void SelfInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResourceCulture.LanguageChanged -= ResourceCulture_LanguageChanged;
        }
    }
}

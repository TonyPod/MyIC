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

        public static Image KiResizeImage(Image img, int newW, int newH)
        {
            try
            {
                Bitmap bmp = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(img);

                // 插值算法的质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return bmp;
            }
            catch
            {
                return null;
            }
        }
        private void SelfInfoForm_Load(object sender, EventArgs e)
        {
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
                        FlushLabel(lbl_hospital, "未填写");
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

                    FlushLabel(lbl_license, (userInfo.LicenseNo == null ? "未填写" : userInfo.LicenseNo));
                    FlushLabel(lbl_realname, (userInfo.RealName == null ? "未填写" : userInfo.RealName));

                    //认证与否
                    if (userInfo.IfAuth)
                    {
                        FlushLabelAndColor(lbl_ifAuth, "已认证", Color.Black);
                    }
                    else
                    {
                        FlushLabelAndColor(lbl_ifAuth, "未认证", Color.Red);
                    }
                }

                FlushFormCursor(Cursors.Default);
            }).Start();
        }
    }
}

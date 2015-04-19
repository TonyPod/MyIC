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
    /// <summary>
    /// 自检图片大图显示窗体
    /// </summary>
    public partial class PicLargeForm : Form
    {
        private string fileName;
        public PicLargeForm(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
        }

        /// <summary>
        /// 窗体载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicLargeForm_Load(object sender, EventArgs e)
        {
            //标题栏文字
            this.Text = ResourceCulture.GetString("PicLargeForm_text");

            pictureBox1.Image = GeneralHelper.GetPhoto(fileName);
            
            ResizeImage();

            this.KeyDown += PicLargeForm_KeyDown;
        }

        private void ResizeImage()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;

            int imgW = pictureBox1.Image.Width;
            int imgH = pictureBox1.Image.Height;

            float ratio = screenW / (float)screenH;
            float imgRatio = imgW / (float)imgH;

            int newW = imgW;
            int newH = imgH;

            //判断图片是水平过长还是垂直过长
            if (imgW > screenW && imgH <= screenH)
            {
                newW = screenW;
                newH = newW * imgH / imgW;
            }
            else if (imgW <= screenW && imgH > screenH)
            {
                newH = screenH;
                newW = newH * imgW / imgH;
            }
            else if (imgW > screenW && imgH > screenH)
            {
                if (imgRatio > ratio)
                {
                    newW = screenW;
                    newH = newW * imgH / imgW;
                }
                else
                {
                    newH = screenH;
                    newW = newH * imgW / imgH;
                }
            }

            this.Width = newW;
            this.Height = newH;
        }

        void PicLargeForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
        private Point mPoint = new Point();

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.Close();
            }

            PictureBox panel = sender as PictureBox;

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

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}

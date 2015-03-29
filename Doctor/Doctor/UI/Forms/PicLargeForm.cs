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
        }
    }
}

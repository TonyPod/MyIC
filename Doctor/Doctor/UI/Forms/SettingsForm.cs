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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void InitLanguage()
        {
            this.Text = ResourceCulture.GetString("SettingsForm_text");
            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");
            btn_save.Text = ResourceCulture.GetString("btn_save");

            groupBox1.Text = ResourceCulture.GetString("language");
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            InitLanguage();

            //加载语言设置
            string region = ResourceCulture.GetCurrentCultureName();

            //初始化RadioButton的选择状态
            foreach (RadioButton item in groupBox1.Controls)
            {
                //rb_en_US
                string name = item.Name;

                //en_US
                string temp = name.Substring(name.IndexOf('_') + 1);
                item.Text = ResourceCulture.GetString(temp);

                //en-US
                string curRegion = temp.Replace('_', '-');
                if (curRegion.Equals(region))
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
        }

        /// <summary>
        /// 点击事件：保存选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            //查看选中的语言并保存
            foreach (RadioButton item in groupBox1.Controls)
            {
                if (item.Checked)
                {
                    string name = item.Name;
                    string curRegion = name.Substring(name.IndexOf('_') + 1).Replace('_', '-');
                    ResourceCulture.SetCurrentCulture(curRegion);
                }
            }

            //关闭窗体
            this.Close();
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

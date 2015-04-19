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
    public partial class MyMessageBox : Form
    {
        public enum MyMessageBoxButtons
        {
            YesNo = 0,
            OK = 1
        }

        private MyMessageBox(string text, string caption, MyMessageBoxButtons option)
        {
            InitializeComponent();
            flowLayoutPanel2.Controls.Clear();

            this.label1.Text = text;
            this.StartPosition = FormStartPosition.CenterParent;

            switch (option)
            {
                case MyMessageBoxButtons.YesNo:
                    Button btn_no = new Button();
                    btn_no.BackColor = Color.FromArgb(224, 224, 224);
                    btn_no.FlatStyle = FlatStyle.Flat;
                    btn_no.FlatAppearance.BorderSize = 0;
                    btn_no.Text = ResourceCulture.GetString("btn_no");
                    btn_no.DialogResult = DialogResult.No;
                    this.flowLayoutPanel2.Controls.Add(btn_no);

                    Button btn_yes = new Button();
                    btn_yes.BackColor = Color.FromArgb(224, 224, 224);
                    btn_yes.FlatStyle = FlatStyle.Flat;
                    btn_yes.FlatAppearance.BorderSize = 0;
                    btn_yes.Text = ResourceCulture.GetString("btn_yes");
                    btn_yes.DialogResult = DialogResult.Yes;
                    this.flowLayoutPanel2.Controls.Add(btn_yes);
                    break;

                case MyMessageBoxButtons.OK:
                    Button btn_ok = new Button();
                    btn_ok.BackColor = Color.FromArgb(224, 224, 224);
                    btn_ok.FlatStyle = FlatStyle.Flat;
                    btn_ok.FlatAppearance.BorderSize = 0;
                    btn_ok.Text = ResourceCulture.GetString("btn_ok");
                    btn_ok.DialogResult = DialogResult.OK;
                    this.flowLayoutPanel2.Controls.Add(btn_ok);
                    break;
                default:
                    break;
            }
        }

        private void MyMessageBox_Load(object sender, EventArgs e)
        {
            InitLanguage();
        }

        private void InitLanguage()
        {
            btn_ok.Text = ResourceCulture.GetString("btn_ok");
            btn_cancel.Text = ResourceCulture.GetString("btn_cancel");
        }

        public static DialogResult Show(string text, MyMessageBoxButtons option = MyMessageBoxButtons.OK)
        {
            var msgBox = new MyMessageBox(text, "", option);
            return msgBox.ShowDialog();
        }

        public static DialogResult Show(string text, string caption, MyMessageBoxButtons option = MyMessageBoxButtons.OK)
        {
            var msgBox = new MyMessageBox(text, caption, option);
            return msgBox.ShowDialog();
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

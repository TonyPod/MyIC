using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IMClient_WinForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (MyIMClient.Connect())
            {
                MyIMClient.Login(tb_username.Text.Trim());

                new IMForm().ShowDialog();                    
            }
            else
            {
                MessageBox.Show("连接失败");
            }
        }
    }
}

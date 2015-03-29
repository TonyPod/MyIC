using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Doctor.Forms;
using DoctorClient;
using System.IO;

namespace Doctor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;

            //加载省市县数据
            if (!GeneralHelper.LoadLocationData())
            {
                MessageBox.Show("文件丢失，请重新安装程序");
                Application.Exit();
            }
            else
            {
                Application.Run(new MainForm());
            }
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //停止即时通讯服务
            MyIMClient.Close();
        }
    }
}

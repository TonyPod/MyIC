using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Doctor.Forms;
using DoctorClient;
using System.IO;
using Doctor.UI.Forms;
using System.Text;

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


            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; 

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

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MyMessageBox.Show(ResourceCulture.GetString("unhandled_exception"));
            WriteToLog(e.ToString());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            //停止即时通讯服务
            MyIMClient.Close();
        }

        const string LogFileName = "error_log.txt";
        static void WriteToLog(string log)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Time: {0}", DateTime.Now).AppendLine();
            builder.AppendFormat("Error: {0}", log).AppendLine();
            builder.AppendLine();

            File.AppendText(builder.ToString());
        }
    }
}

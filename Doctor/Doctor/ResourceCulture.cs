using Doctor.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace Doctor
{
    class ResourceCulture
    {
        /// <summary>
        /// 从Settings文件中读取当前区域
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCulture()
        {
            return Settings.Default.Language;
        }

        public delegate void LanguageChangedHandler(EventArgs e);
        public static event LanguageChangedHandler LanguageChanged;
        private static void OnLanguageChanged(EventArgs e)
        {
            if (LanguageChanged != null)
            {
                LanguageChanged(e);
            }
        }

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="name"></param>
        public static void SetCurrentCulture(string name)
        {
            //name为空则语言为英文
            if (string.IsNullOrEmpty(name))
            {
                name = "en-US";
            }

            //调整界面语言
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(name);

            //保存到配置文件中
            Settings.Default.Language = name;
            Settings.Default.Save();

            //触发事件
            OnLanguageChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 通过id获取当前区域环境下的字符串
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetString(string id)
        {
            string strCurLanguage = "";
            try
            {
                ResourceManager rm = new ResourceManager("Doctor.Resource", Assembly.GetExecutingAssembly());
                CultureInfo info = Thread.CurrentThread.CurrentCulture;
                strCurLanguage = rm.GetString(id, info);
            }
            catch (Exception)
            {
                strCurLanguage = "No id:" + id + ",please add.";                
            }
            return strCurLanguage;
        }
    }
}

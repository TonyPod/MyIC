using Doctor.Properties;
using Doctor.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static CultureInfo mCultureInfo;

        /// <summary>
        /// 从Settings文件中读取当前区域
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentCultureName()
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
            mCultureInfo = new System.Globalization.CultureInfo(name);
            Thread.CurrentThread.CurrentCulture = mCultureInfo;

            //保存到配置文件中
            Settings.Default.Language = name;
            Settings.Default.Save();

            //触发事件
            OnLanguageChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 主界面的左侧Label的字号
        /// </summary>
        /// <returns></returns>
        public static Font GetMainFormLabelFont()
        {
            switch (Settings.Default.Language)
            {
                case "en-US":
                    return new Font("宋体", 10);
                case "zh-CN":
                    return new Font("宋体", 12);
                default:
                    return new Font("宋体", 10);
            }
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
                //CultureInfo info = Thread.CurrentThread.CurrentCulture;
                strCurLanguage = rm.GetString(id, mCultureInfo);
            }
            catch (Exception)
            {
                strCurLanguage = "No id:" + id + ",please add.";                
            }
            return strCurLanguage;
        }

        internal static string GetLocationString(Model.IPRecord iPRecord)
        {
            if (iPRecord == null)
            {
                return GetString("unable_to_locate");
            }

            string str = null;
            switch (Settings.Default.Language)
            {
                case "en-US":
                    StringBuilder builder = new StringBuilder();
                    if (!string.IsNullOrEmpty(iPRecord.District))
                    {
                        builder.Append(GeneralHelper.GetAreaEnglish(iPRecord.District));
                        builder.Append(" ");
                    }
                    if (!string.IsNullOrEmpty(iPRecord.City))
                    {
                        builder.Append(GeneralHelper.GetCityEnglish(iPRecord.City));
                        builder.Append(" ");
                    }
                    if (!string.IsNullOrEmpty(iPRecord.Province))
                    {
                        builder.Append(GeneralHelper.GetProvinceEnglish(iPRecord.Province));
                        builder.Append(" ");
                    }
                    if (!string.IsNullOrEmpty(iPRecord.Country))
                    {
                        if (iPRecord.Country.Equals("中国"))
                        {
                            builder.Append("China");
                        }
                    }
                    str = builder.ToString();
                    break;
                case "zh-CN":
                    str = iPRecord.ToString();
                    break;
                default:
                    str = iPRecord.ToString();
                    break;
            }
            return str;
        }

        internal static string GetConnectingString(int p)
        {
            switch (Settings.Default.Language)
            {
                case "zh-CN":
                    return string.Format("即时通讯连接失败，尝试第{0}次重连", p);
                case "en-US":
                    return "Connecting...";
                default:
                    return "Connecting...";
            }
        }
    }
}

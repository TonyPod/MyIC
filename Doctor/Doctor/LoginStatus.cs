using Doctor.Model;
using DoctorClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctor
{
    class LoginStatus
    {
        public delegate void LoginStatusChangedEventHandler(EventArgs e);

        public static event LoginStatusChangedEventHandler LoginStatusChanged;

        private static void OnLoginStatusChanged(EventArgs e)
        {
            if (LoginStatusChanged != null)
            {
                LoginStatusChanged(e);
            }
        }

        public delegate void IPLocationChangedEventHandler(EventArgs e);
        public static event IPLocationChangedEventHandler IPLocationChanged;

        private static void OnIPLocationChanged(EventArgs e)
        {
            if (IPLocationChanged != null)
            {
                IPLocationChanged(e);
            }
        }

        private static IPRecord ipRecord;

        public static void RefreshIP()
        {
            //异步刷新IP
            Task.Factory.StartNew(() =>
            {
                UserIP = HttpHelper.GetIPRecord();
            });
        }

        public static IPRecord UserIP
        {
            get { return ipRecord; }
            set
            {
                ipRecord = value;
                OnIPLocationChanged(EventArgs.Empty);
            }
        }

        private static DoctorModel userInfo;

        public static DoctorModel UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
                OnLoginStatusChanged(EventArgs.Empty);
            }
        }
        public static void Clear() 
        { 
            UserInfo = null;
            OnLoginStatusChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 通过IP地址得到的地址寻找对应的省市县编码（如双流->510122）
        /// </summary>
        /// <returns></returns>
        public static string GetLocalId()
        {
            if (null == UserIP)
            {
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(UserIP.Country))
	            {
		            return "000000";
	            }
                else if (string.IsNullOrEmpty(UserIP.Province))
                {
                    //国内 000000
                    return "000000";
                }
                else if (string.IsNullOrEmpty(UserIP.City))
                {
                    //省内
                    var result = from province in GeneralHelper.Provinces
                                 where province.Province.Contains(UserIP.Province)
                                 select province;
                    return result.ElementAt(0).ProvinceID;
                }
                else if (string.IsNullOrEmpty(UserIP.District))
                {
                    //市内
                    var result = from city in GeneralHelper.Cities
                                 where city.City.Contains(UserIP.City)
                                 select city;
                    return result.ElementAt(0).CityID;
                }
                else
                {
                    //县区内（IP地址查询应该精确不到）
                    var result = from area in GeneralHelper.Areas
                                 where area.Area.Contains(UserIP.District)
                                 select area;
                    return result.ElementAt(0).AreaID;
                }
            }
        }

        internal static void SaveLoginStatus(DoctorModel doctorModel)
        {
            UserInfo = doctorModel;

            //加载联系人
            MyIMClient.LoadContacts();

            //加载本地消息
            MyIMClient.LoadMsgs();
        }
    }
}

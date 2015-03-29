using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.DAL.DAL
{
    public class LocationDAL
    {
        public static string GetCityId(string locStr)
        {
            string id = GetLocalId(locStr);
            return id.Substring(0, 4) + "00";
        }

        /// <summary>
        /// 通过“四川省_成都市_青羊区”这样的字符串返回"5101??"的编码
        /// </summary>
        /// <param name="locStr"></param>
        /// <returns></returns>
        public static string GetLocalId(string locStr)
        {
            string result = "000000";
            string[] loc = locStr.Split('_');

            string province = null;
            string city = null;
            string area = null;

            if (loc.Length >= 1)
            {
                province = loc[0];
            }

            if (loc.Length >= 2)
            {
                city = loc[1];
            }

            if (loc.Length >= 3)
            {
                area = loc[2];
            }

            if (!string.IsNullOrEmpty(province))
            {
                //省内
                var provinceModel = Hat_provinceDAL.GetByName(province);
                if (provinceModel != null)
                {
                    result = provinceModel.ProvinceID;
                }
            }
            if (!string.IsNullOrEmpty(city))
            {
                //市内
                var cityModel = Hat_cityDAL.GetByName(city);
                if (cityModel != null)
                {
                    result = cityModel.CityID;
                }
            }
            if (!string.IsNullOrEmpty(area))
            {
                //县区内（IP地址查询应该精确不到）
                var areaModel = Hat_areaDAL.GetByName(area);
                if (areaModel != null)
                {
                    result = areaModel.AreaID;
                }
            }
            return result;
        }
    }
}

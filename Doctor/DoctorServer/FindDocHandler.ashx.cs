using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DoctorServer
{
    /// <summary>
    /// FindDocHandler 的摘要说明
    /// </summary>
    public class FindDocHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //收到经纬度：返回医院列表（同城最好，离我最近）
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            //1.从JSON中解析经纬度

            //2.分析经纬度并得到“四川_成都”字符串，查找数据库找到优先的医院列表（同城最好）

            //3.确定一个经纬度范围（圆圈），查找在这个返回的医院

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
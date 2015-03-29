using Doctor.DAL;
using Doctor.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DoctorServer
{
    /// <summary>
    /// PatientInfoHandler 的摘要说明
    /// 通过病人的id获取病人的信息
    /// </summary>
    public class PatientInfoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            //使用long型主键或用string型的username解析
            UserModel patient = null;
            JObject jObj = JObject.Parse(requestStr);
            if (jObj["username"] != null)
            {
                patient = UserDAL.GetByUsername(jObj["username"].ToString());
            }
            else
            {
                patient = UserDAL.GetById(long.Parse(jObj["user_id"].ToString()));
            }

            byte[] buf = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(patient));
            context.Response.OutputStream.Write(buf, 0, buf.Length);
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
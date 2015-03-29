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
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string requestStr = reader.ReadToEnd();

            JObject jObj = JObject.Parse(requestStr);
            string username = (string)jObj.Property("username");
            string password = (string)jObj.Property("password");

            string state = null;
            DoctorModel doctorModel = DoctorDAL.CheckPassword(username, password, ref state);

            JObject jObjResponse = new JObject();
            jObjResponse.Add("state", state);
            if (doctorModel != null)
            {
                jObjResponse.Add("content", JsonConvert.SerializeObject(doctorModel));
            }

            byte[] buf = Encoding.UTF8.GetBytes(jObjResponse.ToString());
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
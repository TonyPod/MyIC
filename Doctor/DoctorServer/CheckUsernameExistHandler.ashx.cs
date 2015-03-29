using Doctor.DAL;
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
    /// CheckUsernameExistHandler 的摘要说明
    /// </summary>
    public class CheckUsernameExistHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            JObject jObj = JObject.Parse(requestStr);
            JObject jObjResponse = new JObject();
            if (null != jObj)
            {

                string username = (string)jObj["username"];
                if(DoctorDAL.CheckUsernameExist(username))
                {
                    jObjResponse.Add("state", "exist");
                }
                else
                {
                    jObjResponse.Add("state", "not exist");
                }
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
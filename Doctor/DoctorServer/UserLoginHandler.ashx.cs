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
    /// UserLoginHandler 的摘要说明
    /// </summary>
    public class UserLoginHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream);
            string requestStr = reader.ReadToEnd();

            JObject jObj = JObject.Parse(requestStr);
            string username = (string)jObj.Property("username");
            string password = (string)jObj.Property("password");

            string state = null;
            UserModel user = UserDAL.CheckPassword(username, password, ref state);

            JObject jObjResponse = new JObject();
            jObjResponse.Add("state", state);
            JObject jUser = new JObject();
            if (user != null)
            {
                jUser.Add("user_id", user.User_id);
                jUser.Add("name", user.Name);
                jUser.Add("date_of_birth", user.Date_of_birth);
                jUser.Add("male", user.Male);
            }
            jObjResponse.Add("content", jUser);

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
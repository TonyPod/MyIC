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
    /// UserRegisterHandler 用户注册的事件处理 
    /// </summary>
    public class UserRegisterHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            UserModel userModel = new UserModel();
            JObject jObj = JObject.Parse(requestStr);
            userModel.Name = jObj["name"].ToString();
            userModel.Password = jObj["password"].ToString();

            if (jObj["male"] == null || string.IsNullOrEmpty(jObj["male"].ToString()))
            {
                userModel.Male = null;
            }
            else
            {
                userModel.Male = bool.Parse(jObj["male"].ToString());
            }

            if (jObj["date_of_birth"] == null ||
                string.IsNullOrEmpty(jObj["date_of_birth"].ToString()))
            {
                userModel.Date_of_birth = null;
            }
            else
            {
                userModel.Date_of_birth = DateTime.Parse(jObj["date_of_birth"].ToString());
            }

            JObject jResponse = new JObject();

            //检查用户名是否存在
            if (DoctorDAL.CheckUsernameExist(userModel.Name))
            {
                jResponse.Add("state", "username exist");
            }
            else
            {
                if (UserDAL.Insert(userModel))
                {
                    jResponse.Add("state", "success");
                }
                else
                {
                    jResponse.Add("state", "failed");
                }
            }

            byte[] buf = Encoding.UTF8.GetBytes(jResponse.ToString());
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
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
    /// RegisterHandler 的摘要说明
    /// </summary>
    public class RegisterHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string jObjStr = reader.ReadToEnd();
            DoctorModel userModel = JsonConvert.DeserializeObject<DoctorModel>(jObjStr);

            JObject jObj = new JObject();
            //检查用户名是否存在
            if(DoctorDAL.CheckUsernameExist(userModel.Name))
            {
                jObj.Add("state", "username exist");
            }
            else
            {
                if(DoctorDAL.Insert(userModel))
                {
                    jObj.Add("state", "success");
                }
                else
                {
                    jObj.Add("state", "failed");
                }
            }
            byte[] buf = Encoding.UTF8.GetBytes(jObj.ToString());
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
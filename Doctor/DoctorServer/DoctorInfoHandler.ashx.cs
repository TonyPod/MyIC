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
    /// DoctorInfoHandler 的摘要说明
    /// </summary>
    public class DoctorInfoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            JObject jObj = JObject.Parse(requestStr);
            long doc_id;
            JObject jResponse = new JObject();
            if (long.TryParse(jObj["doc_id"].ToString(), out doc_id))
            {
                var doctor = DoctorDAL.GetById(doc_id);
                jResponse.Add("id", doctor.Name);
                jResponse.Add("name", doctor.RealName);
                jResponse.Add("introduction", doctor.Introduction);
                jResponse.Add("hospital", HospitalDAL.GetById((long)doctor.Hospital_id).Name);
                jResponse.Add("photo", doctor.PhotoPath);
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
using Doctor.DAL;
using Doctor.DAL.DAL;
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
    /// HospitalInfoHandler 的摘要说明
    /// </summary>
    public class HospitalInfoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //发过来医院的主键，返回医院的详细信息
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            long hospital_id;
            string json = null;
            if (!long.TryParse(requestStr, out hospital_id))
            {
                //这是移动端的
                JObject jObj = JObject.Parse(requestStr);
                string hospitalName = jObj["name"].ToString();
                string locStr = jObj["citycode"].ToString();
                if (HospitalDAL.Find(hospitalName, LocationDAL.GetCityId(locStr), out hospital_id))
                {
                    HospitalModel hospital = HospitalDAL.GetById(hospital_id);
                    JObject jResponse = new JObject();
                    jResponse.Add("name", hospital.Name);
                    jResponse.Add("address", hospital.Address);
                    jResponse.Add("introduction", hospital.Introduction);
                    json = jResponse.ToString();
                }
                else
                {
                    return;
                }
            }
            else
            {
                //这是医生端的
                HospitalModel hospital = HospitalDAL.GetById(hospital_id);
                json = JsonConvert.SerializeObject(hospital);
            }

            byte[] buf = Encoding.UTF8.GetBytes(json);
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
using Doctor.DAL;
using Doctor.Model;
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
    /// HospitalListHandler 的摘要说明
    /// </summary>
    public class HospitalListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //发送过来地区信息 hat_area的主键id(int)
            //返回本地区的所有医院
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            int id;
            List<HospitalModel> hospitals = new List<HospitalModel>();
            if(int.TryParse(requestStr, out id))
            {
                hospitals.AddRange(HospitalDAL.GetAllByAreaId(id));
            }

            //组装医院集合的JSON数组（只有医院id和医院名称）
            JObject jObj = new JObject();
            jObj.Add("count", hospitals.Count);
            JArray jArr = new JArray();
            foreach (var hospital in hospitals)
            {
                JObject jObjHospital = new JObject();
                jObjHospital.Add("hospital_id", hospital.Hospital_id);
                jObjHospital.Add("name", hospital.Name);
                jArr.Add(jObjHospital);
            }
            jObj.Add("content", jArr);

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
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
    /// DoctorListHandler 的摘要说明
    /// </summary>
    public class DoctorListHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //通过医院的名称获得医生列表
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            //接收到的医院列表
            JObject jObj = JObject.Parse(requestStr);
            string hospitalName = jObj["name"].ToString();
            string locStr = jObj["citycode"].ToString();

            long hospital_id;
            JObject jObjResult = new JObject();
            JArray jArr = new JArray();
            //先获得医院的主键id
            if (HospitalDAL.Find(hospitalName, LocationDAL.GetCityId(locStr), out hospital_id))
            {
                //获得医生列表
                DoctorModel[] doctors = DoctorDAL.GetByHospitalId(hospital_id);
                foreach (var doctor in doctors)
                {
                    JObject jObjDoc = new JObject();
                    jObjDoc.Add("name", doctor.RealName);
                    jObjDoc.Add("id", doctor.Name);
                    jObjDoc.Add("hospital", hospitalName);
                    jArr.Add(jObjDoc);
                }
            }

            //int count = int.Parse(jObj["count"].ToString());
            //JArray jArrHospitals = JArray.Parse(jObj["content"].ToString());

            //JObject jObjResult = new JObject();
            //JArray jArr = new JArray();
            //for (int i = 0; i < count; i++)
            //{
            //    string hospitalName = jArrHospitals[i]["name"].ToString();

            //    //这里传过来的locStr是类似于“四川省_成都市_青羊区”的字符串
            //    string locStr = jArrHospitals[i]["citycode"].ToString();

            //    long hospital_id;

            //    //先获得医院的主键id
            //    if (HospitalDAL.Find(hospitalName, LocationDAL.GetCityId(locStr), out hospital_id))
            //    {
            //        //获得医生列表
            //        DoctorModel[] doctors = DoctorDAL.GetByHospitalId(hospital_id);
            //        foreach (var doctor in doctors)
            //        {
            //            JObject jObjDoc = new JObject();
            //            jObjDoc.Add("name", doctor.RealName);
            //            jObjDoc.Add("id", doctor.Name);
            //            jObjDoc.Add("hospital", hospitalName);
            //            jArr.Add(jObjDoc);
            //        }
            //    }
            //}

            jObjResult.Add("count", jArr.Count);
            jObjResult.Add("content", jArr);

            byte[] buf = Encoding.UTF8.GetBytes(jObjResult.ToString());
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
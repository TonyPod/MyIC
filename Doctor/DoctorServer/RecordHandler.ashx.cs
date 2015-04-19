using Doctor.DAL;
using Doctor.DAL.DAL;
using Doctor.Model;
using Doctor.Model.Model;
using Doctor.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DoctorServer
{
    /// <summary>
    /// RecordHandler 的摘要说明
    /// </summary>
    public class RecordHandler : IHttpHandler
    {
        //JSON数据格式
        //{
        //    Username: string
        //    Description：string
        //    Time：DateTime
        //    LocationStr string（四川_成都_郫县）
        //    Answers：string（"010100")
        //    PicNames:string的JArray
        //}
        public void ProcessRequest(HttpContext context)
        {
            StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string requestStr = reader.ReadToEnd();

            JObject jObj = JObject.Parse(requestStr);
            string username = jObj["Username"].ToString();
            string description = jObj["Description"].ToString();
            DateTime time = DateTime.Parse(jObj["Time"].ToString());
            string locationStr = jObj["LocationStr"].ToString();
            string answers = jObj["Answers"].ToString();
            JArray picNames = JArray.Parse(jObj["PicNames"].ToString());

            //添加到自检记录
            RecordModel record = new RecordModel();
            record.Answers = answers;
            record.Description = description;

            //如果locationStr为null，则返回"000000"（中国），否则解析并保存
            if (string.IsNullOrEmpty(locationStr))
            {
                record.Citycode = "000000";
            }
            else
            {
                record.Citycode = LocationDAL.GetLocalId(locationStr);
            }

            record.Time = time;
            record.User_id = UserDAL.GetByUsername(username).User_id;
            long record_id = RecordDAL.Insert(record);
            
            //添加自检图片
            foreach (string picName in picNames)
            {
                PhotoModel photo = new PhotoModel();
                photo.Path = picName;
                photo.Record_id = record_id;
                PhotoDAL.Insert(photo);
            }

            //返回Record_id给移动端
            JObject jObjSend = new JObject();
            jObjSend.Add("Record_id", record_id);

            byte[] buf = Encoding.UTF8.GetBytes(jObjSend.ToString());
            context.Response.OutputStream.Write(buf, 0, buf.Length);

            Thread thread = new Thread(() =>
            {
                List<CVResultModel> results = new List<CVResultModel>();

                //检查图片是否分析完成(是否Insert到CVResult中)
                const int WAIT_TIME = 240;   //等待时间(如果240秒某张图片都没有传输成功且分析完成，则放弃)
                foreach (string picName in picNames)
                {
                    //轮询
                    for (int i = 0; i < WAIT_TIME; i++)
                    {
                        if (null != CVResultDAL.GetById(picName))
                        {
                            results.Add(CVResultDAL.GetById(picName));
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }

                //计算分数并保存
                float score = ScoreUtil.GetScore(results);
                RecordDAL.UpdateScore(score, record_id);
            });
            thread.Start();
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
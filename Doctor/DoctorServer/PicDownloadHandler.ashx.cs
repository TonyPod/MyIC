using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DoctorServer
{
    /// <summary>
    /// PicDownloadHandler 的摘要说明
    /// </summary>
    public class PicDownloadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = null;
            switch (context.Request.HttpMethod)
            {
                case "POST":
                    StreamReader reader = new StreamReader(context.Request.InputStream);
                    fileName = reader.ReadToEnd();
                    break;
                case "GET":
                    fileName = context.Request.Params["fileName"];
                    break;
                default:
                    break;
            }

            context.Response.WriteFile(Path.Combine(context.Server.MapPath("~/UploadFiles/"), fileName));
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
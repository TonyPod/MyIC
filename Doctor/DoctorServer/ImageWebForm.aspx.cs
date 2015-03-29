using Doctor.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorServer
{
    public partial class ImageWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileType = Request.Params["fileType"];
            string fileName = Request.Params["picName"];
            if (!string.IsNullOrEmpty(fileName))
            {
                //通过fileType判断在何处取图片
                string folder = null;
                switch (fileType)
                {
                    case "doctor":
                        folder = Server.MapPath("~/UploadFiles/");
                        break;
                    case "src":
                        folder = ConfigurationManager.AppSettings["SrcPhotoFolder"];
                        break;
                    case "dst":
                        folder = ConfigurationManager.AppSettings["DstPhotoFolder"];
                        break;
                    default:
                        break;
                }

                string filePath = Path.Combine(folder, fileName);
                //如果本地没有缩略图，则生成缩略图再传输

                if (File.Exists(filePath))
                {
                    string folderPath = Path.Combine(folder, "Thumbnails\\");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string thumbnailPath = Path.Combine(folderPath, fileName);
                    if (!File.Exists(thumbnailPath))
                    {
                        ImageUtil.GetThumbnail(filePath, thumbnailPath, 114, 150);
                    }
                    Response.WriteFile(thumbnailPath);
                }
            }
        }
    }
}
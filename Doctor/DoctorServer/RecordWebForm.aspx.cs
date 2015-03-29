using Doctor.DAL;
using Doctor.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorServer
{
    public partial class RecordWebForm : System.Web.UI.Page
    {
        /// <summary>
        /// 窗口加载时：通过GET请求的"record_id"参数到数据库取得相应的自检信息并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            long record_id;
            if (long.TryParse(Request.Params["record_id"], out record_id))
            {
                //调用数据库访问类查询该主键对应的自检记录
                RecordModel record = RecordDAL.GetById(record_id);
                PhotoModel[] photos = PhotoDAL.GetAllByRecordId(record_id);
                
                //原图片
                List<string> srcUrls = new List<string>();
                List<string> dstUrls = new List<string>();
                foreach (var photo in photos)
                {
                    srcUrls.Add("~/ImageWebForm.aspx?fileType=src&picName=" + photo.Path);
                    dstUrls.Add("~/ImageWebForm.aspx?fileType=dst&picName=" + photo.Path);
                }

                //将图片绑定到控件上
                dl_srcImgs.DataSource = srcUrls;
                dl_srcImgs.DataBind();

                dl_dstImgs.DataSource = dstUrls;
                dl_dstImgs.DataBind();

                //医生意见
                DiagnosisModel[] diagnoses = DiagnosisDAL.GetAllByRecordId(record_id);
                List<string> comments = new List<string>();
                foreach (var diagnosis in diagnoses)
                {
                    string item = string.Format("{0} {1}", diagnosis.Time.ToString(), DoctorDAL.GetById(diagnosis.Doc_id).RealName)
                        + Environment.NewLine + diagnosis.Result;
                    comments.Add(item);
                }
                dl_comments.DataSource = comments;
                dl_comments.DataBind();
            }
            else
            {
                //record_id出错
            }
        }
    }
}
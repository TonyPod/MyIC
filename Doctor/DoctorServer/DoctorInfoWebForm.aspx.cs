using Doctor.DAL;
using Doctor.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoctorServer
{
    public partial class DoctorInfoWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.Params["name"];

            if (!string.IsNullOrEmpty(name))
            {
                DoctorModel doctor = DoctorDAL.GetByName(name);
                if (doctor != null)
                {
                    lbl_username.Text = doctor.Name;
                    lbl_realname.Text = doctor.RealName;

                    HospitalModel hospital = HospitalDAL.GetById((long)doctor.Hospital_id);
                    if (hospital != null)
                    {
                        lbl_hospital.Text = hospital.Name;
                        lbl_hospitalPos.Text = hospital.Address;
                    }

                    string imageUrl = string.Format("~/ImageWebForm.aspx?picName={0}&fileType=doctor", doctor.PhotoPath);
                    graphPlaceHolder.Controls.Add(new Image() { ImageUrl = imageUrl, Width = 114, Height = 150 });
                }
            }
        }
    }
}
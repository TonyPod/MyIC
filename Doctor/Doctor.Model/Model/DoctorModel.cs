using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    public class DoctorModel
    {
        public System.Int64 Doc_id { get; set; }
        public System.Int64? Hospital_id { get; set; }
        public System.String Password { get; set; }
        public System.String Name { get; set; }
        public System.String PhotoPath { get; set; }
        public System.String LicenseNo { get; set; }
        public System.String LicensePath { get; set; }
        public System.Boolean IfAuth { get; set; }
        public System.String RealName { get; set; }
        public System.String Introduction { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    public class HospitalModel
    {
        public System.Int64 Hospital_id { get; set; }
        public System.String Name { get; set; }
        public System.String Address { get; set; }
        public System.Double Latitude { get; set; }
        public System.Double Longitude { get; set; }
        public System.Int32 Area_id { get; set; }
        public System.String Introduction { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    public class RecordModel
    {
        public System.Int64 Record_id { get; set; }
        public System.Int64 User_id { get; set; }
        public System.String Answers { get; set; }
        public System.String Description { get; set; }
        public System.DateTime Time { get; set; }
        public System.String Citycode { get; set; }
    }

}

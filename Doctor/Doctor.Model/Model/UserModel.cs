using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    public class UserModel
    {
        public System.Int64 User_id { get; set; }
        public System.String Password { get; set; }
        public System.String Name { get; set; }
        public System.DateTime? Date_of_birth { get; set; }
        public System.Boolean? Male { get; set; }
    }

}

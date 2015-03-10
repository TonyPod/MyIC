using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor
{
    class ContactsEventArgs : EventArgs
    {
        public enum EnumReason
        {
            Added = 0,
            Removed = 1
        }
        public string NewContact { get; set; }
        public string RemovedContact { get; set; }
        public EnumReason Reason { get; set; }
        public string Group { get; set; }
    }
}

using Doctor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor
{
    class OfflineMsgEventArgs : EventArgs
    {
        public enum EnumReason
        {
            MsgAdded = 0,
            MsgRead = 1
        }
        public List<Msg> NewMsgs { get; set; }
        public EnumReason Reason { get; set; }
        public string User { get; set; }
    }
}

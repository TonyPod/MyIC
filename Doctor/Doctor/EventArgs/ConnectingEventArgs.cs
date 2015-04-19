using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor
{
    class ConnectingEventArgs : EventArgs
    {
        //当前是第几次连接（从1开始）
        public int CurTime { get; set; }
    }
}

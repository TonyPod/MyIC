using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor
{
    class ConnectionSuspendedEventArgs : EventArgs
    {
        
        public ConnectionSuspendedEventArgs(bool isPushedAside)
        {
            IsPushedAside = isPushedAside;
        }

        /// <summary>
        /// 是否被挤掉
        /// </summary>
        public bool IsPushedAside { get; set; }
    }
}

using SDCIPCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceControl.Messages
{
    public class DeviceOnMessage : MessageBase
    {
        public string DeviceName { get; set; }

    }
}

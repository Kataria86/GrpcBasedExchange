using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceControl.Messages
{
    public class DevicesStatusMessage: DeviceControlMessage
    {
        public DeviceStatus Status { get; set; }
    }

    public enum DeviceStatus
    {
        On,
        Off,
        Unknown,
    }
}

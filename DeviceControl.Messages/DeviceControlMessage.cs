using SDCIPCCore;

namespace DeviceControl.Messages
{
    public class DeviceControlMessage : MessageBase
    {
        public DeviceControlMessage()
        {
            DeviceName=string.Empty;
        }

        public string DeviceName { get; set; }

    }
}
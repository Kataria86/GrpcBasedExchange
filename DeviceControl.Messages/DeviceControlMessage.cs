using SDCIPCCore;

namespace DeviceControl.Messages
{
    public class DeviceControlMessage : Message
    {
        public DeviceControlMessage()
        {
            DeviceName=string.Empty;
        }

        public string DeviceName { get; set; }

    }
}
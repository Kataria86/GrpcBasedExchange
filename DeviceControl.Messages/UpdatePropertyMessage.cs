using SDCIPCCore;

namespace DeviceControl.Messages
{
    public class UpdatePropertyMessage : DeviceControlMessage
    {
        public UpdatePropertyMessage()
        {
        }

        public string PropertyName { get; set; } = string.Empty;
        public string PropertyValue { get; set; } = string.Empty;
    }
}

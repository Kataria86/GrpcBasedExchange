using SDCIPCCore;

namespace DeviceControl.Messages
{
    public class UpdatePropertyMessage : IMessage
    {
        public string MessageId => "DC_UpdatePropertyMessage";
    }
}

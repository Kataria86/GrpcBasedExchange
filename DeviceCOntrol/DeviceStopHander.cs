using DeviceControl.Messages;
using SDCIPCCore;

namespace DeviceCOntrol
{
    internal class DeviceStopHander : MessageHandlerBase<DeviceOffMessage>
    {
        public event EventHandler<dynamic> Processed;

        public override bool Handle(string messageId, string messagePayload)
        {
            DeviceOffMessage result = Parse(messagePayload);

            if (result != null && Processed != null)
            {
                Processed.Invoke(this, new { JSON = messagePayload, ConnreteObject = result });
            }

            return true;
        }
    }
}
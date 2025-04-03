using DeviceControl.Messages;
using SDCIPCCore;

namespace DeviceCOntrol
{
    public class DeviceStartHander : MessageHandlerBase<DeviceOnMessage>
    {
        public event EventHandler<dynamic> Processed;

        public override bool Handle(string messageId, string messagePayload)
        {
            Thread.Sleep(5000);
            DeviceOnMessage result = Parse(messagePayload);

            if (result!=null && Processed != null) 
            {
                Processed.Invoke(this, new { JSON = messagePayload, ConnreteObject = result });
            }
            
            return true;
        }
    }
}
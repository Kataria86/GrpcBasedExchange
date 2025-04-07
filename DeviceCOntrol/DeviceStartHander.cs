using DeviceControl.Messages;
using SDCIPCCore;

namespace DeviceCOntrol
{
    public class DeviceStartHander : MessageHandlerBase<DeviceOnMessage>
    {
        public event EventHandler<dynamic> Processed;

        public override object Handle(string messageId, string messagePayload)
        {
            DeviceOnMessage result = Parse(messagePayload);

            if (result!=null && Processed != null) 
            {
                Processed.Invoke(this, new { JSON = messagePayload, ConnreteObject = result });

                MainWindow.messageService.BroadcastMessage(new DevicesStatusMessage { DeviceName = result.DeviceName, Status = DeviceStatus.On });
            }
            
            return true;
        }
    }
}
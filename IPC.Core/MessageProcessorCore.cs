using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public abstract class MessageProcessorBase
    {
        public abstract IList<IMessageHandler> MessageHandlers { get; set; }
        public bool Process(MessageContainer messageContainer)
        {
            bool result = false;
            foreach (var messageHandler in MessageHandlers)
            {
                if (messageContainer != null &&  messageHandler.CanHandle(messageContainer.MessageId))
                {
                    result = messageHandler.Handle(messageContainer.MessageBody);
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public class MessageProcessorCore
    {
        private readonly IList<IMessageHandler> messageHandlers;

        public MessageProcessorCore(IList<IMessageHandler> messageHandlers)
        {
            this.messageHandlers = messageHandlers;

        }

        public bool Process(MessageContainer messageContainer)
        {
            bool result = false;
            foreach (var messageHandler in messageHandlers)
            {
                if (messageContainer != null && messageHandler.CanHandle(messageContainer.MessageId))
                {
                    result = messageHandler.Handle(messageContainer.MessageId, messageContainer.MessagePayload);
                }
            }

            return result;
        }
    }
}

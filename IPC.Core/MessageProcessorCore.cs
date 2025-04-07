using Newtonsoft.Json;
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

        public Result Process(string messageId, string messagePayload)
        {
            Result result = null;
            foreach (var messageHandler in messageHandlers)
            {
                if (messagePayload != null && messageHandler.CanHandle(messageId))
                {
                    return new Result
                    {
                        Data =messageHandler.Handle(messageId, messagePayload),
                        Success = true

                    };
                }
            }

            return result;
        }
    }

    public class Result
    {
        public bool Success { get; set; }
        public Object Data { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public static class MessageBuilder
    {
        public static MessageContainer Build(MessageBase messageBase)
        {
            return new MessageContainer
            {
                MessageId = messageBase.MessageId,
                MessagePayload = JsonConvert.SerializeObject(messageBase)
            };
        }
    }
}

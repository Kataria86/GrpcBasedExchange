using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public static class MessageBuilder
    {
        public static MessageContainer Build(IMessage message, IList<string> receivers)
        {
            return new MessageContainer
            {
                MessageId = message.MessageId,
                Receivers = receivers,
                MessagePayload = JsonConvert.SerializeObject(message)
            };
        }
    }
}

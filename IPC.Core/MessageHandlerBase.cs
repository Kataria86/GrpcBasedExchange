
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public abstract class MessageHandlerBase<T> : IMessageHandler
    {
        public bool CanHandle(string messageId)
        {
            return string.Equals(messageId, GetMessageId(), StringComparison.OrdinalIgnoreCase);
        }

        public abstract bool Handle(string messageContainer);

        protected virtual T Parse(string msg, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                throw new ArgumentException("Message content cannot be null or empty.", nameof(msg));
            }

            return settings == null
                ? JsonConvert.DeserializeObject<T>(msg)
                : JsonConvert.DeserializeObject<T>(msg, settings);
        }

        protected abstract string GetMessageId();
    }
}

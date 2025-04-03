
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public abstract class MessageHandlerBase<T> : IMessageHandler where T : Message
    {
        public bool CanHandle(string messageId)
        {
            var id = typeof(T).ToString();
            return string.Equals(messageId, id, StringComparison.OrdinalIgnoreCase);
        }

        public abstract bool Handle(string messageId, string messagePayload);

   

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
    }
}

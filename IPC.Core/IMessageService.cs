using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public interface IMessageService
    {
        string SenderId { get; set; }
        Task<bool> BroadcastMessage(MessageBase message);
        Task<Response> SendMessage(string receiverId, MessageBase message);
        Task<T?> SendMessage<T>(string receiverId, MessageBase message);

        Task<bool> SendResponse(string transactionId, bool result, string data);
        Task RegisterClient(string clientId, MessageProcessorCore messageProcessorCore);
    }

    public class Response
    {
        public Response(bool success, string reply)
        {
            Success = success;
            Reply = reply;
        }

        public string Reply { get; set; }
        public bool Success { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public interface IMessageService
    {
        bool SendMessage(MessageContainer message);
        Task<bool> SendMessageWithWait(MessageContainer message);


        Task RegisterClient(string clientId, MessageProcessorCore messageProcessorCore);
    }
}

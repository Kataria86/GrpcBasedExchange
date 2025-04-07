using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public interface IMessageHandler
    {
        bool CanHandle(string messageId); // Checks if the message can be handled
        object Handle(string messageId, string messagePayload); // Handles the message and returns TResult


    }

}

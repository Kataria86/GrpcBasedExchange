using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public interface IMessageHandler
    {
        bool CanHandle(string messageId);
        bool Handle(string messageContainer);
    }
}

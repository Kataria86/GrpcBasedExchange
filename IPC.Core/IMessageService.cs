using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public interface IMessageService
    {
        public bool SendMessage(string message);

        public bool RegisterClient(string clientId, MessageProcessorCore messageProcessorCore);
    }
}

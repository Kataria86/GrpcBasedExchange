using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public class MessageReceivedArgs : EventArgs
    {
        public MessageContainer Message;

        public MessageReceivedArgs(MessageContainer message)
        {
            this.Message = message;
        }
    }
}

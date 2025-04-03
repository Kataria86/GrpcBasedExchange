using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public class Message
    {

        public string MessageId { get { return this.GetType().ToString(); } }

    }

}

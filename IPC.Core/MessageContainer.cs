using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public class MessageContainer
    {
        public string TransactionId {  get; set; }        
        public string MessageId {  get; set; }
        public string Sender { get; set; }
        public bool WaitingForResponse { get; set; }
        public IList<string> Receivers { get; set; }
        public string MessagePayload { get; set; }
    }
}

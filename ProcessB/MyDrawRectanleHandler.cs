using GrpcBasedExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessB
{
    class MyDrawRectanleHandler : IProcessor
    {
        public string HandleMessage(string sender, string message)
        {
            return "Ankit";
        }
    }
}

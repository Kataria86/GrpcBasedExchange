﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDCIPCCore
{
    public class MessageBase
    {

        public string MessageId { get { return this.GetType().ToString(); } }

    }

}

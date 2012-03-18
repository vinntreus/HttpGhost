using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Firefly.Utils;

namespace IntegrationTests
{
    public class StdoutTrace : IServerTrace
    {
        public void Event(TraceEventType type, TraceMessage message)
        {
            Console.WriteLine("[{0} {1}]", type, message);
        }
    }
}
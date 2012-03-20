using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Hosting.Self;

namespace IntegrationTests.Nancy
{
    public class Host
    {
        private readonly NancyHost host;

        public Host(string path = "http://127.0.0.1:8080")
        {
            host = new NancyHost(new Uri(path));
        }

        public void Start()
        {
            host.Start();
        }

        public void Stop()
        {
            host.Stop();
        }
    }
}

using System;
using IntegrationTests.Nancy.Modules;
using Nancy.Hosting.Self;

namespace IntegrationTests.Nancy
{
    public class Host
    {
        private readonly NancyHost host;

        public Host(string path = "http://127.0.0.1:1234")
        {
            host = new NancyHost(new AuthenticationBootstrapper(), new Uri(path));
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
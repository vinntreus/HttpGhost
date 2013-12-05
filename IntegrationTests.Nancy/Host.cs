using System;
using Nancy.Hosting.Self;

namespace IntegrationTests.Nancy
{
    public class Host
    {
        private readonly NancyHost host;

        public Host(string path = "http://localhost:1234")
        {
            var configuration = new HostConfiguration { RewriteLocalhost = false };
            host = new NancyHost(new Uri(path), new AuthenticationBootstrapper(), configuration);
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
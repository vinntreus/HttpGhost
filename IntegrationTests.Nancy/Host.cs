using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;
using Nancy.Authentication.Basic;
using Nancy.Security;

namespace IntegrationTests.Nancy
{
    public class Host
    {
        protected NancyHost host;

        public Host(string path = "http://127.0.0.1:8080")
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

    public class BasicAuthHost : Host
    {
        public BasicAuthHost(string path) : base(path)
        {
            host = new NancyHost(new AuthenticationBootstrapper(), new Uri(path));
        }
    }

    public class AuthenticationBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoC.TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            var config = new BasicAuthenticationConfiguration(container.Resolve<IUserValidator>(), "basic");

            pipelines.EnableBasicAuthentication(config);
        }
    }

    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            return new BasicUserIdentity { UserName = username };
        }
    }
}

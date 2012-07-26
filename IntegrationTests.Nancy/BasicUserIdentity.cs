using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Security;

namespace IntegrationTests.Nancy
{
    public class BasicUserIdentity : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}

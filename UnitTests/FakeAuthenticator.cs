using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public class FakeAuthenticator: HttpGhost.Authentication.IAuthenticate
    {
        public void Process(HttpGhost.Transport.Request request)
        {
            
        }
    }
}

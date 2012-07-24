using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpGhost.Navigation;
using HttpGhost.Transport;

namespace UnitTests
{
    public class FakeNavigator : INavigate
    {
        public IRequest Request { get; private set; }        

        public INavigationResult Navigate(IRequest request)
        {
            Request = request;
            return null;
        }
    }
}

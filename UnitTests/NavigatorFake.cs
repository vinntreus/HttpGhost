using HttpGhost.Navigation;
using HttpGhost.Transport;

namespace UnitTests
{
    public class NavigatorFake : INavigate
    {
        public IRequest Request { get; private set; }        

        public INavigationResult Navigate(IRequest request)
        {
            Request = request;
            return new NavigationResult(request, new Response());
        }
    }
}

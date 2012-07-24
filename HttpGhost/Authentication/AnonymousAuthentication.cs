
using HttpGhost.Transport;

namespace HttpGhost.Authentication
{
    public class AnonymousAuthentication : IAuthenticate
    {
        public void Process(Request request)
        {
            //Should not do anything
        }
    }
}

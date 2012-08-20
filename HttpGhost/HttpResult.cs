using HttpGhost.Navigation;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class HttpResult : IHttpResult
    {
        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpent = navigationResult.TimeSpent;
            Response = navigationResult.Response;
            Request = navigationResult.Request;
        }

        public IResponse Response { get; private set; }
        public IRequest Request { get; private set; }
        public long TimeSpent { get; private set; }
    }
}
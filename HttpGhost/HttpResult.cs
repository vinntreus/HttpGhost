using HttpGhost.Navigation;
using HttpGhost.Transport;

namespace HttpGhost
{
    internal class HttpResult : IHttpResult
    {
        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpentInMs = navigationResult.TimeSpent;
            Response = navigationResult.Response;
            Request = navigationResult.Request;
        }

        public IResponse Response { get; private set; }
        public IRequest Request { get; private set; }
        public long TimeSpentInMs { get; private set; }
    }
}
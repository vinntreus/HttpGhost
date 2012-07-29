using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    internal class NavigationResult : INavigationResult
    {
        public NavigationResult(IRequest request, IResponse response)
        {
            Request = request;
            Response = response;
        }

        public long TimeSpent { get; set; }
        public IResponse Response { get; private set; }
        public IRequest Request { get; private set; }
    }
}
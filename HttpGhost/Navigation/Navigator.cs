using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public abstract class Navigator
    {
        protected readonly IRequest request;

        protected Navigator(IRequest request)
        {
            this.request = request;
        }

        public INavigationResult Navigate()
        {
            return new NavigationResult(request, request.GetResponse());
        }
    }
}
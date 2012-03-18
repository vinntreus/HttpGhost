using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public abstract class NavigationBase
    {
        protected readonly IRequest request;

        protected NavigationBase(IRequest request)
        {
            this.request = request;
        }

        public INavigationResult Navigate()
        {
            return new NavigationResult(request, request.GetResponse());
        }
    }
}
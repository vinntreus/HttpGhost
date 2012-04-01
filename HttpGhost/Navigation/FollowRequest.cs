using HttpGhost.Navigation.Methods;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public class FollowRequest
    {
        private readonly IRequest request;
        private readonly string url;

        public FollowRequest(IRequest request, string url)
        {
            this.request = request;
            this.url = new UrlByLink(url, request.Uri).Build();
        }

        protected virtual IRequest CreateRequest()
        {
            return Request.Create(url);
        }

        protected virtual GetNavigationOptions GetNavigationOptions()
        {
            return new GetNavigationOptions(request.GetAuthentication(), request.GetContentType());
        }

        public virtual INavigationResult Navigate()
        {
            return new Get(CreateRequest(), GetNavigationOptions()).Navigate();
        }

    }
}
using System;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    internal class FollowRequest
    {
        private readonly string url;

        public FollowRequest(IRequest request, string url)
        {
            this.url = new UrlByLink(url, new Uri(request.Url)).Build();
        }

        public virtual INavigationResult Navigate()
        {
            return new WebRequestNavigator().Navigate(new Request(url));
        }
    }
}
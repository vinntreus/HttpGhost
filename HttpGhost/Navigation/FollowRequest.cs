using System;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    internal class FollowRequest
    {
        private readonly IRequest request;
        private readonly string url;

        public FollowRequest(IRequest request, string url)
        {
            this.request = request;
            this.url = new UrlByLink(url, new Uri(request.Url)).Build();
        }

        public virtual INavigationResult Navigate()
        {
            var request = new Request(url) { Method = "GET" };
            var nav = new WebClientNavigator();
            return nav.Navigate(request);
        }
    }
}
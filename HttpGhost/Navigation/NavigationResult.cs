using System.Linq;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Parsing;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    internal class NavigationResult : INavigationResult
    {
        private readonly IRequest request;
        private IEnumerable<HtmlNode> nodes;
        protected readonly IResponse response;

        public NavigationResult(IRequest request, IResponse response)
        {
            this.request = request;
            this.response = response;
        }

        public long TimeSpent { get; set; }

        public HttpStatusCode Status { get { return response.StatusCode; } }

        public string ResponseContent { get { return response.Body; } }

        public WebHeaderCollection ResponseHeaders
        {
            get { return response.Headers; }
        }

        public string RequestUrl
        {
            get { return request.Url; }
        }

        public T FromJsonTo<T>()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ResponseContent);
        }

        public Elements Find(string selector)
        {
            return new Elements(GetHtmlNodes(selector));
        }
        
        private IEnumerable<HtmlNode> GetHtmlNodes(string selector)
        {
            if (nodes == null)
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(ResponseContent);
                selector = new CssSelectorParser(selector).ToXPath();
                nodes = htmlDoc.DocumentNode.SelectNodes(selector);
            }
            return nodes;
        }

        public INavigationResult Follow(string selector)
        {
            var href = Find(selector).Attribute("href");
            if (string.IsNullOrEmpty(href))
                throw new NavigationResultException("No element with href found for selector: " + selector);
            return GetFollowRequest(href).Navigate();
        }

        protected virtual FollowRequest GetFollowRequest(string href)
        {
            return new FollowRequest(request, href);
        }
    }
}
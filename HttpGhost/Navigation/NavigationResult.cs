using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using HttpGhost.Navigation.Methods;
using HttpGhost.Parsing;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public class NavigationResult : INavigationResult
    {
        private readonly IRequest request;
        protected readonly IResponse response;

        public NavigationResult(IRequest request, IResponse response)
        {
            this.request = request;
            this.response = response;
        }

        public long TimeSpent { get; set; }

        public HttpStatusCode Status { get { return response.StatusCode; } }

        public string ResponseContent { get { return response.Html; } }

        public WebHeaderCollection ResponseHeaders
        {
            get { return response.Headers; }
        }

        public IEnumerable<string> Find(string pattern)
        {
            var items = GetHtmlNodes(pattern);

            return items == null ? new List<string>() : items.Select(i => i.OuterHtml);
        }

        private IEnumerable<HtmlNode> GetHtmlNodes(string pattern)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(ResponseContent);
            pattern = new SelectorParser(pattern).ToXPath();
            return htmlDoc.DocumentNode.SelectNodes(pattern);
        }

        public T FromJsonTo<T>()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ResponseContent);
        }

        public string RequestUrl
        {
            get { return request.Url; }
        }

        public INavigationResult Follow(string selector)
        {
            var url = GetHtmlNodes(selector).First().Attributes["href"].Value;
            var actualUrl = new GetUrlBuilder(url, request.Uri).Build();
            Console.WriteLine(actualUrl);
            var webRequest = new RequestFactory(new FormSerializer()).Create(actualUrl);
            var options = new GetNavigationOptions(request.GetAuthentication(), request.GetContentType());
            return new Get(webRequest, options).Navigate();
        }
    }

    public class GetUrlBuilder
    {
        private readonly string url;
        private readonly Uri uri;

        public GetUrlBuilder(string url, Uri uri)
        {
            this.url = url;
            this.uri = uri;
        }

        public string Build()
        {
            if (url.StartsWith("http"))
                return url;

            return string.Format("{0}://{1}{2}", "http", uri.Authority,url);
        }
    }
}
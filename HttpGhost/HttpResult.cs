using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Navigation;
using HttpGhost.Parsing;

namespace HttpGhost
{
    public class HttpResult : IHttpResult
    {
        private IEnumerable<HtmlNode> nodes;

        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpent = navigationResult.TimeSpent;
            Status = navigationResult.Response.StatusCode;
            ResponseContent = navigationResult.Response.Body;
            ResponseHeaders = navigationResult.Response.Headers;
            RequestUrl = navigationResult.Request.Url;
        }

        public long TimeSpent { get; private set; }
        public HttpStatusCode Status { get; private set; }
        public string ResponseContent { get; private set; }
        public WebHeaderCollection ResponseHeaders { get; private set; }
        public string RequestUrl { get; private set; }
        public Func<string, IHttpResult> OnFollow { get; set; }

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

        public IHttpResult Follow(string selector)
        {
            var href = Find(selector).Attribute("href");
            if (string.IsNullOrEmpty(href))
                throw new NavigationResultException("No element with href found for selector: " + selector);
            var url = UrlByLink.Build(href, new Uri(RequestUrl));
            return OnFollow(url);
        }
    }
}
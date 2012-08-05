using System;
using System.Net;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Navigation;
using HttpGhost.Serialization;

namespace HttpGhost
{
    public class HttpResult : IHttpResult
    {
        private readonly JsonSerializer serializer;
        private readonly HtmlDocument htmlDoc;

        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpent = navigationResult.TimeSpent;
            Status = navigationResult.Response.StatusCode;
            ResponseContent = navigationResult.Response.Body ?? "";
            ResponseHeaders = navigationResult.Response.Headers;
            RequestUrl = navigationResult.Request.Url;
            serializer = new JsonSerializer();
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(ResponseContent);
        }

        public long TimeSpent { get; private set; }
        public HttpStatusCode Status { get; private set; }
        public string ResponseContent { get; private set; }
        public WebHeaderCollection ResponseHeaders { get; private set; }
        public string RequestUrl { get; private set; }
        public Func<string, IHttpResult> OnFollow { get; set; }
        public Func<object, string, IHttpResult> OnSubmitForm { get; set; }

        public T FromJsonTo<T>()
        {
            return serializer.Deserialize<T>(ResponseContent);
        }

        public Elements Find(string selector)
        {
            return new Elements(htmlDoc.Find(selector));
        }

        public Form FindForm(string selector)
        {
            var node = htmlDoc.FindOne(selector);
            if(node == null)
            {
                throw new NavigationResultException(FormatSelectorError("Could not find form", selector, ResponseContent));
            }
            return new Form(node, RequestUrl) { OnSubmit = OnSubmitForm };
        }

        public IHttpResult Follow(string selector)
        {
            var href = Find(selector).Attribute("href");
            if (string.IsNullOrEmpty(href))
                throw new NavigationResultException(FormatSelectorError("Could not find element with href", selector, ResponseContent));

            var url = UrlByLink.Build(href, new Uri(RequestUrl));

            return OnFollow(url);
        }

        private static string FormatSelectorError(string message, string selector, string body)
        {
            return string.Format("{0}\nSelector: '{1}'\nBody: '{2}'", message, selector, body);
        }
    }
}
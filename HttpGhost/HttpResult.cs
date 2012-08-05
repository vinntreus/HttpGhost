using System;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class HttpResult : IHttpResult
    {
        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpent = navigationResult.TimeSpent;
            Response = navigationResult.Response;
            Request = navigationResult.Request;
        }

        public IResponse Response { get; private set; }
        public IRequest Request { get; private set; }
        public long TimeSpent { get; private set; }
    }

    public class JsonResult : HttpResult
    {
        private readonly JsonSerializer serializer;

        public JsonResult(INavigationResult navigationResult) : base(navigationResult)
        {
            serializer = new JsonSerializer();
        }

        public T To<T>()
        {
            return serializer.Deserialize<T>(Response.Body);
        }
    }
    
    public class HtmlResult : HttpResult
    {
        private readonly HtmlDocument htmlDoc;

        public HtmlResult(INavigationResult navigationResult) : base(navigationResult)
        {
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(Response.Body);
        }

        public Func<string, IHttpResult> OnFollow { get; set; }
        public Func<object, string, IHttpResult> OnSubmitForm { get; set; }


        public Elements Find(string selector)
        {
            return new Elements(htmlDoc.Find(selector));
        }

        public Form FindForm(string selector)
        {
            var node = htmlDoc.FindOne(selector);
            if (node == null)
            {
                throw new NavigationResultException(FormatSelectorError("Could not find form", selector, Response.Body));
            }
            return new Form(node, Request.Url) { OnSubmit = OnSubmitForm };
        }

        public IHttpResult Follow(string selector)
        {
            var href = Find(selector).Attribute("href");
            if (string.IsNullOrEmpty(href))
                throw new NavigationResultException(FormatSelectorError("Could not find element with href", selector, Response.Body));

            var url = UrlByLink.Build(href, new Uri(Request.Url));

            return OnFollow(url);
        }

        private static string FormatSelectorError(string message, string selector, string body)
        {
            return string.Format("{0}\nSelector: '{1}'\nBody: '{2}'", message, selector, body);
        }
    }
}
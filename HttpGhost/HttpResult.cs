using System;
using System.Net;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Navigation;
using HttpGhost.Parsing;
using HttpGhost.Serialization;

namespace HttpGhost
{
    public class HttpResult : IHttpResult
    {
        private readonly JsonSerializer serializer;
        private readonly HtmlDocument htmlDoc;
        //private readonly HtmlNavigator htmlNavigator;

        public HttpResult(INavigationResult navigationResult)
        {
            TimeSpent = navigationResult.TimeSpent;
            Status = navigationResult.Response.StatusCode;
            ResponseContent = navigationResult.Response.Body ?? "";
            ResponseHeaders = navigationResult.Response.Headers;
            RequestUrl = navigationResult.Request.Url;
            serializer = new JsonSerializer();
            //htmlNavigator = new HtmlNavigator(ResponseContent);
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

        public IHttpResult Follow(string selector)
        {
            var href = Find(selector).Attribute("href");
            if (string.IsNullOrEmpty(href))
                throw new NavigationResultException(FormatSelectorError("Could not find element with href", selector, ResponseContent));

            var url = UrlByLink.Build(href, new Uri(RequestUrl));

            return OnFollow(url);
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

        private static string FormatSelectorError(string message, string selector, string body)
        {
            return string.Format("{0}\nSelector: '{1}'\nBody: '{2}'", message, selector, body);
        }
    }

    public static class HtmlNodeExtension
    {
        public static HtmlNodeCollection SelectNodesByCss(this HtmlNode node, string cssSelector)
        {
            var xpath = CssSelectorToXPath(cssSelector);
            return node.SelectNodes(xpath);
        }

        public static HtmlNode SelectSingleNodeByCss(this HtmlNode node, string cssSelector)
        {
            var xpath = CssSelectorToXPath(cssSelector);
            return node.SelectSingleNode(xpath);
        }

        public static HtmlNodeCollection Find(this HtmlDocument htmlDoc, string selector)
        {
            var xpath = CssSelectorToXPath(selector);
            return htmlDoc.DocumentNode.SelectNodes(xpath);
        }

        public static HtmlNode FindOne(this HtmlDocument htmlDoc, string selector)
        {
            var xpath = CssSelectorToXPath(selector);
            return htmlDoc.DocumentNode.SelectSingleNode(xpath);
        }

        private static string CssSelectorToXPath(string selector)
        {
            return new CssSelectorParser(selector).ToXPath();
        }
    }

    //public class HtmlNavigator
    //{
    //    private readonly HtmlDocument htmlDoc;

    //    public HtmlNavigator(string html)
    //    {
    //        htmlDoc = new HtmlDocument();
    //        htmlDoc.LoadHtml(html);
    //    }

    //    public HtmlNodeCollection Find(string selector)
    //    {
    //        var xpath = SelectorToXPath(selector);
    //        return htmlDoc.DocumentNode.SelectNodes(xpath);
    //    }

    //    public HtmlNode FindOne(string selector)
    //    {
    //        var xpath = SelectorToXPath(selector);
    //        return htmlDoc.DocumentNode.SelectSingleNode(xpath);
    //    }

    //    private static string SelectorToXPath(string selector)
    //    {
    //        return new CssSelectorParser(selector).ToXPath();
    //    }
    //}
}
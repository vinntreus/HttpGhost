using System;
using HtmlAgilityPack;
using HttpGhost.Html;
using HttpGhost.Navigation;

namespace HttpGhost
{
    public interface IHtmlResult : IHttpResult
    {
        /// <summary>
        /// Finds html-elements by using css-selector or xpath
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Html elements collection</returns>
        Elements Find(string selector);

        /// <summary>
        /// Find html form element by using css-selector or xpath
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Form element</returns>
        Form FindForm(string selector);

        /// <summary>
        /// Follow a link, aka simulate a click on a href-element, use css- or xpath-selector to find link and make get request from its href attribute
        /// </summary>
        /// <param name="selector">Css or xpath-selector</param>
        /// <returns>Httpresult</returns>
        IHttpResult Follow(string selector);
    }

    public class HtmlResult : HttpResult, IHtmlResult
    {
        private readonly HtmlDocument htmlDoc;

        public HtmlResult(INavigationResult navigationResult) : base(navigationResult)
        {
            htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(Response.Body);
        }

        internal Func<string, IHttpResult> OnFollow { get; set; }
        internal Func<object, string, IHttpResult> OnSubmitForm { get; set; }

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
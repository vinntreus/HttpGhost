using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using HttpGhost.Navigation;

namespace HttpGhost.Html
{
    public class Form : Element
    {
        private readonly string baseUrl;

        public Form(HtmlNode node, string baseUrl) : base(node)
        {
            this.baseUrl = baseUrl;
        }

        internal Func<object, string, IHttpResult> OnSubmit { get; set; }

        public void SetValue(string selector, string value)
        {
            var field = Node.SelectNodesByCss(selector);
            foreach (var node in field)
            {
                node.Attributes["value"].Value = value;
            }
        }

        public IHttpResult Submit()
        {
            var dataToSubmit = GetDataToSubmit();
            var action = GetAttribute("action");
            var url = UrlByLink.Build(action, new Uri(baseUrl));
            return OnSubmit(dataToSubmit, url);
        }

        private IDictionary<string, string> GetDataToSubmit()
        {
            var inputs = Node.SelectNodesByCss("input");
            var data = new Dictionary<string, string>();
            foreach (var input in inputs)
            {
                var name = input.GetAttributeValue("name", "");
                var value = input.GetAttributeValue("value", "");
                if (!string.IsNullOrEmpty(name))
                {
                    data.Add(name, value);
                }
            }
            return data;
        }
    }
}
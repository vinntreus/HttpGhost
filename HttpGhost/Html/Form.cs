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

        public Form SetValue(string selector, string value)
        {
            var field = Node.SelectNodesByCss(selector);
            foreach (var node in field)
            {
                node.Attributes["value"].Value = value;
            }
            return this;
        }

        /// <summary>
        /// Submits form (POST) to url build by former RequestUrl + forms action attribute
        /// </summary>
        /// <returns></returns>
        public IHttpResult Submit()
        {
            var dataToSubmit = GetFormData();
            var action = GetAttribute("action");
            return OnSubmit(dataToSubmit, action);
        }

        /// <summary>
        /// Retrieves key/values (from name/value attributes) from input-fields in form
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetFormData()
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
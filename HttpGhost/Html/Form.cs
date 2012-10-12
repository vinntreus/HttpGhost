using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using HttpGhost.CssSelector;

namespace HttpGhost.Html
{
    public class Form : Element
    {
        public Form(HtmlNode node) : base(node)
        {
            extraValues = new Dictionary<string, string>();
        }

        internal Func<object, string, IHtmlResult> OnSubmit { get; set; }

        /// <summary>
        /// Sets value on existing element which is a child of the actual form element
        /// </summary>
        /// <param name="selector">css or xpath</param>
        /// <param name="value">field value</param>
        /// <returns></returns>
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
        /// Submits form (POST) using action-attribute and all input name/values
        /// </summary>
        /// <returns></returns>
        public IHtmlResult Submit()
        {
            var dataToSubmit = GetFormData();
            foreach (var extraValue in extraValues)
            {
                if(!dataToSubmit.ContainsKey(extraValue.Key))
                {
                    dataToSubmit.Add(extraValue);
                }
            }
            var action = GetAttribute("action");
            return OnSubmit(dataToSubmit, action);
        }

        private readonly IDictionary<string, string> extraValues; 

        /// <summary>
        /// Adds name/value to collection of items to be submited. Item does not need to be in inital form.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Form InsertValue(string name, string value)
        {
            if(!extraValues.ContainsKey(name))
                extraValues.Add(name, value);
            return this;
        }

        /// <summary>
        /// Retrieves key/values (from name/value attributes) from input-fields in form
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> GetFormData()
        {
            var inputs = Node.SelectNodesByCss("input");
            var data = new Dictionary<string, string>();

            if (inputs != null)
            {
                foreach (var input in inputs)
                {
                    var name = input.GetAttributeValue("name", "");
                    var value = input.GetAttributeValue("value", "");
                    if (!string.IsNullOrEmpty(name))
                    {
                        data.Add(name, value);
                    }
                }
            }

            return data;
        }
    }
}
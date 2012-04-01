using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HttpGhost.Html
{
    public class Elements : IEnumerable<Element>
    {
        private readonly IEnumerable<Element> elements;

        public Elements(IEnumerable<HtmlNode> nodes)
        {
            nodes = nodes ?? new List<HtmlNode>();
            elements = nodes.Select(n => new Element(n));
        }

        public string Attribute(string attribute)
        {
            return !elements.Any() ? "" : elements.First().GetAttribute(attribute);
        }

        public string Text { get { return !elements.Any() ? "" : elements.First().Text; } }
        public string Raw { get { return !elements.Any() ? "" : elements.First().Raw; } }

        public IEnumerator<Element> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
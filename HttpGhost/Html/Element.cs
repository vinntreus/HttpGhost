using HtmlAgilityPack;

namespace HttpGhost.Html
{
    public class Element
    {
        private readonly HtmlNode node;

        public Element(HtmlNode node)
        {
            this.node = node;
        }

        public string GetAttribute(string attribute)
        {
            return node.Attributes[attribute] != null ? node.Attributes[attribute].Value : "";
        }

        public string Raw
        {
            get { return node.OuterHtml; }
        }

        public string Text
        {
            get { return node.InnerText; }
        }
       
        public override string ToString()
        {
            return node.OuterHtml;
        }
    }
}
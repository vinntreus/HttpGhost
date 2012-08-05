using HtmlAgilityPack;

namespace HttpGhost.Html
{
    public class Element
    {
        protected readonly HtmlNode Node;

        public Element(HtmlNode node)
        {
            Node = node;
        }

        public string GetAttribute(string attribute)
        {
            return Node.Attributes[attribute] != null ? Node.Attributes[attribute].Value : "";
        }

        public string Raw
        {
            get { return Node.OuterHtml; }
        }

        public string Text
        {
            get { return Node.InnerText; }
        }
       
        public override string ToString()
        {
            return Node.OuterHtml;
        }
    }
}
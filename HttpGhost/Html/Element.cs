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
            return Node.GetAttributeValue(attribute, "");
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
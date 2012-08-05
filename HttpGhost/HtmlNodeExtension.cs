using HtmlAgilityPack;
using HttpGhost.Parsing;

namespace HttpGhost
{
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
}
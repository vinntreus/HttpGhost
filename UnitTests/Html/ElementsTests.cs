using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using HttpGhost.Html;
using NUnit.Framework;

namespace UnitTests.Html
{
    [TestFixture]
    public class ElementsTests
    {
        [Test]
        public void Ctor_Null_EmptyList()
        {
            var elements = new Elements(null);

            Assert.That(elements.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Ctor_OneNode_ListWithOneElement()
        {
            var elements = new Elements(new List<HtmlNode> {new HtmlNode(HtmlNodeType.Element, new HtmlDocument(), 0)});

            Assert.That(elements.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Attribute_WithoutElements_ReturnStringEmpty()
        {
            var elements = new Elements(null);

            Assert.That(elements.Attribute("href"), Is.EqualTo(string.Empty));
        }

        [Test]
        public void Attribute_WithMatch_ReturnsAttributeValue()
        {
            var nodes = GetNodes("<a href='someurl'>text</a>");
            
            var elements = new Elements(nodes);

            Assert.That(elements.Attribute("href"), Is.EqualTo("someurl"));
        }

        [Test]
        public void Attribute_MultipleNodes_ReturnsFirstFoundAttributeValue()
        {
            var nodes = GetNodes("<a href='url'>text</a><a href='otherurl'>text</a>");

            var elements = new Elements(nodes);

            Assert.That(elements.Attribute("href"), Is.EqualTo("url"));
        }

        [Test]
        public void Text_WithMatch_ReturnsValue()
        {
            var nodes = GetNodes("<a href='someurl'>text</a>");

            var elements = new Elements(nodes);

            Assert.That(elements.Text, Is.EqualTo("text"));
        }

        [Test]
        public void Text_MultipleNodes_ReturnsFirstFoundValue()
        {
            var nodes = GetNodes("<a href='url'>text</a><a href='otherurl'>other</a>");

            var elements = new Elements(nodes);

            Assert.That(elements.Text, Is.EqualTo("text"));
        }

        [Test]
        public void Raw_WithMatch_ReturnsValue()
        {
            var nodes = GetNodes("<a href='someurl'>text</a>");

            var elements = new Elements(nodes);

            Assert.That(elements.Raw, Is.EqualTo("<a href='someurl'>text</a>"));
        }

        [Test]
        public void Raw_MultipleNodes_ReturnsFirstFoundValue()
        {
            var nodes = GetNodes("<a href='url'>text</a><a href='otherurl'>other</a>");

            var elements = new Elements(nodes);

            Assert.That(elements.Raw, Is.EqualTo("<a href='url'>text</a>"));
        }


        private static IEnumerable<HtmlNode> GetNodes(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode.SelectNodes("//*");
        }
    }
}
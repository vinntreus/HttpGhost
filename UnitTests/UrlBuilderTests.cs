using System;
using HttpGhost;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class UrlBuilderTests
    {
        private UrlBuilder GetBuilder(string url, object querystring)
        {
            return new UrlBuilder(url, querystring);
        }

        [TestCase("")]
        [TestCase(null)]
        public void UrlIsEmpty_ThrowsArgumentNullException(string url)
        {
            Assert.That(() => new UrlBuilder(url, null), 
                Throws.TypeOf<ArgumentException>().With.Message.StringContaining("url"));
        }

        [TestCase("a")]
        [TestCase("b")]
        public void NoQuerystring_ReturnsUrl(string url)
        {
            var builder = GetBuilder(url, null);

            var result = builder.Build();

            Assert.That(result, Is.EqualTo(url));
        }

        [Test]
        public void WithQuerystring_AppendQuestionMarkToUrl()
        {
            var builder = GetBuilder("a", new {b = "c"});

            var result = builder.Build();

            Assert.That(result, Is.StringContaining("a?"));
        }

        [Test]
        public void WithQuerystring_AppendPropertyWithValueAfterQuestionMark()
        {
            var builder = GetBuilder("a", new { b = "c" });

            var result = builder.Build();

            Assert.That(result, Is.StringEnding("?b=c"));
        }


        [Test]
        public void WithTwoQuerystringProperties_AppendFirstPropertyWithAmpersand()
        {
            var builder = GetBuilder("a", new { b = "c", d = "e" });

            var result = builder.Build();

            Assert.That(result, Is.StringEnding("c&d=e"));
        }
    }
}
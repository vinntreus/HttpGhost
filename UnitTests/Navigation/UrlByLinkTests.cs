using System;
using HttpGhost.Navigation;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class UrlByLinkTests
    {
        [Test]
        public void Build_WithHref_ReturnsValidUrl()
        {
            var url = UrlByLink.Build("/car", new Uri("http://localhost"));

            Assert.That(url, Is.EqualTo("http://localhost/car"));
        }

        [Test]
        public void Build_UriAsHttps_ReturnsHttpsUrl()
        {
            var url = UrlByLink.Build("/car", new Uri("https://localhost"));

            Assert.That(url, Is.EqualTo("https://localhost/car"));
        }

        [TestCase("http://something/car")]
        [TestCase("https://something/car")]
        [TestCase("https://")]
        [TestCase("http://")]
        public void Build_HrefIsAbsoluteUrl_ReturnHref(string href)
        {
            var url = UrlByLink.Build(href, new Uri("https://localhost"));

            Assert.That(url, Is.EqualTo(href));
        }
    }
}
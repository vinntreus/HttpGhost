using System;
using HttpGhost.Navigation;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class UrlByLinkTests
    {
        private static UrlByLink GetUrlByLink(string href, Uri uri)
        {
            return new UrlByLink(href, uri);
        }

        [Test]
        public void Build_WithHref_ReturnsValidUrl()
        {
            var urlByLink = GetUrlByLink("/car", new Uri("http://localhost"));

            var url = urlByLink.Build();

            Assert.That(url, Is.EqualTo("http://localhost/car"));
        }

        [Test]
        public void Build_UriAsHttps_ReturnsHttpsUrl()
        {
            var urlByLink = GetUrlByLink("/car", new Uri("https://localhost"));

            var url = urlByLink.Build();

            Assert.That(url, Is.EqualTo("https://localhost/car"));
        }

        [TestCase("http://something/car")]
        [TestCase("https://something/car")]
        [TestCase("https://")]
        [TestCase("http://")]
        public void Build_HrefIsAbsoluteUrl_ReturnHref(string href)
        {
            var urlByLink = GetUrlByLink(href, new Uri("https://localhost"));

            var url = urlByLink.Build();

            Assert.That(url, Is.EqualTo(href));
        }
    }
}
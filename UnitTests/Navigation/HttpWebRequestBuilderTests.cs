using System.Net;
using HttpGhost.Navigation;
using HttpGhost.Transport;
using NUnit.Framework;

namespace UnitTests.Navigation
{
    [TestFixture]
    public class HttpWebRequestBuilderTests
    {
        private Request request;
        private const string SOME_URL = "http://a";

        [SetUp]
        public void Setup()
        {
            request = new Request(SOME_URL);
        }

        [TestCase(HttpRequestHeader.ContentType, "a")]
        [TestCase(HttpRequestHeader.Accept, "a")]
        [TestCase(HttpRequestHeader.Connection, "a")]
        [TestCase(HttpRequestHeader.Date, "2000-01-01")]
        [TestCase(HttpRequestHeader.Expect, "a")]
        [TestCase(HttpRequestHeader.IfModifiedSince, "2000-01-01")]
        [TestCase(HttpRequestHeader.Referer, "a")]
        [TestCase(HttpRequestHeader.TransferEncoding, "a")]
        [TestCase(HttpRequestHeader.UserAgent, "a")]
        public void BuildWebRequest_RestrictedTypes_AddsCorrectHeader(HttpRequestHeader header, string value)
        {
            request.AddHeader(header, value);

            var webrequest = HttpWebRequestBuilder.BuildFrom(request);

            Assert.That(webrequest.Headers[header], Is.Not.Null);
        }
    }
}
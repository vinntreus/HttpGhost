using System.Net;
using HttpGhost.Transport;
using NUnit.Framework;

namespace UnitTests.Transport
{
    [TestFixture]
    public class RequestTests
    {
        private const string SOME_URL = "http://a";

        [Test]
        public void AddHeader_SomeHeader_AddsHeader()
        {
            var request = new Request(SOME_URL);

            request.AddHeader(HttpRequestHeader.ContentType, "application/json");

            Assert.That(request.Headers[HttpRequestHeader.ContentType], Is.EqualTo("application/json"));
        }
    }
}
using System.Net;
using HttpGhost;
using HttpGhost.Serialization;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class SessionTests
    {
        private AuthenticatorFake authenticatorFake;
        private NavigatorFake navigatorFake;
        private HttpSession httpSession;

        [SetUp]
        public void Setup()
        {
            authenticatorFake = new AuthenticatorFake();
            navigatorFake = new NavigatorFake();
            httpSession = new HttpSession(authenticatorFake, navigatorFake, new DefaultSerializeBuilder());
        }

        [Test]
        public void Get_Always_RequestHasGETMethod()
        {
            httpSession.Get("http://a");

            var request = navigatorFake.Request;

            Assert.That(request.Method, Is.EqualTo("GET"));
        }

        [Test]
        public void Get_WithoutQuerystring_RequestHasUrlWithoutQuerystring()
        {
            httpSession.Get("http://a");

            var request = navigatorFake.Request;

            Assert.That(request.Url, Is.EqualTo("http://a"));
        }

        [Test]
        public void Get_ProvidedQuerystring_RequestHasUrlWithQuerystring()
        {
            httpSession.Get("http://a", new { b = 2 });

            var request = navigatorFake.Request;

            Assert.That(request.Url, Is.EqualTo("http://a?b=2"));
        }

        [Test]
        public void Get_WithoutSpecifyedContentType_ContentTypeIsEmpty()
        {
            httpSession.Get("http://a");

            var request = navigatorFake.Request;

            Assert.That(request.Headers[HttpRequestHeader.ContentType], Is.Null);
        }

        [Test]
        public void Get_WithContentType_RequestHasContentType()
        {
            httpSession.Get("http://a", null, "application/json");

            var request = navigatorFake.Request;

            Assert.That(request.Headers[HttpRequestHeader.ContentType], Is.EqualTo("application/json"));
        }

        [TestCase(null)]
        [TestCase("application/json")]
        public void Get_Always_RequestBodyIsEmpty(string contentType)
        {
            httpSession.Get("http://a", null, contentType);

            var request = navigatorFake.Request;

            Assert.That(request.Body, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Post_Always_RequestHasPOSTMethod()
        {
            httpSession.Post("http://a", new{ a = 1 });

            var request = navigatorFake.Request;

            Assert.That(request.Method, Is.EqualTo("POST"));
        }

        [Test]
        public void Post_Always_AddPostingObjectToRequestBody()
        {
            httpSession.Post("http://a", new { a = 1 });

            var request = navigatorFake.Request;

            Assert.That(request.Body, Is.EqualTo("a=1"));
        }

        [Test]
        public void Put_Always_RequestHasPUTMethod()
        {
            httpSession.Put("http://a", new { a = 1 });

            var request = navigatorFake.Request;

            Assert.That(request.Method, Is.EqualTo("PUT"));
        }

        [Test]
        public void Put_Always_AddPostingObjectToRequestBody()
        {
            httpSession.Put("http://a", new { a = 2 });

            var request = navigatorFake.Request;

            Assert.That(request.Body, Is.EqualTo("a=2"));
        }

        [Test]
        public void Delete_Always_RequestHasDeleteMethod()
        {
            httpSession.Delete("http://a", new { a = 1 });

            var request = navigatorFake.Request;

            Assert.That(request.Method, Is.EqualTo("DELETE"));
        }

        [Test]
        public void Delete_Always_AddPostingObjectToRequestBody()
        {
            httpSession.Delete("http://a", new { a = 3 });

            var request = navigatorFake.Request;

            Assert.That(request.Body, Is.EqualTo("a=3"));
        }

    }
}

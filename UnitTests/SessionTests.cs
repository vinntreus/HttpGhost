using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HttpGhost;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class SessionTests
    {
        private FakeAuthenticator fakeAuthenticator;
        private FakeNavigator fakeNavigator;
        private Session session;

        [SetUp]
        public void Setup()
        {
            fakeAuthenticator = new FakeAuthenticator();
            fakeNavigator = new FakeNavigator();
            session = new Session(fakeAuthenticator, fakeNavigator);
        }

        [Test]
        public void Get_Always_RequestHasGETMethod()
        {
            session.Get("http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Method, Is.EqualTo("GET"));
        }

        [Test]
        public void Get_WithoutQuerystring_RequestHasUrlWithoutQuerystring()
        {
            session.Get("http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Url, Is.EqualTo("http://a"));
        }

        [Test]
        public void Get_ProvidedQuerystring_RequestHasUrlWithQuerystring()
        {
            session.Get("http://a", new { b = 2 });

            var request = fakeNavigator.Request;

            Assert.That(request.Url, Is.EqualTo("http://a?b=2"));
        }

        [Test]
        public void Get_WithoutSpecifyedContentType_ContentTypeIsEmpty()
        {
            session.Get("http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Headers[HttpRequestHeader.ContentType], Is.Null);
        }

        [Test]
        public void Get_WithContentType_RequestHasContentType()
        {
            session.ContentType = "application/json";
            session.Get("http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Headers[HttpRequestHeader.ContentType], Is.EqualTo("application/json"));
        }

        [TestCase(null)]
        [TestCase("application/json")]
        public void Get_Always_RequestBodyIsEmpty(string contentType)
        {
            session.ContentType = contentType;
            session.Get("http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Body, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Post_Always_RequestHasPOSTMethod()
        {
            session.Post(new{ a = 1 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Method, Is.EqualTo("POST"));
        }

        [Test]
        public void Post_Always_AddPostingObjectToRequestBody()
        {
            session.Post(new { a = 1 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Body, Is.EqualTo("a=1"));
        }

        [Test]
        public void Put_Always_RequestHasPUTMethod()
        {
            session.Put(new { a = 1 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Method, Is.EqualTo("PUT"));
        }

        [Test]
        public void Put_Always_AddPostingObjectToRequestBody()
        {
            session.Put(new { a = 2 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Body, Is.EqualTo("a=2"));
        }

        [Test]
        public void Delete_Always_RequestHasDeleteMethod()
        {
            session.Delete(new { a = 1 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Method, Is.EqualTo("DELETE"));
        }

        [Test]
        public void Delete_Always_AddPostingObjectToRequestBody()
        {
            session.Delete(new { a = 3 }, "http://a");

            var request = fakeNavigator.Request;

            Assert.That(request.Body, Is.EqualTo("a=3"));
        }

    }
}

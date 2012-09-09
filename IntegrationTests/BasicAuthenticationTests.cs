using HttpGhost;
using NUnit.Framework;
using System.Net;

namespace IntegrationTests
{
    [TestFixture]    
    [Category("Integration")]
    public class BasicAuthenticationTests : IntegrationTestsBase
    {
        private const string USERNAME = "user";
        private const string PASSWORD = "pass";
        //this is found in IntegrationTests.Nancy/Basic
        private const string BASE_URL = "http://127.0.0.1:8080/basic";

        [Test]
        public void Session_GetWithoutcredentials_Fails()
        {
            var session = new HttpSession();

            Assert.That(() => session.Get(BASE_URL), 
                        Throws.TypeOf<WebException>().With.Message.StringContaining("401"));            
        }

        [Test]
        public void Session_Get_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Get(BASE_URL);

            Assert.That(result.Response.Body, Is.StringContaining("got it"));
        }

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Post(BASE_URL, new { Title = "jippi" }, ContentType.X_WWW_FORM_URLENCODED);

            Assert.That(result.Response.Body, Is.StringContaining("jippi"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Put(BASE_URL, new { Title = "jippi2" }, ContentType.X_WWW_FORM_URLENCODED);

            Assert.That(result.Response.Body, Is.StringContaining("jippi2"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Delete(BASE_URL, new { Id = 1 }, ContentType.X_WWW_FORM_URLENCODED);

            Assert.That(result.Response.Body, Is.StringContaining("1"));
        }       

        [Test]
        public void Session_PostThatRedirects_ShouldPassCredentialsInRedirect()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Post(BASE_URL + "/redir", new { title = "arne" }, ContentType.X_WWW_FORM_URLENCODED);

            Assert.That(result.Response.Body, Is.StringContaining("got it"));
        }

        private static HttpSession GetSessionWithBasicAuthentication()
        {
            return new HttpSession(USERNAME, PASSWORD);
        }
    }
}
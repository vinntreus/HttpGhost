using HttpGhost;
using NUnit.Framework;
using System.Net;

namespace IntegrationTests
{
    [TestFixture]    
    public class BasicAuthenticationTests : IntegrationTestsBase
    {
        private const string USERNAME = "user";
        private const string PASSWORD = "pass";
        //this is found in IntegrationTests.Nancy/Basic
        private const string BASE_URL = "http://127.0.0.1:8080/basic";

        [Test]
        public void Session_GetWithoutcredentials_Fails()
        {
            var session = new Session();

            Assert.That(() => session.Get(BASE_URL), 
                        Throws.TypeOf<WebException>().With.Message.StringContaining("401"));            
        }

        [Test]
        public void Session_Get_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Get(BASE_URL);

            Assert.That(result.ResponseContent, Is.StringContaining("got it"));
        }

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Post(new { Title = "jippi" }, BASE_URL);

            Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Put(new { Title = "jippi2" }, BASE_URL);

            Assert.That(result.ResponseContent, Is.StringContaining("jippi2"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Delete(new { Id = 1 }, BASE_URL);

            Assert.That(result.ResponseContent, Is.StringContaining("1"));
        }       

        [Test]
        public void Session_PostThatRedirects_ShouldPassCredentialsInRedirect()
        {
            var session = GetSessionWithBasicAuthentication();

            var result = session.Post(new { title = "arne" }, BASE_URL + "/redir");

            Assert.That(result.ResponseContent, Is.StringContaining("got it"));
        }

        private Session GetSessionWithBasicAuthentication()
        {
            return new Session(USERNAME, PASSWORD)
            {
                ContentType = "application/x-www-form-urlencoded" //nancy only transforms Request.Form when this contenttype
            };
        }
    }
}
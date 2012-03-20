using System.Linq;
using System.Collections.Generic;
using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
	public class NoAuthenticationTests : IntegrationTestsBase
	{
        protected readonly string baseUrl = "http://127.0.0.1:8080";
	    private Session session;

	    [SetUp]
	    public void Setup()
	    {
            session = new Session();
	    }
        
		[Test]
		public void Session_Get_ReturnHtml()
		{
			var result = session.Get(baseUrl);

			Assert.That(result.ResponseContent, Is.StringContaining("Getting"));
		}

        [Test]
        public void Session_GetWithQuerystring_ReturnHtml()
        {
            var url = baseUrl + "/get-querystring";
            var result = session.Get(url, new {q = "b"});

            Assert.That(result.ResponseContent, Is.StringContaining("b"));
        }

        [Test]
        public void Session_GetToUrlWhichRedirects_FollowRedirect()
        {
            var url = baseUrl + "/redirect-to-home";
            var result = session.Get(url);

            Assert.That(result.ResponseContent, Is.StringContaining("Getting"));
        }

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var result = session.Post(new { title = "jippi" }, baseUrl);
            
            Assert.That(result.ResponseContent, Is.StringContaining("Posting"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var result = session.Put(new { Title = "jippi" }, baseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("Putting"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var result = session.Delete(new { id = 2 }, baseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("Deleting"));
        }
	}
}

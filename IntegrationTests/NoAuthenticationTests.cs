using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
	public class NoAuthenticationTests : IntegrationTestsBase
	{
        protected readonly string BaseUrl = "http://localhost:1234";
	    private HttpSession session;

	    [SetUp]
	    public void Setup()
	    {
            session = new HttpSession();
	    }
        
		[Test]
		public void Session_Get_ReturnHtml()
		{
			var result = session.Get(BaseUrl);
            
			Assert.That(result.Response.Body, Is.StringContaining("Getting"));
		}        

        [Test]
        public void Session_GetWithQuerystring_ReturnHtml()
        {
            var url = BaseUrl + "/get-querystring";
            var result = session.Get(url, new {q = "b"});

            Assert.That(result.Response.Body, Is.StringContaining("b"));
        }

        [Test]
        public void Session_GetToUrlWhichRedirects_FollowRedirect()
        {
            var url = BaseUrl + "/redirect-to-home";
            var result = session.Get(url);

            Assert.That(result.Response.Body, Is.StringContaining("Getting"));
        }

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var result = session.Post(BaseUrl, new { title = "jippi" });
            
            Assert.That(result.Response.Body, Is.StringContaining("Posting"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var result = session.Put(BaseUrl, new { Title = "jippi" });

            Assert.That(result.Response.Body, Is.StringContaining("Putting"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var result = session.Delete(BaseUrl, new { id = 2 });

            Assert.That(result.Response.Body, Is.StringContaining("Deleting"));
        }
	}
}

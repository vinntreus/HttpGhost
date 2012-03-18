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
	    public override void Setup()
	    {
	        base.Setup();
            session = new Session();
	    }
        
		[Test]
		public void Session_Get_ReturnHtml()
		{
			var result = session.Get(baseUrl);

			Assert.That(result.ResponseContent, Is.StringContaining("Hello"));
		}

        [Test]
        public void Session_Post_ReturnHtml()
        {
            var result = session.Post(new { title = "jippi" }, baseUrl);
            
            Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
        }

        [Test]
        public void Session_Put_ReturnHtml()
        {
            var result = session.Put(new { Title = "jippi" }, baseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
        }

        [Test]
        public void Session_Delete_ReturnHtml()
        {
            var result = session.Delete(new { id = 2 }, baseUrl);

            Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
        }
	}
}

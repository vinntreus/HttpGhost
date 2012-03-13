using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
    [Explicit("Need site up and running")]
	public class NoAuthenticationTests : IntegrationTestsBase
	{
		protected readonly string baseUrl = ConfigurationManager.AppSettings["NoAuthenticationUrl"];

		[TestFixtureSetUp]
		public void BeforeAllTests()
		{
			new Session().Get(baseUrl); //this is for jit warmup, to measure times better for requests
		}

		[Test]
		public void Session_Get_ReturnHtml()
		{
			currentTest = "NoAuthenticationTests_Session_Get_ReturnHtml";
			var session = new Session();
			
			var result = session.Get(baseUrl);

			Assert.That(result.ResponseContent, Is.StringStarting("<!DOCTYPE html>"));
		}

		[Test]
		public void Session_Post_ReturnHtml()
		{
			currentTest = "NoAuthenticationTests_Session_Post_ReturnHtml";
			var session = new Session();

			var result = session.Post(new {Title = "jippi"}, baseUrl + "/Home/Parse");

			Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
		}

		[Test]
		public void Session_Put_ReturnHtml()
		{
			currentTest = "NoAuthenticationTests_Session_Put_ReturnHtml";
			var url = baseUrl + "/Home/Update";
			var session = new Session();

			var result = session.Put(new { Title = "jippi" }, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>New title: jippi</h2>"));
		}

		[Test]
		public void Session_Delete_ReturnHtml()
		{
			currentTest = "NoAuthenticationTests_Session_Delete_ReturnHtml";
			var url = baseUrl + "/Home/Delete/2";
			var session = new Session();

			var result = session.Delete(url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>Deleted: 2</h2>"));
		}
	}
}

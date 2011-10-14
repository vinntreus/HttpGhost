using System.Configuration;
using NUnit.Framework;

namespace RestInspector.IntegrationTests
{
	[TestFixture]
	public class BasicAuthenticationTests : IntegrationTestsBase
	{
		private readonly string username = ConfigurationManager.AppSettings["BasicAuthenticationUsername"];
		private readonly string password = ConfigurationManager.AppSettings["BasicAuthenticationPassword"];
		private readonly string baseUrl = ConfigurationManager.AppSettings["BasicAuthenticationUrl"];

		[TestFixtureSetUp]
		public void BeforeAllTests()
		{
			new Session(username, password).Get(baseUrl); //this is for jit warmup, to measure times better for requests
		}

		[Test]
		public void Session_Get_ReturnHtml()
		{
			currentTest = "BasicAuthenticationTests_Session_Get_ReturnHtml";
			var session = new Session(username, password);
			
			var result = session.Get(baseUrl);

			Assert.That(result.ResponseContent, Is.StringContaining("<html>"));
		}

		[Test]
		public void Session_Post_ReturnHtml()
		{
			currentTest = "BasicAuthenticationTests_Session_Post_ReturnHtml";
			var url = baseUrl + "/Home/Create";
			var session = new Session(username, password);

			var result = session.Post(new { Title = "jippi"}, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>jippi</h2>"));
		}

		[Test]
		public void Session_Put_ReturnHtml()
		{
			currentTest = "BasicAuthenticationTests_Session_Put_ReturnHtml";
			var url = baseUrl + "/Home/Update";
			var session = new Session(username, password);

			var result = session.Put(new { Title = "jippi" }, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>New title: jippi</h2>"));
		}

		[Test]
		public void Session_Delete_ReturnHtml()
		{
			currentTest = "BasicAuthenticationTests_Session_Delete_ReturnHtml";
			var url = baseUrl + "/Home/Delete/2";
			var session = new Session(username, password);

			var result = session.Delete(url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>Deleted: 2</h2>"));
		}

		[Test]
		public void Session_PostThatRedirects_ShouldPassCredentialsInRedirect()
		{
			currentTest = "Session_PostThatRedirects_ShouldPassCredentialsInRedirect";
			var url = baseUrl + "/Home/Redirect";
			var session = new Session(username, password);

			var result = session.Post(new {title = "arne"}, url);

			Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
		}
	}
}
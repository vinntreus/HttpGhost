using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
	[TestFixture]
    [Explicit("Need site up and running")]
	public class BasicAuthenticationTests : IntegrationTestsBase
	{
		private readonly string username = ConfigurationManager.AppSettings["BasicAuthenticationUsername"];
		private readonly string password = ConfigurationManager.AppSettings["BasicAuthenticationPassword"];
		private readonly string baseUrl = ConfigurationManager.AppSettings["BasicAuthenticationUrl"];

		[Test]
		public void Session_Get_ReturnHtml()
		{
			var session = new Session(username, password);
			
			var result = session.Get(baseUrl);

			Assert.That(result.ResponseContent, Is.StringContaining("<html>"));
		}

		[Test]
		public void Session_Post_ReturnHtml()
		{
			var url = baseUrl + "/Home/Parse";
			var session = new Session(username, password);

			var result = session.Post(new { Title = "jippi"}, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>jippi</h2>"));
		}

		[Test]
		public void Session_Put_ReturnHtml()
		{
			var url = baseUrl + "/Home/Update";
			var session = new Session(username, password);

			var result = session.Put(new { Title = "jippi" }, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>New title: jippi</h2>"));
		}

		[Test]
		public void Session_Delete_ReturnHtml()
		{
			var url = baseUrl + "/Home/Delete/";
			var session = new Session(username, password);

			var result = session.Delete(new {id = 2}, url);

			Assert.That(result.ResponseContent, Is.StringContaining("<h2>Deleted: 2</h2>"));
		}

		[Test]
		public void Session_PostThatRedirects_ShouldPassCredentialsInRedirect()
		{
			var url = baseUrl + "/Home/Redirect";
			var session = new Session(username, password);

			var result = session.Post(new {title = "arne"}, url);

			Assert.That(result.ResponseContent, Is.StringContaining("jippi"));
		}
	}
}
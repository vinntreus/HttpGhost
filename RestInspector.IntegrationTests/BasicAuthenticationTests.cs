using System.Configuration;
using NUnit.Framework;

namespace RestInspector.IntegrationTests
{
	[TestFixture]
	public class BasicAuthenticationTests
	{
		[Test]
		public void Session_GetHtml_ReturnHtml()
		{
			var url = ConfigurationManager.AppSettings["BasicAuthenticationUrl"];
			var username = ConfigurationManager.AppSettings["BasicAuthenticationUsername"];
			var password = ConfigurationManager.AppSettings["BasicAuthenticationPassword"];
			var session = new Session(username, password);

			var result = session.Get(url);

			Assert.That(result.Html, Is.StringContaining("<html>"));
		}
	}
}
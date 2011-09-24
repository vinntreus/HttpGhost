using System.Configuration;
using NUnit.Framework;

namespace WebTester.IntegrationTests
{
	[TestFixture]
	public class NoAuthenticationTests
	{
		[Test]
		public void Session_GetHtml_ReturnHtml()
		{
			var session = new Session(ConfigurationManager.AppSettings["NoAuthenticationUrl"]);

			var result = session.Get();

			Assert.That(result.Html, Is.StringStarting("<!DOCTYPE html>"));
		}
	}
}

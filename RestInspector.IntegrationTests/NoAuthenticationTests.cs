using System.Configuration;
using NUnit.Framework;

namespace RestInspector.IntegrationTests
{
	[TestFixture]
	public class NoAuthenticationTests
	{
		private readonly string BaseUrl = ConfigurationManager.AppSettings["NoAuthenticationUrl"];

		[Test]
		public void Session_GetHtml_ReturnHtml()
		{
			var session = new Session(BaseUrl);

			var result = session.Get();

			Assert.That(result.Html, Is.StringStarting("<!DOCTYPE html>"));
		}

		[Test]
		public void Session_PostHtml_ReturnHtml()
		{
			var session = new Session(BaseUrl + "/Home/create/");

			var result = session.Post(new {Title = "jippi"});

			Assert.That(result.Html, Is.StringContaining("jippi"));
		}
	}
}

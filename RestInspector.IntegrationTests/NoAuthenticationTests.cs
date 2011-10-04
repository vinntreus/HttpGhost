using System;
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
			var session = new Session();

			var stopwatch = System.Diagnostics.Stopwatch.StartNew();
			var result = session.Get(BaseUrl);
			stopwatch.Stop();

			Assert.That(result.Html, Is.StringStarting("<!DOCTYPE html>"));
			Console.WriteLine(string.Format("Finished Session_GetHtml_ReturnHtml in {0}ms", stopwatch.ElapsedMilliseconds));
		}

		[Test]
		public void Session_PostHtml_ReturnHtml()
		{
			var session = new Session();

			var result = session.Post(new {Title = "jippi"}, BaseUrl + "/Home/Create");

			Assert.That(result.Html, Is.StringContaining("jippi"));
		}
	}
}

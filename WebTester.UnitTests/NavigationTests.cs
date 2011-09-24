using System;
using System.Net;
using NUnit.Framework;

namespace WebTester.UnitTests
{
	[TestFixture]
	public class NavigationTests
	{
		private TestableNavigator navigator;

		[SetUp]
		public void Setup()
		{
			navigator = new TestableNavigator(new Uri("http://noauth.local")); //uri is not important in these tests
		}

		[Test]
		public void Get_ValidUrl_ReturnHttpStatus()
		{
			navigator.responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = navigator.Get();

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Get_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			navigator.responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Get();

			Assert.That(result.Html, Is.EqualTo(htmlBodyBodyHtml));
		}
	}
}
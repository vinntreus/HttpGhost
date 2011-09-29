using System;
using System.Net;
using System.Text;
using Moq;
using NUnit.Framework;
using RestInspector.Authentication;

namespace RestInspector.UnitTests.Navigation
{
	[TestFixture]
	public class NavigatorTests
	{
		private TestableNavigator navigator;
		private const string some_url = "http:/a";

		private Mock<ISerializer> serializerMock;
		[SetUp]
		public void Setup()
		{
			serializerMock = new Mock<ISerializer>();
			
			navigator = new TestableNavigator(serializerMock.Object);
		}

		[Test]
		public void Get_ValidUrl_ReturnHttpStatus()
		{
			navigator.responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = navigator.Get(some_url, null);

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Get_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			navigator.responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Get(some_url, null);

			Assert.That(result.Html, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Post_ValidUrl_ReturnHttpStatus()
		{
			navigator.responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = navigator.Post("a=a",some_url, null);

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Post_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			navigator.responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Post("b=b",some_url, null);

			Assert.That(result.Html, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Get_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			navigator.Get(some_url, expectedAuthenticationInfo);

			navigator.requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			navigator.Post("a=a", some_url, expectedAuthenticationInfo);

			navigator.requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetMethodToPost()
		{
			navigator.Post("a=a", some_url, null);

			navigator.requestMock.Verify(r => r.SetMethod("Post"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetCorrectContentType()
		{
			navigator.Post("a=a", some_url, null);

			navigator.requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetFormCollection()
		{
			serializerMock.Setup(s => s.Serialize(It.IsAny<object>())).Returns("jihaa");
			navigator.Post("a=a", some_url, null);
			
			navigator.requestMock.Verify(r => r.WriteFormDataToRequestStream("jihaa"), Times.Once());
		}
	}
}
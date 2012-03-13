using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost;
using HttpGhost.Authentication;
using Moq;
using NUnit.Framework;

namespace UnitTests.Navigation
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

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
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

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
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

		[Test]
		public void Put_ShouldSetMethodToPut()
		{
			navigator.Put("a", some_url, null);

			navigator.requestMock.Verify(r => r.SetMethod("Put"), Times.Once());
		}

		[Test]
		public void Put_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
			navigator.Put("a", some_url, authenticationInfo);

			navigator.requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Put_ShouldSetFormCollection()
		{
			serializerMock.Setup(s => s.Serialize(It.IsAny<object>())).Returns("jihaa");
			navigator.Put("a=a", some_url, null);

			navigator.requestMock.Verify(r => r.WriteFormDataToRequestStream("jihaa"), Times.Once());
		}

		[Test]
		public void Put_SHouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			navigator.responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Put("b=b", some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Put_ShouldSetCorrectContentType()
		{
			navigator.Put("a=a", some_url, null);

			navigator.requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Delete_ShouldSetMethodToDelete()
		{
			navigator.Delete(some_url, null);

			navigator.requestMock.Verify(r => r.SetMethod("Delete"), Times.Once());
		}

		[Test]
		public void Delete_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			navigator.Delete(some_url, authenticationInfo);

			navigator.requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Delete_SHouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			navigator.responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Delete(some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}
	}
}
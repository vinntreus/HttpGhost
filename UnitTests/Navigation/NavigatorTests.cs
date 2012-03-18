using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Transport;
using Moq;
using NUnit.Framework;

namespace UnitTests.Navigation
{
	[TestFixture]
	public class NavigatorTests
	{
		private const string some_url = "http:/a";
		
	    private Navigator navigator;
	    private Mock<IRequest> requestMock;
	    private Mock<IResponse> responseMock;

	    [SetUp]
		public void Setup()
		{
            var factoryMock = new Mock<IRequestFactory>();
            requestMock = new Mock<IRequest>();
	        responseMock = new Mock<IResponse>();
	        requestMock.Setup(r => r.GetResponse()).Returns(responseMock.Object);
            factoryMock.Setup(f => f.Create(It.IsAny<string>())).Returns(requestMock.Object);

            navigator = new Navigator(factoryMock.Object);
		}

		[Test]
		public void Get_ValidUrl_ReturnHttpStatus()
		{
			responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = navigator.Get(some_url, null);

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Get_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Get(some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

	    [Test]
	    public void Get_ValidUrl_ReturnsUrl()
	    {
	        requestMock.Setup(n => n.Url).Returns("http://ab");
            var result = navigator.Get(some_url, null);

            Assert.That(result.RequestUrl, Is.EqualTo("http://ab"));
	    }

		[Test]
		public void Post_ValidUrl_ReturnHttpStatus()
		{
			responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = navigator.Post("a=a",some_url, null);

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Post_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Post("b=b",some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Get_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			navigator.Get(some_url, expectedAuthenticationInfo);

			requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			navigator.Post("a=a", some_url, expectedAuthenticationInfo);

			requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetMethodToPost()
		{
			navigator.Post("a=a", some_url, null);

			requestMock.Verify(r => r.SetMethod("Post"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetCorrectContentType()
		{
			navigator.Post("a=a", some_url, null);

			requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetFormCollection()
		{
            navigator.Post("a=a", some_url, null);
			
			requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"a=a"), Times.Once());
		}

		[Test]
		public void Put_ShouldSetMethodToPut()
		{
			navigator.Put("a", some_url, null);

			requestMock.Verify(r => r.SetMethod("Put"), Times.Once());
		}

		[Test]
		public void Put_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
			navigator.Put("a", some_url, authenticationInfo);

			requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Put_ShouldSetFormCollection()
		{
			navigator.Put("a=b", some_url, null);

			requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"a=b"), Times.Once());
		}

		[Test]
		public void Put_SHouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Put("b=b", some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Put_ShouldSetCorrectContentType()
		{
			navigator.Put("a=a", some_url, null);

			requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Delete_ShouldSetMethodToDelete()
		{
			navigator.Delete("a=a", some_url, null);

			requestMock.Verify(r => r.SetMethod("Delete"), Times.Once());
		}

        [Test]
        public void Delete_ShouldSetFormCollection()
        {
            navigator.Delete("b=a", some_url, null);

            requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"b=a"), Times.Once());
        }

		[Test]
		public void Delete_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			navigator.Delete("a=a", some_url, authenticationInfo);

			requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Delete_ShouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = navigator.Delete("a=a", some_url, null);

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}
	}
}
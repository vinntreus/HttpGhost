using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Navigation.Methods;
using HttpGhost.Transport;
using Moq;
using NUnit.Framework;

namespace UnitTests.Navigation
{
	[TestFixture]
	public class NavigatorTests
	{
	    private Mock<IRequest> requestMock;
	    private Mock<IResponse> responseMock;

	    private Get GetNavigator(GetNavigationOptions options = null)
	    {
	        return new Get(requestMock.Object, options ?? new GetNavigationOptions(null, null));
	    }

        private Post PostNavigator(PostNavigationOptions options = null)
        {
            return new Post(requestMock.Object, options ?? new PostNavigationOptions(null, null));
        }

        private Put PutNavigator(PutNavigationOptions options = null)
        {
            return new Put(requestMock.Object, options ?? new PutNavigationOptions(null, null));
        }

        private Delete DeleteNavigator(DeleteNavigationOptions options = null)
        {
            return new Delete(requestMock.Object, options ?? new DeleteNavigationOptions(null, null));
        }

	    [SetUp]
		public void Setup()
		{
            requestMock = new Mock<IRequest>();
	        responseMock = new Mock<IResponse>();
	        requestMock.Setup(r => r.GetResponse()).Returns(responseMock.Object);
		}

		[Test]
		public void Get_ValidUrl_ReturnHttpStatus()
		{
			responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = GetNavigator().Navigate();

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Get_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

            var result = GetNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

	    [Test]
	    public void Get_ValidUrl_ReturnsUrl()
	    {
	        requestMock.Setup(n => n.Url).Returns("http://ab");

            var result = GetNavigator().Navigate();

            Assert.That(result.RequestUrl, Is.EqualTo("http://ab"));
	    }

		[Test]
		public void Post_ValidUrl_ReturnHttpStatus()
		{
			responseMock.Setup(n => n.StatusCode).Returns(HttpStatusCode.OK);

			var result = PostNavigator().Navigate();

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Post_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

            var result = PostNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Get_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			GetNavigator(new GetNavigationOptions(expectedAuthenticationInfo, ""));

			requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
            PostNavigator(new PostNavigationOptions(null, expectedAuthenticationInfo));

			requestMock.Verify(r => r.SetAuthentication(expectedAuthenticationInfo), Times.Once());
		}

		[Test]
		public void Post_ShouldSetMethodToPost()
		{
			PostNavigator();

			requestMock.Verify(r => r.SetMethod("Post"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetCorrectContentType()
		{
            PostNavigator();

			requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Post_ShouldSetFormCollection()
		{
            PostNavigator(new PostNavigationOptions("a=a", null));
			
			requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"a=a"), Times.Once());
		}

		[Test]
		public void Put_ShouldSetMethodToPut()
		{
			PutNavigator();

			requestMock.Verify(r => r.SetMethod("Put"), Times.Once());
		}

		[Test]
		public void Put_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
			PutNavigator(new PutNavigationOptions(null, authenticationInfo));

			requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Put_ShouldSetFormCollection()
		{
		    PutNavigator(new PutNavigationOptions("a=b", null));

			requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"a=b"), Times.Once());
		}

		[Test]
		public void Put_SHouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

			var result = PutNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Put_ShouldSetCorrectContentType()
		{
			PutNavigator();

			requestMock.Verify(r => r.SetContentType("application/x-www-form-urlencoded"), Times.Once());
		}

		[Test]
		public void Delete_ShouldSetMethodToDelete()
		{
		    DeleteNavigator();

			requestMock.Verify(r => r.SetMethod("Delete"), Times.Once());
		}

        [Test]
        public void Delete_ShouldSetFormCollection()
        {
            DeleteNavigator(new DeleteNavigationOptions("b=a",null));

            requestMock.Verify(r => r.WriteFormDataToRequestStream((object)"b=a"), Times.Once());
        }

		[Test]
		public void Delete_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			DeleteNavigator(new DeleteNavigationOptions(null, authenticationInfo));

			requestMock.Verify(r => r.SetAuthentication(authenticationInfo), Times.Once());
		}

		[Test]
		public void Delete_ShouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Setup(n => n.Html).Returns(htmlBodyBodyHtml);

		    var result = DeleteNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}
	}
}
using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Navigation.Methods;
using NUnit.Framework;

namespace UnitTests.Navigation
{
	[TestFixture]
	public class NavigatorTests
	{
	    private FakeRequest requestMock;
	    private FakeResponse responseMock;

	    private Get GetNavigator(GetNavigationOptions options = null)
	    {
	        return new Get(requestMock, options ?? new GetNavigationOptions(null, null));
	    }

        private Post PostNavigator(PostNavigationOptions options = null)
        {
            return new Post(requestMock, options ?? new PostNavigationOptions(null, null));
        }

        private Put PutNavigator(PutNavigationOptions options = null)
        {
            return new Put(requestMock, options ?? new PutNavigationOptions(null, null));
        }

        private Delete DeleteNavigator(DeleteNavigationOptions options = null)
        {
            return new Delete(requestMock, options ?? new DeleteNavigationOptions(null, null));
        }

	    [SetUp]
		public void Setup()
		{
            responseMock = new FakeResponse();
            requestMock = new FakeRequest(responseMock);
		}

		[Test]
		public void Get_ValidUrl_ReturnHttpStatus()
		{
			responseMock.StatusCode = HttpStatusCode.OK;

			var result = GetNavigator().Navigate();

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Get_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Html = htmlBodyBodyHtml;

            var result = GetNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

	    [Test]
	    public void Get_ValidUrl_ReturnsUrl()
	    {
	        requestMock.Url = "http://ab";

            var result = GetNavigator().Navigate();

            Assert.That(result.RequestUrl, Is.EqualTo("http://ab"));
	    }

		[Test]
		public void Post_ValidUrl_ReturnHttpStatus()
		{
			responseMock.StatusCode = HttpStatusCode.OK;

			var result = PostNavigator().Navigate();

			Assert.That(result.Status, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Post_ValidUrl_ReturnHtml()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Html = htmlBodyBodyHtml;

            var result = PostNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Get_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			GetNavigator(new GetNavigationOptions(expectedAuthenticationInfo, ""));

            Assert.That(requestMock.HaveSetAuthentication, Is.EqualTo(1));
		}

		[Test]
		public void Post_ShouldSetAuthentication()
		{
			var expectedAuthenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
            PostNavigator(new PostNavigationOptions(null, expectedAuthenticationInfo));

            Assert.That(requestMock.HaveSetAuthentication, Is.EqualTo(1));
		}

		[Test]
		public void Post_ShouldSetMethodToPost()
		{
			PostNavigator();
			
            Assert.That(requestMock.HaveSetMethodWith("Post"), Is.EqualTo(1));
		}

		[Test]
		public void Post_ShouldSetCorrectContentType()
		{
            PostNavigator();

            Assert.That(requestMock.HaveSetContentTypeWith("application/x-www-form-urlencoded"), Is.EqualTo(1));
		}

		[Test]
		public void Post_ShouldSetFormCollection()
		{
            PostNavigator(new PostNavigationOptions("a=a", null));

            Assert.That(requestMock.HaveSetFormDataWith((object)"a=a"), Is.EqualTo(1));
		}

		[Test]
		public void Put_ShouldSetMethodToPut()
		{
			PutNavigator();
			
            Assert.That(requestMock.HaveSetMethodWith("Put"), Is.EqualTo(1));
		}

		[Test]
		public void Put_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));
			
			PutNavigator(new PutNavigationOptions(null, authenticationInfo));

		    Assert.That(requestMock.HaveSetAuthentication, Is.EqualTo(1));
		}

		[Test]
		public void Put_ShouldSetFormCollection()
		{
		    PutNavigator(new PutNavigationOptions("a=b", null));

			Assert.That(requestMock.HaveSetFormDataWith((object)"a=b"), Is.EqualTo(1));
		}

		[Test]
		public void Put_SHouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Html = htmlBodyBodyHtml;

			var result = PutNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}

		[Test]
		public void Put_ShouldSetCorrectContentType()
		{
			PutNavigator();

            Assert.That(requestMock.HaveSetContentTypeWith("application/x-www-form-urlencoded"), Is.EqualTo(1));
		}

		[Test]
		public void Delete_ShouldSetMethodToDelete()
		{
		    DeleteNavigator();
			
            Assert.That(requestMock.HaveSetMethodWith("Delete"), Is.EqualTo(1));
		}

        [Test]
        public void Delete_ShouldSetFormCollection()
        {
            DeleteNavigator(new DeleteNavigationOptions("b=a",null));
            
            Assert.That(requestMock.HaveSetFormDataWith((object)"b=a"), Is.EqualTo(1));
        }

		[Test]
		public void Delete_ShouldSetAuthenticationInfo()
		{
			var authenticationInfo = new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b"));

			DeleteNavigator(new DeleteNavigationOptions(null, authenticationInfo));

		    Assert.That(requestMock.HaveSetAuthentication, Is.EqualTo(1));
		}

		[Test]
		public void Delete_ShouldReturnHtmlFromResponse()
		{
			const string htmlBodyBodyHtml = "<html><body></body></html>";
			responseMock.Html = htmlBodyBodyHtml;

		    var result = DeleteNavigator().Navigate();

			Assert.That(result.ResponseContent, Is.EqualTo(htmlBodyBodyHtml));
		}
	}
}
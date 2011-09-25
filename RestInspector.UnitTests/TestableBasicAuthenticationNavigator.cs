using System;
using System.Net;
using Moq;
using RestInspector.Authentication;
using RestInspector.Infrastructure;
using RestInspector.Navigation.Implementation;

namespace RestInspector.UnitTests
{
	public class TestableBasicAuthenticationNavigator : BasicAuthenticationNavigator
	{
		public readonly Mock<IWebResponse> responseMock = new Mock<IWebResponse>();
		public readonly Mock<IWebRequest> requestMock = new Mock<IWebRequest>();

		public readonly WebHeaderCollection SetHeaders = new WebHeaderCollection();

		public TestableBasicAuthenticationNavigator(Uri uri, Credentials credentials)
			: base(uri, credentials)
		{
			requestMock.Setup(r => r.GetResponse()).Returns(responseMock.Object);
			requestMock.Setup(r => r.Headers).Returns(SetHeaders);
			requestMock.SetupProperty(r => r.Credentials);
		}


		protected override IWebRequest CreateWebRequest()
		{
			return requestMock.Object;
		}

		public Credentials GetCredentials()
		{
			return this.credentials;
		}
	}
}
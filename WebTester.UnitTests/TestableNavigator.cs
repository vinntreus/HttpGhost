using System;
using Moq;
using WebTester.Infrastructure;
using WebTester.Navigation;
using WebTester.Navigation.Implementation;

namespace WebTester.UnitTests
{
	public class TestableNavigator : Navigator
	{
		public Mock<IWebResponse> responseMock = new Mock<IWebResponse>(); 
		public Mock<IWebRequest> requestMock = new Mock<IWebRequest>();

		public TestableNavigator(Uri uri) : base(uri)
		{
			requestMock.Setup(r => r.GetResponse()).Returns(responseMock.Object);
		}

		protected override IWebRequest CreateWebRequest()
		{
			return requestMock.Object;
		}
	}
}
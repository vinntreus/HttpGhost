using System;
using Moq;
using RestInspector.Infrastructure;
using RestInspector.Navigation.Implementation;

namespace RestInspector.UnitTests
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
using Moq;
using RestInspector.Navigation.Implementation;
using RestInspector.Transport;

namespace RestInspector.UnitTests
{
	public class TestableNavigator : Navigator
	{
		public Mock<IResponse> responseMock = new Mock<IResponse>(); 
		public Mock<IRequest> requestMock = new Mock<IRequest>();

		public TestableNavigator(ISerializer serializer) : base(serializer)
		{
			requestMock.Setup(r => r.GetResponse()).Returns(responseMock.Object);
		}

		protected override IRequest CreateWebRequest(string url)
		{
			return requestMock.Object;
		}
	}
}
using System.Linq;
using System.Collections.Generic;
using HttpGhost;
using HttpGhost.Navigation.Implementation;
using HttpGhost.Transport;
using Moq;

namespace UnitTests
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
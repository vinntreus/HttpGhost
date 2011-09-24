using System.Net;
using WebTester.Navigation;

namespace WebTester.UnitTests
{
	public class TestableNavigationResult : INavigationResult
	{
		public HttpStatusCode Status
		{
			get { throw new System.NotImplementedException(); }
		}

		public string Html
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
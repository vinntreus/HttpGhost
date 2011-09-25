using System.Net;
using RestInspector.Navigation;

namespace RestInspector.UnitTests
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
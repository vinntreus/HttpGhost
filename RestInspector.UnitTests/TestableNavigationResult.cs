using System.Net;
using RestInspector.Navigation;

namespace RestInspector.UnitTests
{
	public class TestableNavigationResult : INavigationResult
	{
		public HttpStatusCode Status
		{
			get { return HttpStatusCode.OK;}
		}

		public string ResponseString
		{
			get { throw new System.NotImplementedException(); }
		}

		public object AsContentTypeFormat
		{
			get { throw new System.NotImplementedException(); }
		}

		public WebHeaderCollection ResponseHeaders
		{
			get { return new WebHeaderCollection(); }
		}
	}
}
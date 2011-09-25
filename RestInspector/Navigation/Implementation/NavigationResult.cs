using System.Net;
using RestInspector.Infrastructure;

namespace RestInspector.Navigation.Implementation
{
	public class NavigationResult : INavigationResult
	{
		private readonly IWebResponse webResponse;

		public NavigationResult(IWebResponse webResponse)
		{
			this.webResponse = webResponse;
		}

		public HttpStatusCode Status { get { return webResponse.StatusCode; } }

		public string Html { get { return webResponse.Html; } }
	}
}
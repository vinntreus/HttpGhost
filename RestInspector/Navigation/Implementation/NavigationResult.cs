using System.Net;
using RestInspector.Transport;

namespace RestInspector.Navigation.Implementation
{
	public class NavigationResult : INavigationResult
	{
		private readonly IResponse response;

		public NavigationResult(IResponse response)
		{
			this.response = response;
		}

		public HttpStatusCode Status { get { return response.StatusCode; } }

		public string Html { get { return response.Html; } }
	}
}
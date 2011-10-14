using System.Net;
using RestInspector.Transport;

namespace RestInspector.Navigation.Implementation
{
	public class NavigationResult : INavigationResult
	{
		protected readonly IResponse response;

		public NavigationResult(IResponse response)
		{
			this.response = response;
		}

		public HttpStatusCode Status { get { return response.StatusCode; } }

		public string ResponseContent { get { return response.Html; } }
		
		public WebHeaderCollection ResponseHeaders
		{
			get { return response.Headers; }
		}
	}
}
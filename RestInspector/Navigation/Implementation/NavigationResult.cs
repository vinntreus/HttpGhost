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

		public string ResponseString { get { return response.Html; } }

		public virtual object AsContentTypeFormat
		{
			get { return ResponseString; }
		}
	}
}
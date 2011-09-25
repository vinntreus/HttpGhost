using System;
using System.Net;
using RestInspector.Infrastructure;
using RestInspector.Navigation.Implementation;

namespace RestInspector.Navigation
{
	public abstract class AbstractNavigator : INavigator
	{
		protected Uri uri;
		protected IWebRequest webRequest;

		protected AbstractNavigator(Uri uri)
		{
			if(uri == null)
				throw new ArgumentNullException("uri");

			this.uri = uri;
		}

		protected abstract void OnGet();

		public INavigationResult Get()
		{
			OnGet();

			var response = GetWebResponse(webRequest);

			return new NavigationResult(response);
		}

		public INavigationResult Post(object postingObject)
		{
			webRequest = CreateWebRequest();

			webRequest.ObjectToPost(postingObject);

			var response = GetWebResponse(webRequest);

			return new NavigationResult(response);
		}

		protected virtual IWebResponse GetWebResponse(IWebRequest request)
		{
			return request.GetResponse();
		}

		protected virtual IWebRequest CreateWebRequest()
		{
			return new WrappedHttpWebRequest((HttpWebRequest)WebRequest.Create(uri));
		}
	}
}
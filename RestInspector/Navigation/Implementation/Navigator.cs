using System;
using System.Net;
using RestInspector.Authentication;
using RestInspector.Transport;
using RestInspector.Transport.Implementation;

namespace RestInspector.Navigation.Implementation
{
	public class Navigator : INavigator
	{
		private readonly ISerializer serializer;

		public Navigator(ISerializer serializer)
		{
			this.serializer = serializer;
		}

		public INavigationResult Get(string url, AuthenticationInfo authentication)
		{
			var request = CreateWebRequest(url);
			request.SetAuthentication(authentication);
			var response = request.GetResponse();

			return new NavigationResult(response);
		}

		public INavigationResult Post(object postingObject, string url, AuthenticationInfo authenticationInfo)
		{
			var webRequest = CreateWebRequest(url);
			webRequest.SetAuthentication(authenticationInfo);
			webRequest.SetMethod("Post");
			webRequest.SetContentType("application/x-www-form-urlencoded");
			var formData = serializer.Serialize(postingObject);
			webRequest.WriteFormDataToRequestStream(formData);
			
			var response = GetWebResponse(webRequest);

			return new NavigationResult(response);
		}

		protected virtual IResponse GetWebResponse(IRequest request)
		{
			return request.GetResponse();
		}

		protected virtual IRequest CreateWebRequest(string url)
		{
			return new Request((HttpWebRequest)WebRequest.Create(new Uri(url)));
		}
	}
}
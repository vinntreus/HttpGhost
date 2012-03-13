using System.Linq;
using System.Collections.Generic;
using System;
using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Transport;
using HttpGhost.Transport.Implementation;

namespace HttpGhost.Navigation.Implementation
{
	public class Navigator : INavigator
	{
		private readonly ISerializer serializer;

		public Navigator(ISerializer serializer)
		{
			this.serializer = serializer;
		}

		public INavigationResult Get(string url, AuthenticationInfo authentication, string contentType = null)
		{
			var request = CreateWebRequest(url);
			request.SetContentType(contentType);
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
			SerializePostingObjectToRequest(postingObject, webRequest);

			return ResultFromResponse(webRequest);
		}

		public INavigationResult Put(object postingObject, string url, AuthenticationInfo authenticationInfo)
		{
			var webRequest = CreateWebRequest(url);
			webRequest.SetAuthentication(authenticationInfo);
			webRequest.SetMethod("Put");
			webRequest.SetContentType("application/x-www-form-urlencoded");
			SerializePostingObjectToRequest(postingObject, webRequest);

			return ResultFromResponse(webRequest);
		}

		public INavigationResult Delete(string url, AuthenticationInfo authenticationInfo)
		{
			var webRequest = CreateWebRequest(url);
			webRequest.SetAuthentication(authenticationInfo);
			webRequest.SetMethod("Delete");

			return ResultFromResponse(webRequest);
		}

		private void SerializePostingObjectToRequest(object postingObject, IRequest webRequest)
		{
			var formData = serializer.Serialize(postingObject);
			webRequest.WriteFormDataToRequestStream(formData);
		}

		private INavigationResult ResultFromResponse(IRequest webRequest)
		{
			var response = GetWebResponse(webRequest);

			if(response.StatusCode == HttpStatusCode.Redirect)
			{
				
			}

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
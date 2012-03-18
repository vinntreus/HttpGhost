using System.Linq;
using System.Collections.Generic;
using System;
using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Navigation.Implementation;

namespace HttpGhost
{
	public class Session
	{
		protected INavigator navigator;
		public AuthenticationInfo Authentication { get; private set; }
		public bool AutoFollowRedirect { get; set; }

		public string ContentType { get; set; }

		/// <summary>
		/// Sets the AuthenticationType to Anonymous
		/// </summary>
		public Session() : this(AuthenticationType.Anonymous, null){}

		/// <summary>
		/// Sets the AuthenticationType to Basic
		/// </summary>
		public Session(string username, string password) : this(AuthenticationType.BasicAuthentication, new Credentials(username, password)){}

		private Session(AuthenticationType type, Credentials credentials)
		{
			Authentication = new AuthenticationInfo(type, credentials);
			navigator = new Navigator(new FormSerializer());
			AutoFollowRedirect = true;
		}

		public INavigationResult Get(string url, object querystring = null)
		{
			return GetResult(() => navigator.Get(url, Authentication, ContentType, querystring));
		}

		public INavigationResult Post(object postingObject, string url)
		{
			return GetResult(() => navigator.Post(postingObject, url, Authentication));
		}

		public INavigationResult Put(object postingObject, string url)
		{
			return GetResult(() => navigator.Put(postingObject, url, Authentication));
		}

		public INavigationResult Delete(object postingObject, string url)
		{
			return GetResult(() => navigator.Delete(postingObject, url, Authentication));
		}

		private INavigationResult GetResult(Func<INavigationResult> navigate)
		{
			var result = navigate();

			if (AutoFollowRedirect && result.Status == HttpStatusCode.Redirect)
			{
				var location = result.ResponseHeaders["Location"];
				if (!string.IsNullOrEmpty(location))
				{
					var uri = new Uri(result.RequestUrl);
					var urlToRedirectTo = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, location);

					return GetResult(() => navigator.Get(urlToRedirectTo, Authentication, ContentType));
				}
			}
			return result;
		}
	}
}
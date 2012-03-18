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

		public INavigationResult Get(string url)
		{
			return GetResult(u => navigator.Get(u, Authentication, ContentType), url);
		}

		public INavigationResult Post(object postingObject, string url)
		{
			return GetResult(u => navigator.Post(postingObject, u, Authentication), url);
		}

		public INavigationResult Put(object postingObject, string url)
		{
			return GetResult(u => navigator.Put(postingObject, url, Authentication), url);
		}

		public INavigationResult Delete(object postingObject, string url)
		{
			return GetResult(u => navigator.Delete(postingObject, url, Authentication), url);
		}

		private INavigationResult GetResult(Func<string, INavigationResult> navigate, string url)
		{
			var result = navigate(url);

			if (AutoFollowRedirect && result.Status == HttpStatusCode.Redirect)
			{
				var location = result.ResponseHeaders["Location"];
				if (!string.IsNullOrEmpty(location))
				{
					var uri = new Uri(url);
					var urlToRedirectTo = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, location);

					return GetResult(u => navigator.Get(u, Authentication, ContentType), urlToRedirectTo);
				}
			}
			return result;
		}
	}
}
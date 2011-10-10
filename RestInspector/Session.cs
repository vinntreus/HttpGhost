using RestInspector.Authentication;
using RestInspector.Navigation;
using RestInspector.Navigation.Implementation;

namespace RestInspector
{
	public class Session
	{
		protected INavigator navigator;
		public AuthenticationInfo Authentication { get; private set; }

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
		}

		public INavigationResult Get(string url)
		{
			return navigator.Get(url, Authentication, ContentType);
		}

		public INavigationResult Post(object postingObject, string url)
		{
			return navigator.Post(postingObject, url, Authentication);
		}

		public INavigationResult Put(object postingObject, string url)
		{
			return navigator.Put(postingObject, url, Authentication);
		}

		public INavigationResult Delete(string url)
		{
			return navigator.Delete(url, Authentication);
		}
	}
}
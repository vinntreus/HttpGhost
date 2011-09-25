using System;
using RestInspector.Authentication;
using RestInspector.Navigation;
using RestInspector.Navigation.Implementation;

namespace RestInspector
{
	public class Session
	{
		private readonly Uri url;
		protected INavigatorFactory navigatorFactory;
		public AuthenticationType Authentication { get; private set; }
		public Credentials Credentials { get; private set; }

		/// <summary>
		/// Defaults the AuthenticationType to Anonymous
		/// </summary>
		/// <param name="url"></param>
		public Session(string url)
		{
			this.url = new Uri(url);
			Authentication = AuthenticationType.Anonymous;
			Credentials = null;
			navigatorFactory = new NavigatorFactory();
		}

		/// <summary>
		/// Defaults the AuthenticationType to BasicAuthentication (to switch, use WithAuthentication)
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public Session As(string username, string password)
		{
			Credentials = new Credentials(username, password);
			Authentication = AuthenticationType.BasicAuthentication;
			
			return this;
		}

		public Session WithAuthentication(AuthenticationType authenticationType)
		{
			Authentication = authenticationType;
			return this;
		}

		public INavigationResult Get()
		{
			var navigator = CreateNavigator();
			return navigator.Get();
		}

		public INavigationResult Post(object postingObject)
		{
			var navigator = CreateNavigator();
			return navigator.Post(postingObject);
		}

		private INavigator CreateNavigator()
		{
			return navigatorFactory.Create(Authentication, Credentials, url);
		}
	}
}
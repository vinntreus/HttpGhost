using System;
using WebTester.Authentication;

namespace WebTester.Navigation.Implementation
{
	public class NavigatorFactory : INavigatorFactory
	{
		public INavigator Create(AuthenticationType type, Credentials credentials, Uri url)
		{
			switch (type)
			{
				case AuthenticationType.BasicAuthentication: 
					return new BasicAuthenticationNavigator(url, credentials);
				default:
					return new Navigator(url);
			}
			
		}
	}
}
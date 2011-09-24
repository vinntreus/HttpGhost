using System;
using WebTester.Authentication;

namespace WebTester.Navigation
{
	public interface INavigatorFactory
	{
		INavigator Create(AuthenticationType type, Credentials credentials, Uri url);
	}
}
using System;
using RestInspector.Authentication;

namespace RestInspector.Navigation
{
	public interface INavigatorFactory
	{
		INavigator Create(AuthenticationType type, Credentials credentials, Uri url);
	}
}
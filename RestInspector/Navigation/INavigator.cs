using RestInspector.Authentication;

namespace RestInspector.Navigation
{
	public interface INavigator
	{
		INavigationResult Get(string url, AuthenticationInfo authentication);
		INavigationResult Post(object postingObject, string url, AuthenticationInfo authenticationInfo);
	}
}
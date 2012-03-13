using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;

namespace HttpGhost.Navigation
{
	public interface INavigator
	{
		INavigationResult Get(string url, AuthenticationInfo authentication, string contentType = null);
		INavigationResult Post(object postingObject, string url, AuthenticationInfo authenticationInfo);
		INavigationResult Put(object postingObject, string url, AuthenticationInfo authenticationInfo);
		INavigationResult Delete(string url, AuthenticationInfo authenticationInfo);
	}
}
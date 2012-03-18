using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using HttpGhost.Navigation.Methods;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
	public class Navigator : INavigator
	{
	    private readonly IRequestFactory requestFactory;

	    public Navigator(IRequestFactory requestFactory)
	    {
	        this.requestFactory = requestFactory;
	    }

	    public INavigationResult Get(string url, AuthenticationInfo authenticationInfo, string contentType = null, object querystring = null)
		{
            var webRequest = requestFactory.Create(url);
            var options = new GetNavigationOptions(authenticationInfo, contentType, querystring);
            return new Get(webRequest, options).Navigate();
		}

		public INavigationResult Post(object postingObject, string url, AuthenticationInfo authenticationInfo)
		{
		    var webRequest = requestFactory.Create(url);
		    var options = new PostNavigationOptions(postingObject, authenticationInfo);
		    return new Post(webRequest, options).Navigate();
		}

		public INavigationResult Put(object postingObject, string url, AuthenticationInfo authenticationInfo)
		{
            var webRequest = requestFactory.Create(url);
            var options = new PutNavigationOptions(postingObject, authenticationInfo);
            return new Put(webRequest, options).Navigate();
		}

		public INavigationResult Delete(object postingObject, string url, AuthenticationInfo authenticationInfo)
		{
            var webRequest = requestFactory.Create(url);
            var options = new DeleteNavigationOptions(postingObject, authenticationInfo);
            return new Delete(webRequest, options).Navigate();
		}
	}
}
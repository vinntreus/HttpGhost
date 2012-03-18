using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Navigation.Methods;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
	public class Session
	{
	    private readonly IRequestFactory requestFactory;
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
		    requestFactory = new RequestFactory(new FormSerializer());
			AutoFollowRedirect = true;
		}

		public INavigationResult Get(string url, object querystring = null)
		{
            var webRequest = requestFactory.Create(url);
            var options = new GetNavigationOptions(Authentication, ContentType, querystring);
            return new Get(webRequest, options).Navigate();
		}

		public INavigationResult Post(object postingObject, string url)
		{
            var webRequest = requestFactory.Create(url);
            var options = new PostNavigationOptions(postingObject, Authentication);
            return new Post(webRequest, options).Navigate();
		}

		public INavigationResult Put(object postingObject, string url)
		{
            var webRequest = requestFactory.Create(url);
            var options = new PutNavigationOptions(postingObject, Authentication);
            return new Put(webRequest, options).Navigate();
		}

		public INavigationResult Delete(object postingObject, string url)
		{
            var webRequest = requestFactory.Create(url);
            var options = new DeleteNavigationOptions(postingObject, Authentication);
            return new Delete(webRequest, options).Navigate();
		}

        //private INavigationResult GetResult(Func<INavigationResult> navigate)
        //{
        //    var result = navigate();

        //    if (AutoFollowRedirect && result.Status == HttpStatusCode.Redirect)
        //    {
        //        var location = result.ResponseHeaders["Location"];
        //        if (!string.IsNullOrEmpty(location))
        //        {
        //            var uri = new Uri(result.RequestUrl);
        //            var urlToRedirectTo = string.Format("{0}://{1}{2}", uri.Scheme, uri.Host, location);

        //            return GetResult(() => navigator.Get(urlToRedirectTo, Authentication, ContentType));
        //        }
        //    }
        //    return result;
        //}
	}
}
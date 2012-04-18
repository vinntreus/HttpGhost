using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Navigation.Methods;
using HttpGhost.Transport;

namespace HttpGhost
{
	/// <summary>
	/// Main entry to use HttpGhost
	/// </summary>
	public class Session
	{
	    /// <summary>
	    /// Gets authentication type
	    /// </summary>
	    public AuthenticationInfo Authentication { get; private set; }

        /// <summary>
        /// Get/Set contenttype
        /// </summary>
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
		}

        /// <summary>
        /// Use Http-get to fetch data from url, querystring is optional
        /// </summary>
        /// <param name="url">An url</param>
        /// <param name="querystring">Use anonymous object for querystring</param>
        /// <returns></returns>
		public INavigationResult Get(string url, object querystring = null)
		{
		    var actualUrl = new UrlBuilder(url, querystring).Build();
            var webRequest = Request.Create(actualUrl);
            var options = new GetNavigationOptions(Authentication, ContentType);
            return new Get(webRequest, options).Navigate();
		}


		/// <summary>
		/// Http-post with data to url
		/// </summary>
		/// <param name="postingObject">Anonymous object for data</param>
		/// <param name="url">Url</param>
		/// <returns></returns>
		public INavigationResult Post(object postingObject, string url)
		{
            var webRequest = Request.Create(url);
            var options = new PostNavigationOptions(postingObject, Authentication, ContentType);
            return new Post(webRequest, options).Navigate();
		}

        /// <summary>
        /// Http-put with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
		public INavigationResult Put(object postingObject, string url)
		{
            var webRequest = Request.Create(url);
            var options = new PutNavigationOptions(postingObject, Authentication);
            return new Put(webRequest, options).Navigate();
		}

        /// <summary>
        /// Http-delete with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
		public INavigationResult Delete(object postingObject, string url)
		{
            var webRequest = Request.Create(url);
            var options = new DeleteNavigationOptions(postingObject, Authentication);
            return new Delete(webRequest, options).Navigate();
		}
	}
}
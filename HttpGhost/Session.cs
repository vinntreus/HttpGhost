using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Transport;
using HttpGhost.Serialization;
using System.Net;

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
            var nav = BuildNavigator(actualUrl);
            return nav.Get(); 
		}
       

		/// <summary>
		/// Http-post with data to url
		/// </summary>
		/// <param name="postingObject">Anonymous object for data</param>
		/// <param name="url">Url</param>
		/// <returns></returns>
		public INavigationResult Post(object postingObject, string url)
		{
            var nav = BuildNavigator(url, postingObject);
            return nav.Post();
		}        

        /// <summary>
        /// Http-put with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
		public INavigationResult Put(object postingObject, string url)
		{
            var nav = BuildNavigator(url, postingObject);
            return nav.Put();
		}

        /// <summary>
        /// Http-delete with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
		public INavigationResult Delete(object postingObject, string url)
		{
            var nav = BuildNavigator(url, postingObject);
            return nav.Delete();
		}

        private Navigator BuildNavigator(string url, object postingObject = null)
        {
            var request = new Request(url);
            request.SetAuthentication(Authentication);
            request.AddHeader(HttpRequestHeader.ContentType, ContentType);
            request.Body = new RequestBodySerializer().Serialize(postingObject ?? "", ContentType);
            return new Navigator(request);
        }
	}
}
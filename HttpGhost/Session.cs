using System;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;
using System.Net;

namespace HttpGhost
{
	/// <summary>
	/// Main entry to use HttpGhost
	/// </summary>
	public class Session
	{
        private readonly INavigate navigator;
	    private readonly ISerializeBuilder serializeBuilder;
	    private readonly IAuthenticate authentication;

		/// <summary>
		/// Uses Anonymous authentication
		/// </summary>
		public Session() : this(new AnonymousAuthentication()){}

		/// <summary>
		/// Uses Basic Authentication.
		/// </summary>
		public Session(string username, string password) : this(new BasicAuthentication(username, password)){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net webclient for requests and a default serializer
        /// </summary>
        /// <param name="authentication"></param>
		public Session(IAuthenticate authentication) : this(authentication, new WebRequestNavigator(), new DefaultSerializeBuilder()){}

	    /// <summary>
	    /// Uses provided authentication, navigation and serialize mechanism
	    /// </summary>
	    /// <param name="authentication"></param>
	    /// <param name="navigator"></param>
	    /// <param name="serializeBuilder"> </param>
	    public Session(IAuthenticate authentication, INavigate navigator, ISerializeBuilder serializeBuilder)
        {
            this.authentication = authentication;
            this.navigator = navigator;
            this.serializeBuilder = serializeBuilder;
        }

	    /// <summary>
	    /// Use Http-get to fetch data from url, querystring is optional
	    /// </summary>
	    /// <param name="url">An url</param>
	    /// <param name="querystring">Use anonymous object for querystring</param>
	    /// <param name="contentType">Contenttype</param>
	    /// <returns></returns>
	    public IHttpResult Get(string url, object querystring = null, string contentType = null)
		{
		    var actualUrl = new UrlBuilder(url, querystring).Build();
            var req = BuildRequest(actualUrl, HttpMethods.GET, null, contentType);
            return Navigate(req);
		}

	    /// <summary>
	    /// Http-post with data to url
	    /// </summary>
	    /// <param name="url">Url</param>
	    /// <param name="postingObject">Anonymous object for data</param>
	    /// <param name="contentType">Contenttype</param>
	    /// <returns></returns>
	    public IHttpResult Post(string url, object postingObject, string contentType = null)
		{
            var req = BuildRequest(url, HttpMethods.POST, postingObject, contentType);
            return Navigate(req);
		}

	    /// <summary>
	    /// Http-put with data to url
	    /// </summary>
	    /// <param name="url">Url</param>
	    /// <param name="postingObject">Anonymous object for data</param>
	    /// <param name="contentType">Contentype</param>
	    /// <returns></returns>
	    public IHttpResult Put(string url, object postingObject, string contentType = null)
		{
            var req = BuildRequest(url, HttpMethods.PUT, postingObject, contentType);
            return Navigate(req);
		}

	    /// <summary>
	    /// Http-delete with data to url
	    /// </summary>
	    /// <param name="url">Url</param>
	    /// <param name="postingObject">Anonymous object for data</param>
	    /// <param name="contentType"> </param>
	    /// <returns></returns>
	    public IHttpResult Delete(string url, object postingObject, string contentType = null)
		{
            var req = BuildRequest(url, HttpMethods.DELETE , postingObject, contentType);
            return Navigate(req);
		}

        private IRequest BuildRequest(string url, string method, object postingObject = null, string contentType = null)
        {
            var request = new Request(url);
            authentication.Process(request);
            if (!string.IsNullOrEmpty(contentType))
            {
                request.AddHeader(HttpRequestHeader.ContentType, contentType);
            }
            request.Body = serializeBuilder.BuildBy(contentType).Serialize(postingObject);
            request.Method = method;
            return request;
        }

        private IHttpResult Navigate(IRequest request)
        {
            return new HttpResult(navigator.Navigate(request))
            {
                OnFollow = url => Get(url),
                OnSubmitForm = (postingObject, action) =>
                {
                    var url = UrlByLink.Build(action, new Uri(request.Url));
                    return Post(url, postingObject, ContentType.X_WWW_FORM_URLENCODED);
                }
            };
        }
	}
}
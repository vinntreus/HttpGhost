using System;
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
        private readonly INavigate navigator;
        private readonly IAuthenticate authentication;

        public string ContentType { get; set; }

		/// <summary>
		/// Uses Anonymous authentication
		/// </summary>
		public Session() : this(new AnonymousAuthentication()){}

		/// <summary>
		/// Uses Basic Authentication.
		/// </summary>
		public Session(string username, string password) : this(new BasicAuthentication(username, password)){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net webclient for requests
        /// </summary>
        /// <param name="authentication"></param>
		public Session(IAuthenticate authentication) : this(authentication, new WebRequestNavigator()){}

        /// <summary>
        /// Uses provided authentication and navigation mechanism
        /// </summary>
        /// <param name="authentication"></param>
        /// <param name="navigator"></param>
        public Session(IAuthenticate authentication, INavigate navigator)
        {
            this.authentication = authentication;
            this.navigator = navigator;
        }

        /// <summary>
        /// Use Http-get to fetch data from url, querystring is optional
        /// </summary>
        /// <param name="url">An url</param>
        /// <param name="querystring">Use anonymous object for querystring</param>
        /// <returns></returns>
        public IHttpResult Get(string url, object querystring = null)
		{
		    var actualUrl = new UrlBuilder(url, querystring).Build();
            var req = BuildRequest(actualUrl, "GET");
            return Navigate(req);
		}       

		/// <summary>
		/// Http-post with data to url
		/// </summary>
		/// <param name="postingObject">Anonymous object for data</param>
		/// <param name="url">Url</param>
		/// <returns></returns>
        public IHttpResult Post(object postingObject, string url)
		{
            var req = BuildRequest(url, "POST", postingObject);
            return Navigate(req);
		}        

        /// <summary>
        /// Http-put with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public IHttpResult Put(object postingObject, string url)
		{
            var req = BuildRequest(url, "PUT", postingObject);
            return Navigate(req);
		}

        /// <summary>
        /// Http-delete with data to url
        /// </summary>
        /// <param name="postingObject">Anonymous object for data</param>
        /// <param name="url">Url</param>
        /// <returns></returns>
        public IHttpResult Delete(object postingObject, string url)
		{
            var req = BuildRequest(url, "DELETE", postingObject);
            return Navigate(req);
		}

        private IRequest BuildRequest(string url, string method, object postingObject = null)
        {
            var request = new Request(url);
            authentication.Process(request);
            if (!string.IsNullOrEmpty(ContentType))
            {
                request.AddHeader(HttpRequestHeader.ContentType, ContentType);
            }
            request.Body = BuildSerializerBy(ContentType).Serialize(postingObject);
            request.Method = method;
            return request;
        }

        private ISerializer BuildSerializerBy(string contentType)
        {
            switch (contentType)
            {
                case "application/json": return new JsonSerializer();
                default: return new FormSerializer();
            }
        }

        private IHttpResult Navigate(IRequest request)
        {
            return new HttpResult(navigator.Navigate(request))
            {
                OnFollow = url => Get(url)
            };
        }
	}
}
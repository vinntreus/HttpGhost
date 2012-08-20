using System.Net;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
    /// <summary>
    /// Main entry to use HttpGhost
    /// </summary>
    public abstract class Session<T>
    {
        protected readonly INavigate navigator;
        protected readonly ISerializeBuilder serializeBuilder;
        protected readonly IAuthenticate authentication;

        /// <summary>
        /// Uses Anonymous authentication
        /// </summary>
        protected Session() : this(new AnonymousAuthentication()){}

        /// <summary>
        /// Uses Basic Authentication.
        /// </summary>
        protected Session(string username, string password) : this(new BasicAuthentication(username, password)){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net classes for requests and a default serializer
        /// </summary>
        /// <param name="authentication"></param>
        protected Session(IAuthenticate authentication) : this(authentication, new WebRequestNavigator(), new DefaultSerializeBuilder()){}

        /// <summary>
        /// Uses provided authentication, navigation and serialize mechanism
        /// </summary>
        /// <param name="authentication"></param>
        /// <param name="navigator"></param>
        /// <param name="serializeBuilder"> </param>
        protected Session(IAuthenticate authentication, INavigate navigator, ISerializeBuilder serializeBuilder)
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
        public virtual T Get(string url, object querystring = null, string contentType = null)
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
        public T Post(string url, object postingObject, string contentType = null)
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
        public T Put(string url, object postingObject, string contentType = null)
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
        public T Delete(string url, object postingObject, string contentType = null)
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

        protected abstract T Navigate(IRequest request);
    }
}
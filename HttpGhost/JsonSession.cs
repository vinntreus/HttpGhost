using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class JsonSession : Session<IJsonResult>
    {
         /// <summary>
		/// Uses Anonymous authentication
		/// </summary>
		public JsonSession(){}

		/// <summary>
		/// Uses Basic Authentication.
		/// </summary>
		public JsonSession(string username, string password) : base(username, password){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net webclient for requests and a default serializer
        /// </summary>
        /// <param name="authentication"></param>
        public JsonSession(IAuthenticate authentication) : base(authentication){}

        public JsonSession(IAuthenticate authentication, INavigate navigator, ISerializeBuilder serializeBuilder) : base(authentication, navigator, serializeBuilder) { }

        /// <summary>
        /// Get with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="querystring"></param>
        /// <returns></returns>
        public IJsonResult Get(string url, object querystring = null)
        {
            return Get(url, querystring, ContentType.JSON);
        }

        /// <summary>
        /// Post with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Post(string url, object postingObject)
        {
            return Post(url, postingObject, ContentType.JSON);
        }

        /// <summary>
        /// Put with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Put(string url, object postingObject)
        {
            return Put(url, postingObject, ContentType.JSON);
        }

        /// <summary>
        /// Put with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Delete(string url, object postingObject)
        {
            return Delete(url, postingObject, ContentType.JSON);
        }

        protected override IJsonResult Navigate(IRequest request)
        {
            return new JsonResult(navigator.Navigate(request));
        }
    }
}
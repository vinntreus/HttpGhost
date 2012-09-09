using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class HttpSession : Session<IHttpResult>
    {
        /// <summary>
		/// Uses Anonymous authentication
		/// </summary>
		public HttpSession(){}

		/// <summary>
		/// Uses Basic Authentication.
		/// </summary>
		public HttpSession(string username, string password) : base(username, password){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net webclient for requests and a default serializer
        /// </summary>
        /// <param name="authentication"></param>
        public HttpSession(IAuthenticate authentication) : base(authentication){}

        public HttpSession(IAuthenticate authentication, INavigate navigator, ISerializeBuilder serializeBuilder) : base(authentication, navigator, serializeBuilder){}

        protected override IHttpResult Navigate(IRequest request)
        {
            return new HttpResult(navigator.Navigate(request));
        }
    }
}
using System;
using HttpGhost.Authentication;
using HttpGhost.Navigation;
using HttpGhost.Serialization;
using HttpGhost.Transport;

namespace HttpGhost
{
    public class HtmlSession : Session<IHtmlResult>
    {
         /// <summary>
		/// Uses Anonymous authentication
		/// </summary>
		public HtmlSession(){}

		/// <summary>
		/// Uses Basic Authentication.
		/// </summary>
		public HtmlSession(string username, string password) : base(username, password){}
        
        /// <summary>
        /// Uses provided authentication mechanism and navigator which uses .Net webclient for requests and a default serializer
        /// </summary>
        /// <param name="authentication"></param>
        public HtmlSession(IAuthenticate authentication) : base(authentication){}

        public HtmlSession(IAuthenticate authentication, INavigate navigator, ISerializeBuilder serializeBuilder) : base(authentication, navigator, serializeBuilder) { }

        protected override IHtmlResult Navigate(IRequest request)
        {
            return new HtmlResult(navigator.Navigate(request))
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
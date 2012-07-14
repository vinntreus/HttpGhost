using System;
using System.Net;
using System.Reflection;
using System.Text;
using HttpGhost.Authentication;

namespace HttpGhost.Transport
{
    public class Request : IRequest
    {
        private AuthenticationInfo authenticationInfo;
        public string Url { get; private set; }
        public string Body { get; set; }
        public WebHeaderCollection Headers { get; private set; }

        public Request(string url)
        {
            this.Url = url;
            this.Headers = new WebHeaderCollection();
        }

        public void AddHeader(HttpRequestHeader requestHeader, string value)
        {
            this.Headers.Add(requestHeader, value);
        }

        /// <summary>
        /// Anonymous or Basic, sets a Authorization-header for Basic-authentication
        /// </summary>
        /// <param name="authentication"></param>
        public void SetAuthentication(AuthenticationInfo authentication)
        {
            authenticationInfo = authentication;
            if (authentication.Type == AuthenticationType.Anonymous)
                return;

            var base64String = Convert.ToBase64String(new ASCIIEncoding().GetBytes(authentication.Credentials.UsernamePassword));
            Headers.Add("Authorization", "Basic " + base64String);
        }
    }    
}

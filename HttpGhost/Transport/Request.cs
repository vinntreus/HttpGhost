using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using HttpGhost.Authentication;

namespace HttpGhost.Transport
{
    public class Request : IRequest
    {        
        public string Url { get; private set; }
        public string Body { get; set; }
        public string Method { get; set; }
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
    }    
}

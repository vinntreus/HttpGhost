using System.Net;

namespace HttpGhost.Transport
{
    public class Request : IRequest
    {        
        public string Url { get; private set; }
        public string Body { get; set; }
        public string Method { get; set; }
        public WebHeaderCollection Headers { get; private set; }
        
        public Request(string url) : this(url, "GET"){}
        public Request(string url, string method): this(url, method, new WebHeaderCollection()){}
        public Request(string url, string method, WebHeaderCollection headers)
        {
            Url = url;
            Method = method;
            Headers = headers;
        }

        public void AddHeader(HttpRequestHeader requestHeader, string value)
        {
            Headers.Add(requestHeader, value);
        }

        public void AddHeader(string name, string value)
        {
            Headers.Add(name, value);
        }
    }    
}

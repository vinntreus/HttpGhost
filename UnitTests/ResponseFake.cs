using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Transport;

namespace UnitTests
{
    public class ResponseFake : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public string ContentType { get; set; }
    }
}
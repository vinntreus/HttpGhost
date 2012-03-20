using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Transport;

namespace UnitTests
{
    public class FakeResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Html { get; set; }
        public WebHeaderCollection Headers { get; set; }
    }
}
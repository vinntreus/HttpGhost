﻿using System.Net;

namespace HttpGhost.Transport
{
    public class Response : IResponse
    {
        public Response()
        {
            Headers = new WebHeaderCollection();
        }
        public HttpStatusCode StatusCode { get; set; }
        public WebHeaderCollection Headers { get; set; }        
        public string Body { get; set; }
    }
}

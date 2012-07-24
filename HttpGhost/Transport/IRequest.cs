using System;
using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using System.Net;

namespace HttpGhost.Transport
{
	public interface IRequest
	{
        WebHeaderCollection Headers { get; }

        void AddHeader(HttpRequestHeader requestHeader, string value);        

        /// <summary>
        /// Request body (JSON-string, Form-data etc)
        /// </summary>
        string Body { get; }
        
        /// <summary>
        /// Requesting url
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Requesting method like GET, PUT, POST or DELETE
        /// </summary>
        string Method { get; }
	}
}
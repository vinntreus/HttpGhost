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
        
        string Url { get; }

		void SetAuthentication(AuthenticationInfo authentication);	    
	}
}
using System;
using System.Net;
using System.Reflection;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    /// <summary>
    /// A wrapper for System.Net.WebClient
    /// </summary>
    public static class Client
    {
        /// <summary>
        /// Uses DownloadString in WebClient, use this if you want to do a GET-request.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static IResponse Fetch(IRequest req)
        {
            return WithWebClient(req, c => c.DownloadString(req.Url));
        }

        /// <summary>
        /// Uses UploadString in WebClient, use this if you want to do POST, PUT, DELETE. 
        /// Set IRequest.Body with content to send.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static IResponse Push(IRequest req)
        {            
            return WithWebClient(req, c => c.UploadString(req.Url, req.Method, req.Body));
        }

        private static IResponse WithWebClient(IRequest request, Func<WebClient, string> body)
        {
            var res = CreateResponse();
            using (var webclient = new WebClient())
            {
                webclient.Headers = request.Headers;
                res.Body = body(webclient);
                res.Headers = webclient.ResponseHeaders;
                res.StatusCode = GetStatusCode(webclient);
            }
            return res;
        }

        private static Response CreateResponse()
        {
            return new Response();
        }

        private static HttpStatusCode GetStatusCode(WebClient client)
        {
            var responseField = typeof(WebClient).GetField("m_WebResponse", BindingFlags.Instance | BindingFlags.NonPublic);

            var response = (HttpWebResponse)responseField.GetValue(client);

            return response.StatusCode;
        }
    }
}

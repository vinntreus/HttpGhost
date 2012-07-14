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
            return WithWebClient(req.Headers, c => c.DownloadString(req.Url));
        }

        /// <summary>
        /// Uses UploadString in WebClient, use this if you want to do POST, PUT, DELETE. 
        /// Set IRequest.Body with content to send.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="method">HttpMetod, eg. POST</param>
        /// <returns></returns>
        public static IResponse Push(IRequest req, string method)
        {            
            return WithWebClient(req.Headers, c => c.UploadString(req.Url, method, req.Body));
        }

        private static IResponse WithWebClient(WebHeaderCollection headers, Func<WebClient, string> getData)
        {
            var res = CreateResponse();
            using (var webclient = new WebClient())
            {
                webclient.Headers = headers;                
                res.Body = getData(webclient);
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
            FieldInfo responseField = client.GetType().GetField("m_WebResponse", BindingFlags.Instance | BindingFlags.NonPublic);

            var response = (HttpWebResponse)responseField.GetValue(client);

            return response.StatusCode;
        }
    }
}

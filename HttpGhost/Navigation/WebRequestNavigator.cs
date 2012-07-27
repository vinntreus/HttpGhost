using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
    public class WebRequestNavigator : INavigate
    {
        private readonly Stopwatch watch;

        public WebRequestNavigator()
        {
            watch = new Stopwatch();
        }

        public INavigationResult Navigate(IRequest request)
        {
            if (string.IsNullOrEmpty(request.Method))
            {
                throw new NavigationResultException("Request method is empty");
            }
            watch.Start();

            var httpWebRequest = HttpWebRequestBuilder.BuildFrom(request);
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            watch.Stop();

            var response = BuildResponse(httpWebResponse);

            if (IsRedirect(response.StatusCode))
            {
                var url = GetUrlForRedirect(request.Url, response);
                return Navigate(new Request(url, "GET", request.Headers));
            }

            watch.Reset();

            return new NavigationResult(request, response)
            {
                TimeSpent = watch.ElapsedMilliseconds
            };
        }

        private static string GetUrlForRedirect(string baseUrl, IResponse response)
        {
            return new Uri(new Uri(baseUrl), response.Headers["location"]).ToString();
        }

        private static bool IsRedirect(HttpStatusCode httpStatusCode)
        {
            var statusCode = (int) httpStatusCode;
            return statusCode.IsBetween(299, 400);
        }
        
        private static IResponse BuildResponse(HttpWebResponse httpWebResponse)
        {
            var responseStream = httpWebResponse.GetResponseStream();
            if (responseStream == null)
                return null;

            var response = new Response();
            using (var s = new StreamReader(responseStream))
            {
                response.Body = s.ReadToEnd();
            }
            foreach (var key in httpWebResponse.Headers.AllKeys)
            {
                response.Headers.Add(key, httpWebResponse.Headers.Get(key));
            }
            response.StatusCode = httpWebResponse.StatusCode;
            return response;
        }
    }

    public static class IntegerExtensions
    {
        public static bool IsBetween(this int i, int greaterThan, int lessThan)
        {
            return i > greaterThan && i < lessThan;
        }
    }


    public static class HttpWebRequestBuilder
    {
        public static HttpWebRequest BuildFrom(IRequest request)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);

            httpWebRequest.AddAllowedHeaders(request);
            httpWebRequest.Method = request.Method;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.ContentType = request.Headers[HttpRequestHeader.ContentType];
            httpWebRequest.Accept = request.Headers[HttpRequestHeader.Accept];
            httpWebRequest.Connection = request.Headers[HttpRequestHeader.Connection];
            httpWebRequest.Expect = request.Headers[HttpRequestHeader.Expect];
            httpWebRequest.Referer = request.Headers[HttpRequestHeader.Referer];
            httpWebRequest.SendChunked = request.Headers[HttpRequestHeader.TransferEncoding] != null;
            httpWebRequest.TransferEncoding = request.Headers[HttpRequestHeader.TransferEncoding];
            httpWebRequest.UserAgent = request.Headers[HttpRequestHeader.UserAgent];
            httpWebRequest.AddDateHeader(request);
            httpWebRequest.AddIfModifiedSinceHeader(request);
            httpWebRequest.AddBody(request.Body);

            return httpWebRequest;
        }
    }

    internal static class HttpWebRequestExtensions
    {
        public static void AddAllowedHeaders(this HttpWebRequest httpWebRequest, IRequest request)
        {
            foreach (var key in request.Headers.AllKeys.Except(RestrictedHeaders))
            {
                httpWebRequest.Headers.Add(key, request.Headers[key]);
            }
        }

        public static void AddDateHeader(this HttpWebRequest httpWebRequest, IRequest request)
        {
            if (request.Headers[HttpRequestHeader.Date] != null)
            {
                httpWebRequest.Date = DateTime.Parse(request.Headers[HttpRequestHeader.Date]);
            }
        }

        public static void AddIfModifiedSinceHeader(this HttpWebRequest httpWebRequest, IRequest request)
        {
            if (request.Headers[HttpRequestHeader.IfModifiedSince] != null)
            {
                httpWebRequest.IfModifiedSince = DateTime.Parse(request.Headers[HttpRequestHeader.IfModifiedSince]);
            }
        }

        public static void AddBody(this HttpWebRequest httpWebRequest, string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                var bytes = Encoding.ASCII.GetBytes(body);
                httpWebRequest.ContentLength = bytes.Length;
                using (var requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
        }

        private static readonly string[] RestrictedHeaders = new[]
        {
            "Content-Type", 
            "Accept",
            "Connection",
            "Date",
            "Expect",
            "If-Modified-Since",
            "Referer",
            "Transfer-Encoding",
            "User-Agent",
            
            //not implemented handling
            "Range",
            //these do not seem to actually set headers on webrequest (at least from unit-tests)
            "Content-Length",
            "Host"
        };
    }
}
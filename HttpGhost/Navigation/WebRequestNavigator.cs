using System;
using System.Diagnostics;
using System.Globalization;
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

            var httpWebRequest = BuildHttpWebRequest(request);
            
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            watch.Stop();

            var response = BuildResponse(httpWebResponse);
            if(IsRedirect(response.StatusCode))
            {
                var redirect = BuildRedirectRequest(request, response);
                return Navigate(redirect);
            }
            watch.Reset();
            return new NavigationResult(request, response)
            {
                TimeSpent = watch.ElapsedMilliseconds
            };
        }

        private static readonly string[] RestrictedHeaders = new[]
        {
            "Content-Type", 
            "Accept",
            "Connection",
            "Content-Length",
            "Date",
            "Expect",
            "Host"
        };

        public static HttpWebRequest BuildHttpWebRequest(IRequest request)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create(request.Url);
            foreach (var key in request.Headers.AllKeys.Except(RestrictedHeaders))
            {
                httpWebRequest.Headers.Add(key, request.Headers[key]);
            }
            httpWebRequest.Method = request.Method;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.ContentType = request.Headers[HttpRequestHeader.ContentType];
            httpWebRequest.Accept = request.Headers[HttpRequestHeader.Accept];
            httpWebRequest.Connection = request.Headers[HttpRequestHeader.Connection];
            httpWebRequest.Expect = request.Headers[HttpRequestHeader.Expect];
            
            if(request.Headers[HttpRequestHeader.Date] != null)
            {
                httpWebRequest.Date = DateTime.Parse(request.Headers[HttpRequestHeader.Date]);
            }

            if (!string.IsNullOrEmpty(request.Body))
            {
                var bytes = Encoding.ASCII.GetBytes(request.Body);
                httpWebRequest.ContentLength = bytes.Length;
                using (var requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }

            return httpWebRequest;
        }

        private static Request BuildRedirectRequest(IRequest request, IResponse response)
        {
            var uri = new Uri(new Uri(request.Url), response.Headers["location"]);
            var redirect = new Request(uri.ToString());
            foreach (var key in request.Headers.AllKeys)
            {
                redirect.AddHeader(key, request.Headers.Get(key));
            }
            redirect.Method = "GET";
            return redirect;
        }

        private static bool IsRedirect(HttpStatusCode statusCode)
        {
            return ((int)statusCode).ToString(CultureInfo.InvariantCulture).StartsWith("3");
        }

        private static IResponse BuildResponse(HttpWebResponse httpWebResponse)
        {
            var responseStream = httpWebResponse.GetResponseStream();
            if (responseStream == null)
                return null;

            var response = new Response();
            using(var s = new StreamReader(responseStream))
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
}
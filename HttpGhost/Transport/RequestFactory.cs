using System;
using System.Net;
using HttpGhost.Serialization;

namespace HttpGhost.Transport
{
    internal class RequestFactory : IRequestFactory
    {
        private readonly ISerializer serializer;

        public RequestFactory(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public IRequest Get(string url)
        {
            return new Request((HttpWebRequest)WebRequest.Create(new Uri(url)), serializer);
        }
    }
}
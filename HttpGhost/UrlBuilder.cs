using System;
using System.Linq;
using System.Collections.Generic;
using HttpGhost.Serialization;

namespace HttpGhost
{
    public class UrlBuilder
    {
        private readonly string url;
        private readonly object querystring;
        private readonly ISerializer serializer;

        public UrlBuilder(string url, object querystring)
        {
            if(string.IsNullOrEmpty(url))
                throw new ArgumentException("url");

            this.url = url;
            this.querystring = querystring;
            serializer = new FormSerializer();
        }

        public string Build()
        {
            if (querystring != null)
                return string.Format("{0}?{1}", url, serializer.Serialize(querystring));
            return url;
        }
    }
}
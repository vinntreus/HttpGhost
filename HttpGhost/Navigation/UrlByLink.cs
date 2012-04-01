using System;
using System.Linq;
using System.Collections.Generic;

namespace HttpGhost.Navigation
{
    internal class UrlByLink
    {
        private readonly string href;
        private readonly Uri uri;

        public UrlByLink(string href, Uri uri)
        {
            this.href = href;
            this.uri = uri;
        }

        public string Build()
        {
            if (href.StartsWith("http"))
                return href;

            return string.Format("{0}{1}{2}", uri.GetLeftPart(UriPartial.Scheme), uri.Authority, href);
        }
    }
}
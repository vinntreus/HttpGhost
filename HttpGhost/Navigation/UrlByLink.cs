using System;

namespace HttpGhost.Navigation
{
    internal static class UrlByLink
    {
        public static string Build(string href, Uri uri)
        {
            if (href.StartsWith("http"))
                return href;

            return string.Format("{0}{1}{2}", uri.GetLeftPart(UriPartial.Scheme), uri.Authority, href);
        }
    }
}
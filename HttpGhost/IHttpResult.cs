using System.Net;
using HttpGhost.Html;

namespace HttpGhost
{
    public interface IHttpResult
    {
        /// <summary>
        /// Time spent in milliseconds
        /// </summary>
        long TimeSpent { get; }

        /// <summary>
        /// Status from response
        /// </summary>
        HttpStatusCode Status { get; }

        /// <summary>
        /// Response content/body
        /// </summary>
        string ResponseContent { get; }

        /// <summary>
        /// Response headers
        /// </summary>
        WebHeaderCollection ResponseHeaders { get; }

        /// <summary>
        /// Absolute url from request
        /// </summary>
        string RequestUrl { get; }

        /// <summary>
        /// Find elements by css-selector or x-path
        /// </summary>
        /// <param name="selector">css-selector or x-path</param>
        /// <returns></returns>
        Elements Find(string selector);

        /// <summary>
        /// Converts ResponseContent (assuming it is JSON) to desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T FromJsonTo<T>();

        /// <summary>
        /// Finds element, uses it's href attribute value and makes a Get request
        /// </summary>
        /// <param name="selector">CSS or XPath selector to element with href attribute</param>
        /// <returns>Navigationresult</returns>
        IHttpResult Follow(string selector);

        Form FindForm(string form);
    }
}
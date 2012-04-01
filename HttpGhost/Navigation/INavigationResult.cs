using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace HttpGhost.Navigation
{
	public interface INavigationResult
	{
        /// <summary>
        /// Time spent in milliseconds
        /// </summary>
	    long TimeSpent { get; }
		HttpStatusCode Status { get; }
		string ResponseContent { get; }
		WebHeaderCollection ResponseHeaders { get; }
        
        /// <summary>
        /// Find elements by css-selector or x-path
        /// </summary>
        /// <param name="pattern">css-selector or x-path</param>
        /// <returns></returns>
	    IEnumerable<string> Find(string pattern);

        /// <summary>
        /// Converts ResponseContent (assuming it is JSON) to desired type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
	    T FromJsonTo<T>();

	    string RequestUrl { get; }

        /// <summary>
        /// Finds element, uses it's href attribute value and makes a Get request
        /// </summary>
        /// <param name="selector">CSS or XPath selector to element with href attribute</param>
        /// <returns>Navigationresult</returns>
	    INavigationResult Follow(string selector);
	}
}
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace HttpGhost.Navigation
{
	public interface INavigationResult
	{
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
	}
}
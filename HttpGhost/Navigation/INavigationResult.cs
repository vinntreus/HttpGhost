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
	    IEnumerable<string> Find();
	    T ToJson<T>();
	    string RequestUrl { get; }
	}
}
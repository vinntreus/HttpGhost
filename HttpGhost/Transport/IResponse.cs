using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace HttpGhost.Transport
{
	public interface IResponse
	{
		HttpStatusCode StatusCode { get; }
		string Html { get; }
		WebHeaderCollection Headers { get; }
	}
}
using System.Net;

namespace RestInspector.Transport
{
	public interface IResponse
	{
		HttpStatusCode StatusCode { get; }
		string Html { get; }
	}
}
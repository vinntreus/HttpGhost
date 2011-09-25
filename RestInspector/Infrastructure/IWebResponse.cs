using System.Net;

namespace RestInspector.Infrastructure
{
	public interface IWebResponse
	{
		HttpStatusCode StatusCode { get; }
		string Html { get; }
	}
}
using System.Net;

namespace WebTester.Infrastructure
{
	public interface IWebResponse
	{
		HttpStatusCode StatusCode { get; }
		string Html { get; }
	}
}
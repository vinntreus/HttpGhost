using System.Net;

namespace WebTester.Infrastructure
{
	public interface IWebRequest
	{
		IWebResponse GetResponse();
		ICredentials Credentials { get; set; }
		WebHeaderCollection Headers { get; set; }
	}
}
using System.Net;

namespace RestInspector.Infrastructure
{
	public interface IWebRequest
	{
		IWebResponse GetResponse();
		ICredentials Credentials { get; set; }
		WebHeaderCollection Headers { get; set; }
	}
}
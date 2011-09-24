using System.Net;

namespace WebTester.Infrastructure
{
	public class WrappedHttpWebRequest : IWebRequest
	{
		private readonly HttpWebRequest webRequest;

		public WrappedHttpWebRequest(HttpWebRequest webRequest)
		{
			this.webRequest = webRequest;
			webRequest.Headers = new WebHeaderCollection();
		}

		public IWebResponse GetResponse()
		{
			return new WrappedHttpWebResponse((HttpWebResponse) webRequest.GetResponse());
		}

		public ICredentials Credentials
		{
			get { return webRequest.Credentials; }
			set { webRequest.Credentials = value; }
		}

		public WebHeaderCollection Headers
		{
			get { return webRequest.Headers; }
			set { webRequest.Headers = value; }
		}
	}
}
using System.Net;
using System.Text;

namespace RestInspector.Infrastructure
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

		//Spiked -- cap and add tests
		public void ObjectToPost(object objectToPost)
		{
			webRequest.Method = "POST";
			webRequest.ContentType = "application/x-www-form-urlencoded";

			var data = new HttpSerializer(objectToPost);
			var bytes = Encoding.UTF8.GetBytes(data.Serialize());
			webRequest.ContentLength = bytes.Length;
			
			var dataStream = webRequest.GetRequestStream();
			dataStream.Write(bytes, 0, bytes.Length);
			dataStream.Close();
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
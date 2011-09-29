using System;
using System.IO;
using System.Net;

namespace RestInspector.Transport.Implementation
{
	public class Response : IResponse
	{
		private readonly HttpWebResponse response;

		public Response(HttpWebResponse response)
		{
			this.response = response;
		}

		public HttpStatusCode StatusCode
		{
			get { return response.StatusCode; }
		}

		private string html;
		public string Html
		{
			get
			{
				if (string.IsNullOrEmpty(html))
				{
					var responseStream = response.GetResponseStream();
					if (responseStream == null)
						throw new InvalidOperationException("Reponsestream is null");

					using (var sr = new StreamReader(responseStream))
					{
						html = sr.ReadToEnd();
					}
				}

				return html;
			}
		}
	}
}
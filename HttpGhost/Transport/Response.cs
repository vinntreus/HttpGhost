using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace HttpGhost.Transport
{
	public class Response : IResponse
	{
		private readonly HttpWebResponse response;
		private string html;

		public Response(HttpWebResponse response)
		{
			this.response = response;
		}

		public HttpStatusCode StatusCode
		{
			get { return response.StatusCode; }
		}

		public WebHeaderCollection Headers
		{
			get { return response.Headers; }
		}
		
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
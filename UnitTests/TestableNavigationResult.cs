using System.Linq;
using System.Collections.Generic;
using System.Net;
using HttpGhost.Navigation;

namespace UnitTests
{
	public class TestableNavigationResult : INavigationResult
	{
		public HttpStatusCode Status
		{
			get { return HttpStatusCode.OK;}
		}

		public string ResponseContent
		{
			get { throw new System.NotImplementedException(); }
		}
	

		public WebHeaderCollection ResponseHeaders
		{
			get { return new WebHeaderCollection(); }
		}


        public IEnumerable<string> Find()
        {
            throw new System.NotImplementedException();
        }

        public T ToJson<T>()
        {
            throw new System.NotImplementedException();
        }

        public string RequestUrl
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
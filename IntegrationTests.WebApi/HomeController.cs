using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace IntegrationTests.WebApi
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("Hello, AspNetWebApi!")
            };
        }

        public HomePostModel Post(HomePostModel model)
        {
            return new HomePostModel {Title = "jippi"};
        }

        public HomePostModel Put(HomePostModel model)
        {
            return new HomePostModel { Title = "jippi" };
        }
        
        public string Delete(string id)
        {
            return "jippi";
        }
    }

    public class HomePostModel
    {
        public string Title { get; set; }
    }
}
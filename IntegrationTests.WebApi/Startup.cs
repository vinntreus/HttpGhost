using System.Net.Http.Formatting;
using System.Web.Http;
using Gate.Adapters.AspNetWebApi;
using Owin;

namespace IntegrationTests.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            var config = new HttpConfiguration(new HttpRouteCollection("/"));
            config.Routes.MapHttpRoute(
                name: "Default", 
                routeTemplate: "{controller}/{id}", 
                defaults: new { controller = "Home", id = RouteParameter.Optional });

            builder.RunHttpServer(config);
        }
    }
}

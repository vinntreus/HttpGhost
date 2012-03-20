using System.Linq;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses;

namespace IntegrationTests.Nancy
{
    public class Home : NancyModule
    {
        public Home()
        {
            Get["/"] = _ => "Getting";
            Get["/get-querystring"] = parameters => Request.Query.q + "";
            Get["/redirect-to-home"] = _ => Response.AsRedirect("/", RedirectResponse.RedirectType.Permanent);
            Post["/"] = _ => "Posting";
            Put["/"] = _ => "Putting";
            Delete["/"] = _ => "Deleting";

        }
    }
}
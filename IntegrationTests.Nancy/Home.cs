using System.Linq;
using System.Collections.Generic;
using Nancy;

namespace IntegrationTests.Nancy
{
    public class Home : NancyModule
    {
        public Home()
        {
            Get["/"] = _ => "Getting";
            Get["/getqs"] = parameters => Request.Query.q + "";
            Post["/"] = _ => "Posting";
            Put["/"] = _ => "Putting";
            Delete["/"] = _ => "Deleting";
        }
    }
}
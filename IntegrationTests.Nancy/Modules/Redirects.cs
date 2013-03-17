using Nancy;
using Nancy.Responses;

namespace IntegrationTests.Nancy.Modules
{
    public class Redirects : NancyModule
    {
        public Redirects()
        {
            Get["/with-link"] = _ => "<a id='mylink' href='/follow'>follow</a><a id='mylink302' href='/with-link-302'>follow</a>";
            Get["/with-link-302"] = _ => Response.AsRedirect("/follow", RedirectResponse.RedirectType.Permanent);
            Get["/follow"] = _ => "Followed";
        }
    }
}
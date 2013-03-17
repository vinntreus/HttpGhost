using Nancy;
using Nancy.Security;
namespace IntegrationTests.Nancy
{
    public class BasicAuthentication : NancyModule
    {
        public BasicAuthentication()
        {
            this.RequiresAuthentication();
            
            Get["/basic"] = _ => "got it";
            Post["/basic"] = m => Request.Form["Title"].Value;
            Put["/basic"] = m => Request.Form["Title"].Value;
            Delete["/basic"] = m => Request.Form["Id"].Value;
            Post["/basic/redir"] = m => Response.AsRedirect("/basic");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.Security;
namespace IntegrationTests.Nancy
{
    public class BasicAuthentication : NancyModule
    {
        public BasicAuthentication()
        {
            this.RequiresAuthentication();
            
            Get["/basic"] = _ => { return "got it"; };
            Post["/basic"] = m => { return Request.Form["Title"].Value; };
            Put["/basic"] = m => { return Request.Form["Title"].Value; };
            Delete["/basic"] = m => { return Request.Form["Id"].Value; };
            Post["/basic/redir"] = m => {
                var auth = Request.Headers["Authorization"];
                return Response.AsRedirect("/basic"); 
            };
        }
    }
}

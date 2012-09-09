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


            Get["/with-link"] = _ => "<a id='mylink' href='/follow'>follow</a><a id='mylink302' href='/with-link-302'>follow</a>";
            Get["/with-link-302"] = _ => Response.AsRedirect("/follow", RedirectResponse.RedirectType.Permanent); ;
            Get["/follow"] = _ => "Followed";

            Get["/page-with-form"] = _ => "<form action='/form-submit' method='post' id='form'>" +
                                            "<input type='text' name='item' id='input1' value='' />"+
                                         "</form>";
            Post["/form-submit"] = _ =>
                {
                    return Request.Form["item"].Value;
                };

            Get["/json", c => c.Request.Headers.ContentType == "application/json"] = _ =>
                {
                    return new JsonResponse(new {A = "b"}, new DefaultJsonSerializer());
                };
            Post["/json", c => c.Request.Headers.ContentType == "application/json"] = _ =>
            {
                return new JsonResponse(new { A = "b" }, new DefaultJsonSerializer());
            };

            Put["/json", c => c.Request.Headers.ContentType == "application/json"] = _ =>
            {
                return new JsonResponse(new { A = "b" }, new DefaultJsonSerializer());
            };
            Delete["/json", c => c.Request.Headers.ContentType == "application/json"] = _ =>
            {
                return new JsonResponse(new { A = "b" }, new DefaultJsonSerializer());
            };
        }
    }
}
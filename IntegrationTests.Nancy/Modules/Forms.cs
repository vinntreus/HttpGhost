using Nancy;

namespace IntegrationTests.Nancy.Modules
{
    public class Forms : NancyModule
    {
        public Forms()
        {
            Get["/page-with-form"] = _ => "<form action='/form-submit' method='post' id='form'>" +
                                          "<input type='text' name='item' id='input1' value='' />" +
                                          "</form>";

            Post["/form-submit"] = _ => Request.Form["item"].Value;
        }
    }
}
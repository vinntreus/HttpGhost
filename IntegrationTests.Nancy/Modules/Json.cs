using Nancy;

namespace IntegrationTests.Nancy.Modules
{
    public class Json : NancyModule
    {
        public Json()
        {
            Get["/json"] = _ => new {A = "b"};
            Post["/json"] = _ => new { A = "b" };
            Put["/json"] = _ => new { A = "b" };
            Delete["/json"] = _ => new { A = "b" };
        }
    }
}
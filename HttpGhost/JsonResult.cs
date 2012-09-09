using HttpGhost.Navigation;
using HttpGhost.Serialization;

namespace HttpGhost
{
    public class JsonResult : HttpResult, IJsonResult
    {
        private readonly JsonSerializer serializer;

        public JsonResult(INavigationResult navigationResult) : base(navigationResult)
        {
            serializer = new JsonSerializer();
        }

        public T To<T>()
        {
            return serializer.Deserialize<T>(Response.Body);
        }
    }
}
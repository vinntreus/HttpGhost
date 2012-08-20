using HttpGhost.Navigation;
using HttpGhost.Serialization;

namespace HttpGhost
{
    public interface IJsonResult : IHttpResult
    {
        /// <summary>
        /// Converts the response body from JSON to T
        /// </summary>
        /// <typeparam name="T">Selected type</typeparam>
        /// <returns>T</returns>
        T To<T>();
    }

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
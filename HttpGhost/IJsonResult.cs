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
}
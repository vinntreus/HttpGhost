using HttpGhost.Transport;

namespace HttpGhost
{
    public class JsonSession : Session<IJsonResult>
    {
        /// <summary>
        /// Get with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="querystring"></param>
        /// <returns></returns>
        public IJsonResult Get(string url, object querystring = null)
        {
            return Get(url, querystring, ContentType.JSON);
        }

        /// <summary>
        /// Post with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Post(string url, object postingObject)
        {
            return Post(url, postingObject, ContentType.JSON);
        }

        /// <summary>
        /// Put with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Put(string url, object postingObject)
        {
            return Put(url, postingObject, ContentType.JSON);
        }

        /// <summary>
        /// Put with content-type = application/json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postingObject"></param>
        /// <returns></returns>
        public IJsonResult Delete(string url, object postingObject)
        {
            return Delete(url, postingObject, ContentType.JSON);
        }

        protected override IJsonResult Navigate(IRequest request)
        {
            return new JsonResult(navigator.Navigate(request));
        }
    }
}
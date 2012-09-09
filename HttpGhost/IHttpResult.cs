using HttpGhost.Transport;

namespace HttpGhost
{
    public interface IHttpResult
    {
        /// <summary>
        /// Time spent in milliseconds
        /// </summary>
        long TimeSpentInMs { get; }

        IRequest Request { get; }

        IResponse Response { get; }
    }
}
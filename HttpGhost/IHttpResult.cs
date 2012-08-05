using HttpGhost.Transport;

namespace HttpGhost
{
    public interface IHttpResult
    {
        /// <summary>
        /// Time spent in milliseconds
        /// </summary>
        long TimeSpent { get; }

        IRequest Request { get; }

        IResponse Response { get; }
    }

    
}
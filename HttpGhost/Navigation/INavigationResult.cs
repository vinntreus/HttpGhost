using HttpGhost.Transport;

namespace HttpGhost.Navigation
{
	/// <summary>
	/// Result from a successful navigation
	/// </summary>
	public interface INavigationResult
	{
        /// <summary>
        /// Time spent in milliseconds
        /// </summary>
	    long TimeSpent { get; }

	    IResponse Response { get; }

	    IRequest Request { get; }
	}
}
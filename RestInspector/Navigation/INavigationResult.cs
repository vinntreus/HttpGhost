using System.Net;

namespace RestInspector.Navigation
{
	public interface INavigationResult
	{
		HttpStatusCode Status { get; }
		string ResponseContent { get; }
		WebHeaderCollection ResponseHeaders { get; } 
	}
}
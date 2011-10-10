using System.Net;

namespace RestInspector.Navigation
{
	public interface INavigationResult
	{
		HttpStatusCode Status { get; }
		string ResponseString { get; }
		object AsContentTypeFormat { get; }
	}
}
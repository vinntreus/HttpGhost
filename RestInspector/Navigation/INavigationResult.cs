using System.Net;

namespace RestInspector.Navigation
{
	public interface INavigationResult
	{
		HttpStatusCode Status { get; }
		string Html { get; }
	}
}
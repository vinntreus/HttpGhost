using System.Net;

namespace WebTester.Navigation
{
	public interface INavigationResult
	{
		HttpStatusCode Status { get; }
		string Html { get; }
	}
}
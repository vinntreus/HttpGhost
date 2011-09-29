using RestInspector.Authentication;

namespace RestInspector.Transport
{
	public interface IRequest
	{
		IResponse GetResponse();
		
		void SetAuthentication(AuthenticationInfo authentication);
		void SetMethod(string method);
		void SetContentType(string contentType);
		void WriteFormDataToRequestStream(string formData);
	}
}
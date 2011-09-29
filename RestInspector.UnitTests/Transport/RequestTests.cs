using System;
using System.Net;
using System.Text;
using NUnit.Framework;
using RestInspector.Authentication;
using RestInspector.Transport.Implementation;

namespace RestInspector.UnitTests.Transport
{
	[TestFixture]
	public class RequestTests
	{
		private Request request;
		private HttpWebRequest httpWebRequest;
		private const string some_url = "http://a";

		[SetUp]
		public void Setup()
		{
			httpWebRequest = (HttpWebRequest)WebRequest.Create(some_url);
			request = new Request(httpWebRequest);
		}

		[Test]
		public void SetAuthenctication_WhenBasicAuthentication_SetCorrectHeaders()
		{
			var base64String = Convert.ToBase64String(new ASCIIEncoding().GetBytes("a:b"));

			request.SetAuthentication(new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b")));

			Assert.That(httpWebRequest.Headers.Count, Is.EqualTo(1));
			Assert.That(httpWebRequest.Headers.GetValues("Authorization").GetValue(0), Is.EqualTo("Basic " + base64String));
		}

		[Test]
		public void SetAuthenctication_WhenAnonymousAuthentication_SetNoHeaders()
		{
			request.SetAuthentication(new AuthenticationInfo(AuthenticationType.Anonymous, new Credentials("a", "b")));

			Assert.That(httpWebRequest.Headers.Count, Is.EqualTo(0));
		}

		[Test]
		public void SetAuthenctication_WhenBasicAuthentication_SetCredentialCache()
		{
			request.SetAuthentication(new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b")));
			var credential = httpWebRequest.Credentials.GetCredential(new Uri(some_url), "Basic");

			Assert.That(credential.UserName, Is.EqualTo("a"));
			Assert.That(credential.Password, Is.EqualTo("b"));
		}

		[Test]
		public void SetAuthenctication_WhenAnonymousAuthentication_SetNoCredentialCache()
		{
			request.SetAuthentication(new AuthenticationInfo(AuthenticationType.Anonymous, new Credentials("a", "b")));

			Assert.That(httpWebRequest.Credentials, Is.Null);
		}

		[TestCase("GET")]
		[TestCase("POST")]
		public void SetMethod_SetsMethod(string method)
		{
			request.SetMethod(method);

			Assert.That(httpWebRequest.Method, Is.EqualTo(method));
		}

		[TestCase("a")]
		[TestCase("b")]
		public void SetContentType_SetsContentType(string contentType)
		{
			request.SetContentType(contentType);

			Assert.That(httpWebRequest.ContentType, Is.EqualTo(contentType));
		}
	}
}
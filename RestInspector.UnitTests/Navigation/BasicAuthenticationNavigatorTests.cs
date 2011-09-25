using System;
using System.Text;
using NUnit.Framework;
using RestInspector.Authentication;
using RestInspector.Navigation.Implementation;

namespace RestInspector.UnitTests.Navigation
{
	[TestFixture]
	public class BasicAuthenticationNavigatorTests
	{
		[Test]
		public void Ctor_ArgumentNull_ThrowsArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new BasicAuthenticationNavigator(new Uri("http://a"), null));
		}

		[Test]
		public void Ctor_WithValidCredential_ShouldSetCredentialProperty()
		{
			var expectedCredentials = new Credentials("a", "b");
			var navigator = new TestableBasicAuthenticationNavigator(new Uri("http://a"), expectedCredentials);

			var actualCredentials = navigator.GetCredentials();

			Assert.That(actualCredentials, Is.SameAs(expectedCredentials));
			
		}

		[Test]
		public void Get_ShouldHaveBasicAuthenticationHeader()
		{
			var navigator = new TestableBasicAuthenticationNavigator(new Uri("http://a"), new Credentials("a", "b"));
			var base64String = Convert.ToBase64String(new ASCIIEncoding().GetBytes("a:b"));

			navigator.Get();

			Assert.That(navigator.SetHeaders.Count, Is.EqualTo(1));
			Assert.That(navigator.SetHeaders.GetValues("Authorization").GetValue(0), Is.EqualTo("Basic " + base64String));
		}

		[Test]
		public void Get_ShouldHaveCredentialCache()
		{
			var navigator = new TestableBasicAuthenticationNavigator(new Uri("http://a"), new Credentials("a", "b"));
			
			navigator.Get();

			var credential = navigator.requestMock.Object.Credentials.GetCredential(new Uri("http://a"), "Basic");
			Assert.That(credential.UserName, Is.EqualTo("a"));
			Assert.That(credential.Password, Is.EqualTo("b"));
		}

		
	}
}
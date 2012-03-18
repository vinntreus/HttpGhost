using System.Linq;
using System.Collections.Generic;
using HttpGhost;
using HttpGhost.Authentication;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class SessionTests
	{
		[Test]
		public void Ctor_ShouldSetAuthenticationTypeToAnonymous()
		{
			var session = new Session();
			
			Assert.That(session.Authentication.Type, Is.EqualTo(AuthenticationType.Anonymous));
		}

		[Test]
		public void Ctor_ShouldSetCredentialsToNull()
		{
			var session = new Session();

			Assert.That(session.Authentication.Credentials, Is.Null);
		}

		[Test]
		public void Ctor_WithUserNameAndPassWord_ShouldSetAuthenticationTypeToBasic()
		{
			var session = new Session("a", "b");

			Assert.That(session.Authentication.Type, Is.EqualTo(AuthenticationType.BasicAuthentication));
		}

		[Test]
		public void Ctor_WithUserNameAndPassWord_ShouldSetAuthenticationCredentials()
		{
			var session = new Session("a", "b");

			Assert.That(session.Authentication.Credentials.Username, Is.EqualTo("a"));
			Assert.That(session.Authentication.Credentials.Password, Is.EqualTo("b"));
		}
	}
}
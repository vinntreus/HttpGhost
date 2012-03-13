using System.Linq;
using System.Collections.Generic;
using HttpGhost.Authentication;
using NUnit.Framework;

namespace UnitTests.Authentication
{
	[TestFixture]
	public class CredentialsTests	
	{
		[TestCase("a", "b")]
		[TestCase("b", "a")]
		public void Credentials_SetsUsername(string username, string password)
		{
			var credentials = new Credentials(username, password);

			Assert.That(credentials.Username, Is.EqualTo(username));
		}

		[TestCase("a", "b")]
		[TestCase("b", "a")]
		public void Credentials_SetsPassword(string username, string password)
		{
			var credentials = new Credentials(username, password);

			Assert.That(credentials.Password, Is.EqualTo(password));
		}

		[Test]
		public void UserNamePassword_ShouldBeSeparatedWithColon()
		{
			var credentials = new Credentials("a", "b");

			Assert.That(credentials.UsernamePassword, Is.EqualTo("a:b"));
		}


		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void Credentials_InvalidUsername_ThrowException(string username)
		{
			Assert.Throws<InvalidUserCredentialsException>(() => new Credentials(username, "a"), "not valid username");
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void Credentials_InvalidPassword_ThrowException(string password)
		{
			Assert.Throws<InvalidUserCredentialsException>(() => new Credentials("a", password), "not valid password");
		}
	}
}

using NUnit.Framework;
using RestInspector.Authentication;

namespace RestInspector.UnitTests
{
	[TestFixture]
	public class CredentialsTests	
	{
		//TODO:handle basic authentication
		//TODO:handle no autentication

		[Test]
		public void Credentials_UsernameAndPasswordToConstructor_DoNotThrow()
		{
			var credentials = new Credentials("a", "b");
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

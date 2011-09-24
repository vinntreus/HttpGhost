using System;
using Moq;
using NUnit.Framework;
using WebTester.Authentication;
using WebTester.Navigation;
using WebTester.Navigation.Implementation;

namespace WebTester.UnitTests
{
	[TestFixture]
	public class SessionTests
	{
		private const string SOME_URL = "http://a";

		//string parsing, find data in html result

		[Test]
		public void Ctor_ShouldSetAuthenticationTypeToAnonymous()
		{
			var session = new Session(SOME_URL);
			
			Assert.That(session.Authentication, Is.EqualTo(AuthenticationType.Anonymous));
		}

		[Test]
		public void Ctor_ShouldSetCredentialsToNull()
		{
			var session = new Session(SOME_URL);

			Assert.That(session.Credentials, Is.Null);
		}

		[Test]
		public void As_WithValidUserNameAndPassword_SetsAuthenticationTypeToBasic()
		{
			var session = new Session(SOME_URL).As("a", "b");

			Assert.That(session.Authentication, Is.EqualTo(AuthenticationType.BasicAuthentication));
		}

		[Test]
		public void As_WithValidUserNameAndPassword_SetsCredentials()
		{
			var session = new Session(SOME_URL).As("a", "b");

			Assert.That(session.Credentials.Username, Is.EqualTo("a"));
			Assert.That(session.Credentials.Password, Is.EqualTo("b"));
		}

		[Test]
		public void WithAuthentication_IsAlreadyBasicSetToAnonymous_ShouldReturnAnonymous()
		{
			var session = new Session(SOME_URL).As("a", "b").WithAuthentication(AuthenticationType.Anonymous);

			Assert.That(session.Authentication, Is.EqualTo(AuthenticationType.Anonymous));
			
		}

		[Test]
		public void Get_WhenAuthenticationTypeIsAnonymous_ShouldGetStandardNavigator()
		{
			var session = new TestableSession(SOME_URL);
			var factory = new Mock<INavigatorFactory>();
			var navigator = new Mock<INavigator>();
			factory.Setup(f => f.Create(It.IsAny<AuthenticationType>(), It.IsAny<Credentials>(), It.IsAny<Uri>())).Returns(navigator.Object);
			session.SetupNavigatorFactory(factory);
			
			session.Get();

			factory.Verify(f => f.Create(It.IsAny<AuthenticationType>(), It.IsAny<Credentials>(), It.IsAny<Uri>()));
		}

		private class TestableSession : Session
		{
			public TestableSession(string url) : base(url)
			{
				
			}

			public void SetupNavigatorFactory(Mock<INavigatorFactory> factory)
			{
				navigatorFactory = factory.Object;
			}
		}
	}
}
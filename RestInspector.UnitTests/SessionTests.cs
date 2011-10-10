using Moq;
using NUnit.Framework;
using RestInspector.Authentication;
using RestInspector.Navigation;

namespace RestInspector.UnitTests
{
	[TestFixture]
	public class SessionTests
	{
		private const string SOME_URL = "http://a";

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

		[Test]
		public void Get_ShouldReturnResultFromNavigator()
		{
			var session = new TestableSession();
			var expectedResult = new TestableNavigationResult();
			session.SetupGetToReturn(expectedResult);

			var result = session.Get(SOME_URL);

			Assert.That(result, Is.SameAs(expectedResult));
		}

		[Test]
		public void Post_ShouldReturnResultFromNavigator()
		{
			var session = new TestableSession();
			var expectedResult = new TestableNavigationResult();
			session.SetupPostToReturn(expectedResult);

			var result = session.Post("a=a", SOME_URL);

			Assert.That(result, Is.SameAs(expectedResult));
		}

		[Test]
		public void Put_ShouldReturnResultFromNavigator()
		{
			var session = new TestableSession();
			var expectedResult = new TestableNavigationResult();
			session.SetupPutToReturn(expectedResult);

			var result = session.Put("a=a", SOME_URL);

			Assert.That(result, Is.SameAs(expectedResult));
		}

		[Test]
		public void Delete_ShouldReturnResultFromNavigator()
		{
			var session = new TestableSession();
			var expectedResult = new TestableNavigationResult();
			session.SetupDeleteToReturn(expectedResult);

			var result = session.Delete(SOME_URL);

			Assert.That(result, Is.SameAs(expectedResult));
		}

		private class TestableSession : Session
		{
			private readonly Mock<INavigator> navigatorMock = new Mock<INavigator>();

			public TestableSession()
			{
				navigatorMock = new Mock<INavigator>();
				navigator = navigatorMock.Object;
			}

			public void SetupPostToReturn(INavigationResult navigationResult)
			{
				navigatorMock.Setup(n => n.Post(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<AuthenticationInfo>())).Returns(navigationResult);
			}

			public void SetupGetToReturn(INavigationResult navigationResult)
			{
				navigatorMock.Setup(n => n.Get(It.IsAny<string>(), It.IsAny<AuthenticationInfo>(), null)).Returns(navigationResult);
			}

			public void SetupPutToReturn(INavigationResult navigationResult)
			{
				navigatorMock.Setup(n => n.Put(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<AuthenticationInfo>())).Returns(navigationResult);
			}

			public void SetupDeleteToReturn(INavigationResult navigationResult)
			{
				navigatorMock.Setup(n => n.Delete(It.IsAny<string>(), It.IsAny<AuthenticationInfo>())).Returns(navigationResult);
			}

			
		}
	}
}
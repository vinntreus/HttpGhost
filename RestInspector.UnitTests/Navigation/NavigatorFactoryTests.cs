using System;
using NUnit.Framework;
using RestInspector.Authentication;
using RestInspector.Navigation.Implementation;

namespace RestInspector.UnitTests.Navigation
{
	[TestFixture]
	public class NavigatorFactoryTests
	{
		[Test]
		public void Create_AnonymousAuthentication_ReturnNavigator()
		{
			var navigatorFactory = new NavigatorFactory();
			var result = navigatorFactory.Create(AuthenticationType.Anonymous, new Credentials("a", "b"), new Uri("http://a"));

			Assert.That(result, Is.TypeOf<Navigator>());
		}

		[Test]
		public void Create_BasicAuthentication_ReturnBasicAuthenticationNavigator()
		{
			var navigatorFactory = new NavigatorFactory();
			var result = navigatorFactory.Create(AuthenticationType.BasicAuthentication, new Credentials("a", "b"), new Uri("http://a"));

			Assert.That(result, Is.TypeOf<BasicAuthenticationNavigator>());
		}
	}
}
using System;
using System.Text;
using HttpGhost.Authentication;
using HttpGhost.Transport;
using NUnit.Framework;

namespace UnitTests.Authentication
{
    [TestFixture]
    public class BasicAuthenticationTests
    {
        private Request request;

        [SetUp]
        public void Setup()
        {
            request = new Request("a");            
        }

        [Test]
        public void ProcessRequest_WithCredentials_SetCorrectHeaders()
        {
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes("a:b"));
            var basicAuth = new BasicAuthentication("a", "b");

            basicAuth.Process(request);
            
            Assert.That(request.Headers.GetValues("Authorization").GetValue(0), Is.EqualTo("Basic " + base64String));
        }       

        //[Test]
        //public void SetAuthenctication_WhenBasicAuthentication_SetCredentialCache()
        //{
        //    request.SetAuthentication(new AuthenticationInfo(AuthenticationType.BasicAuthentication, new Credentials("a", "b")));
        //    var credential = httpWebRequest.Credentials.GetCredential(new Uri(SomeURL), "Basic");

        //    Assert.That(credential.UserName, Is.EqualTo("a"));
        //    Assert.That(credential.Password, Is.EqualTo("b"));
        //}        
    }
}

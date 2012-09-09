using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
    public class HtmlSessionTests : IntegrationTestsBase
    {
        protected readonly string BaseUrl = "http://127.0.0.1:8080";
        private HtmlSession session;

        [SetUp]
        public void Setup()
        {
            session = new HtmlSession();
        }

        [Test]
        public void Session_GetAndFollow_ReturnsHtml()
        {
            var url = BaseUrl + "/with-link";
            var firstResult = session.Get(url);
            var secondResult = firstResult.Follow("#mylink");

            Assert.That(secondResult.Request.Url, Is.StringEnding("follow"));
            Assert.That(secondResult.Response.Body, Is.EqualTo("Followed"));
        }

        [Test]
        public void Session_GetAndFollow302_ReturnsHtml()
        {
            var url = BaseUrl + "/with-link";
            var firstResult = session.Get(url);
            var secondResult = firstResult.Follow("#mylink302");

            Assert.That(secondResult.Request.Url, Is.StringEnding("follow"));
            Assert.That(secondResult.Response.Body, Is.EqualTo("Followed"));
        }

        [Test]
        public void Session_SubmitForm_ReturnsResult()
        {
            var url = BaseUrl + "/page-with-form";
            var form = session.Get(url).FindForm("#form");
            var expectedRequestUrl = form.GetAttribute("action");
            form.SetValue("#input1", "value");
            var result = form.Submit();

            Assert.That(result.Request.Url, Is.StringEnding(expectedRequestUrl));
            Assert.That(result.Response.Body, Is.StringContaining("value"));
        }
    }
}
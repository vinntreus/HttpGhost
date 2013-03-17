using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
    [TestFixture]
    [Category("Integration")]
    public class JsonSessionTests : IntegrationTestsBase
    {
        private JsonSession session;

        [SetUp]
        public void Setup()
        {
            session = new JsonSession();
        }

        [Test]
        public void Get_HeadersIsDefaultApplicationJson_CanConvertResultToObject()
        {
            var result = session.Get(BaseUrl + "/json"); //this url demands header application/json and will return { "a" : "b" }

            var obj = result.To<TestClass>();

            Assert.That(obj.A, Is.EqualTo("b"));
        }

        [Test]
        public void Post_HeadersIsDefaultApplicationJson_CanConvertResultToObject()
        {
            var result = session.Post(BaseUrl + "/json", new {a = "a"}); //this url demands header application/json and will return { "a" : "b" }

            var obj = result.To<TestClass>();

            Assert.That(obj.A, Is.EqualTo("b"));
        }

        [Test]
        public void Put_HeadersIsDefaultApplicationJson_CanConvertResultToObject()
        {
            var result = session.Put(BaseUrl + "/json", new { a = "a" }); //this url demands header application/json and will return { "a" : "b" }

            var obj = result.To<TestClass>();

            Assert.That(obj.A, Is.EqualTo("b"));
        }

        [Test]
        public void Delete_HeadersIsDefaultApplicationJson_CanConvertResultToObject()
        {
            var result = session.Delete(BaseUrl + "/json", new { a = "a" }); //this url demands header application/json and will return { "a" : "b" }

            var obj = result.To<TestClass>();

            Assert.That(obj.A, Is.EqualTo("b"));
        }

        private class TestClass
        {
            public string A { get; set; }
        }
    }
}
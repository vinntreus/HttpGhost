using IntegrationTests.Nancy;
using IntegrationTests.Nancy.Modules;
using NUnit.Framework;

namespace IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        private Host host;

        /// <summary>
        /// http://127.0.0.1:1234, found in IntegrationTests.Nancy project
        /// </summary>
        protected readonly string BaseUrl = "http://127.0.0.1:1234";

        [TestFixtureSetUp]
        public void BeforeAllTests()
        {
            host = new Host();
            host.Start();
        }

        [TestFixtureTearDown]
        public void AfterAllTests()
        {
            host.Stop();
        }
    }
}
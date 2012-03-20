using System.Linq;
using System.Collections.Generic;
using IntegrationTests.Nancy;
using NUnit.Framework;

namespace IntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        private Host host;

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
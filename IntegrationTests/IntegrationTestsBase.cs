using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Firefly.Http;
using Gate.Builder;
using HttpGhost;
using NUnit.Framework;

namespace IntegrationTests
{
	public abstract class IntegrationTestsBase
	{
		protected Stopwatch stopwatch;
		protected string currentTest;
	    private IDisposable server;

	    private static Stopwatch StartStopwatch()
		{
			return Stopwatch.StartNew();
		}

        [TestFixtureSetUp]
        public void BeforeAllTests()
        {
            var builder = new AppBuilder();
            var application = new WebApi.Startup();
            var app = builder.Build(application.Configuration);
            server = new ServerFactory(new StdoutTrace()).Create(app, 8080);
        }

        [TestFixtureTearDown]
        public void AfterAllTests()
        {
            server.Dispose();
        }

		[SetUp]
		public virtual void Setup()
		{
			stopwatch = StartStopwatch();
		}

		[TearDown]
		public void TearDown()
		{
			Console.WriteLine(String.Format("Finished in {0} ms", stopwatch.ElapsedMilliseconds));
		}
	}
}
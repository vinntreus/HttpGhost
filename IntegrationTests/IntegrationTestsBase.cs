using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using NUnit.Framework;

namespace IntegrationTests
{
	public abstract class IntegrationTestsBase
	{
		protected Stopwatch stopwatch;
		protected string currentTest;

		private static Stopwatch StartStopwatch()
		{
			return Stopwatch.StartNew();
		}

		[SetUp]
		public void Setup()
		{
			stopwatch = StartStopwatch();
		}

		[TearDown]
		public void TearDown()
		{
			Console.WriteLine(String.Format("Finished {0} in {1} ms", currentTest, stopwatch.ElapsedMilliseconds));
		}
	}
}
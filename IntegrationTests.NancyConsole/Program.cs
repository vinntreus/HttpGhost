using System;
using IntegrationTests.Nancy;

namespace IntegrationTests.NancyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new Host();
            host.Start();
            Console.WriteLine("Host up at http://localhost:1234, press any key to quit");
            Console.ReadLine();
            host.Stop();
        }
    }
}

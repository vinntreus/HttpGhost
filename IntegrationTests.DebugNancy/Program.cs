using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntegrationTests.Nancy;

namespace IntegrationTests.DebugNancy
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new Host();
            host.Start();
            Console.WriteLine("Host up at http://127.0.0.1:8080, press any key to quit");
            Console.ReadLine();
            host.Stop();
        }
    }
}

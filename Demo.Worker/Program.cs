using System;
using Microsoft.Extensions.Logging;

namespace Demo.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var startUp = new Startup();
            var logger = startUp.GetService<ILogger<Program>>();
            logger.LogInformation("Hello World.");
            Console.Read();
        }
    }
}

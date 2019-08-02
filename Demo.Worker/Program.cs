using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Demo.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var startUp = new Startup();

            using (var scope = startUp.Scope)
            {
                var logger = startUp.Scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Hello World.");
                Console.Read();
            }
        }
    }
}

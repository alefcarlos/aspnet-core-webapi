using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Demo.Consumer.HandleMessages
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

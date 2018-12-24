using System;
using Framework.MessageBroker.RabbitMQ;
using Framework.Test;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.MessageBroker.Tests
{
    public class Startup : TestStartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitBroker("testeapp", false);
        }
    }
}
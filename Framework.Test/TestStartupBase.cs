using dotenv.net;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Framework.Test
{
    public abstract class TestStartupBase
    {

        public TestStartupBase()
        {
            if (File.Exists(".env"))
                DotEnv.Config();

        }


        public abstract void ConfigureServices(IServiceCollection services);
    }
}
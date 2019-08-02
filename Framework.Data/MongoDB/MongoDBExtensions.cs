using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Framework.Data.MongoDB
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoUri = configuration.GetConnectionString("MongoDB");

            // MongoClient (Singleton)
            var mongoUrl = new MongoUrl(mongoUri);
            var mongoConnection = new MongoDBConnectionWraper
            {
                MongoURL = mongoUrl,
                MongoClient = new MongoClient(mongoUrl)
            };

            services.AddSingleton(mongoConnection);

            services.AddHealthChecks()
                .AddMongoDb(mongoUri, "mongodb", failureStatus: null, tags: new string[] { "db", "mongodb" });

            return services;
        }
    }
}
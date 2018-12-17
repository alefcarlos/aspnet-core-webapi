using Framework.Helpers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Framework.Data.MongoDB
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            var mongoUri = CommonHelpers.GetValueFromEnv<string>("MONGO_URI");

            // MongoClient (Singleton)
            var mongoUrl = new MongoUrl(mongoUri);
            var mongoConnection = new MongoDBConnectionWraper
            {
                MongoURL = mongoUrl,
                MongoClient = new MongoClient(mongoUrl)
            };

            services.AddSingleton(mongoConnection);

            services.AddHealthChecks()
                .AddMongoDb(mongoUri, "mongodb", tags: new string[] { "db", "mongodb" });

            return services;
        }
    }
}

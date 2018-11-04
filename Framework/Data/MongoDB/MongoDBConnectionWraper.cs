using MongoDB.Driver;

namespace Framework.Data.MongoDB
{
    public class MongoDBConnectionWraper
    {
        public MongoClient MongoClient { get; set; }

        public MongoUrl MongoURL { get; set; }
    }
}

using MongoDB.Driver;

namespace Uneed_Mongo_API.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase db;
        public MongoDBRepository()
        {
            client= new MongoClient("mongodb://uneed-mongo:27017");
            db = client.GetDatabase("Category");
        }
    }
}

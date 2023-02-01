using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Uneed_Mongo_API.Models
{
    public class ServCategory
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        

    }
}

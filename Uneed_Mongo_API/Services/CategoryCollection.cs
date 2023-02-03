using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using Uneed_Mongo_API.Models;
using Uneed_Mongo_API.Repositories;

namespace Uneed_Mongo_API.Services
{
    public class CategoryCollection : ICategoryCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<ServCategory> _collection;
        public CategoryCollection()
        {
            _collection = _repository.db.GetCollection<ServCategory>("Categories");
        }
        public async Task DeleteCategory(string id)
        {
            var filter = Builders<ServCategory>.Filter.Eq(s => s.Id, new ObjectId(id));
                await _collection.DeleteOneAsync(filter);
            
        }

        public async Task<IEnumerable<ServCategory>> GetAllCategories()
        {
            return await _collection.FindAsync(new BsonDocument()).Result.ToListAsync();
            
        }

        public async Task<ServCategory> GetCategoryById(string id)
        {
            return await _collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
            
        }

        public async Task InsertCategory(ServCategory servCategory)
        {
            await _collection.InsertOneAsync(servCategory);
            
        }

        public async Task UpdateCategory(ServCategory servCategory)
        {
            var filter = Builders<ServCategory>
                .Filter
                .Eq(s => s.Id, servCategory.Id);
            await _collection.ReplaceOneAsync(filter, servCategory);
        }
    }
}

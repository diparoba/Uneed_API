using Uneed_Mongo_API.Models;

namespace Uneed_Mongo_API.Services
{
    public interface ICategoryCollection
    {
        Task InsertCategory(ServCategory servCategory);
        Task UpdateCategory(ServCategory servCategory);
        Task DeleteCategory(string id);
        Task<IEnumerable<ServCategory>> GetAllCategories();
        Task<ServCategory> GetCategoryById(string id);

        
    }
}

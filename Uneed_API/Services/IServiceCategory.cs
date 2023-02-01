using Uneed_API.Models;

namespace Uneed_API.Services
{
    public interface IServiceCategory
    {
        Task<IEnumerable<ServCategory>> GetCategories();
    }
}

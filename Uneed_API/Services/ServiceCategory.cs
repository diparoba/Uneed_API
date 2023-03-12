using Microsoft.EntityFrameworkCore;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceCategory : IServiceCategory
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceCategory(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                var result = await _dataContext.Category.Where(data => data.Status.Equals("A")).ToListAsync();
                return (IEnumerable<Category>)result;
            }
            catch
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}

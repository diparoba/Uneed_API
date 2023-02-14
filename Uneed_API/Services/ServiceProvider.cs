using Microsoft.EntityFrameworkCore;
using Uneed_API.DTO;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceProvider(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;

        }
        public async Task<IEnumerable<ProviderResponse>> GetProviders()
        {
            try
            {
                List<ProviderResponse> result = await _dataContext.Provider.Include(u => u.User).Select
                            (u => new ProviderResponse()
                            {
                                Id = u.Id,
                                UserName = u.User.Name,
                                UserLastname = u.User.Lastname,
                                Status = u.Status,
                                Identification = u.User.Identification,
                                Phone = u.User.Phone,
                                Adress = u.User.Adress,
                                ServName = u.ServName,
                                Description = u.Description,
                                CategoryId = u.Category.Id,
                                UserId = u.UserId,
                                CategoryName = u.Category.ServiceName
                            }).ToListAsync();
                return (IEnumerable<ProviderResponse>)result;
            }
            catch
            {
                return null;
            }
        }

        public Task<IEnumerable<ProviderResponse>> GetProvidersByCategory(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProviderResponse>> GetProvidersByUserName(string userName)
        {
            throw new NotImplementedException();
        }
        public Task<ProviderResponse> GetProviderByProviderName(string providerName)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> SaveProvider(Provider provider)
        {
            try
            {
                provider.Status = "A";
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
        public Task<bool> UpdateProvider(int idProvider, Provider provider)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProvider(int idProvider)
        {
            throw new NotImplementedException();
        }
    }
}
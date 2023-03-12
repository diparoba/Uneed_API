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
        public async Task<IEnumerable<ProviderResponse>> GetAll()
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

        public async Task<IEnumerable<ProviderResponse>> GetByCategory(string categoryName)
        {
            try
            {
                List<ProviderResponse> result = await _dataContext.Provider.Include(p => p.Category)
                    .Where(p => p.Status.Equals("A") && p.Category.ServiceName.Equals(categoryName))
                    .Select(p => new ProviderResponse()
                    {
                        Id = p.Id,
                        ServName = p.ServName,
                        UserLastname = p.User.Lastname,
                        Description = p.Description,
                        UserName = p.User.Name,
                        UserEmail = p.User.Email,
                        Status = p.Status,
                        Phone = p.User.Phone,
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.ServiceName
                    })
                    .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProviderResponse>> GetByUserName(string userName)
        {
            try
            {
                List<ProviderResponse> result = await _dataContext.Provider.Include(p => p.Category).Include(p => p.User)
                    .Where(p => p.Status.Equals("A") && p.User.Name.Equals(userName))
                    .Select(p => new ProviderResponse()
                    {
                        Id = p.Id,
                        ServName = p.ServName,
                        UserLastname = p.User.Lastname,
                        Description = p.Description,
                        UserName = p.User.Name,
                        UserEmail = p.User.Email,
                        Status = p.Status,
                        Phone = p.User.Phone,

                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.ServiceName
                    })
                    .ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProviderResponse> GetByProviderName(string providerName)
        {
            try
            {
                ProviderResponse result = await _dataContext.Provider.Include(p => p.Category).Include(p => p.User)
                    .Where(p => p.Status.Equals("A") && p.ServName.Equals(providerName))
                    .Select(p => new ProviderResponse()
                    {
                        Id = p.Id,
                        ServName = p.ServName,
                        UserLastname = p.User.Lastname,
                        Description = p.Description,
                        UserName = p.User.Name,
                        UserEmail = p.User.Email,
                        Status = p.Status,
                        Phone = p.User.Phone,

                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.ServiceName
                    })
                    .FirstOrDefaultAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Save(Provider provider)
        {
            try
            {
                provider.Status = "A";
                provider.User.IsProvider = true;
                _dataContext.Provider.Add(provider);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Update(int idProvider, Provider provider)
        {
            try
            {
                var providerToUpdate = await _dataContext.Provider.Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == idProvider);

                if (providerToUpdate != null)
                {
                    // Actualizar la información del proveedor
                    providerToUpdate.ServName = provider.ServName;
                    providerToUpdate.Description = provider.Description;
                    providerToUpdate.CategoryId = provider.CategoryId;

                    // Actualizar la información del usuario asociado al proveedor
                    providerToUpdate.User.Name = provider.User.Name;
                    providerToUpdate.User.Lastname = provider.User.Lastname;
                    providerToUpdate.User.Email = provider.User.Email;
                    providerToUpdate.User.Phone = provider.User.Phone;


                    // Cambiar el estado a "A" (activo)
                    providerToUpdate.Status = "A";

                    // Cambiar la propiedad IsProvider del usuario a true
                    providerToUpdate.User.IsProvider = true;

                    _dataContext.Provider.Update(providerToUpdate);
                    await _dataContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int idProvider)
        {
            try
            {
                var provider = await _dataContext.Provider.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == idProvider);

                if (provider == null || provider.Status.Equals("I")) return false;

                provider.Status = "I";
                provider.User.IsProvider = false;

                _dataContext.Entry(provider).State = EntityState.Modified;

                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Restore(int idProvider)
        {
            try
            {
                var provider = await _dataContext.Provider.FindAsync(idProvider);
                if (provider == null)
                {
                    return false;
                }

                provider.Status = "A";
                provider.User.IsProvider = true;
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
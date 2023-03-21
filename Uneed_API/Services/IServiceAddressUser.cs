using Uneed_API.Models;

namespace Uneed_API.Services
{
    public interface IServiceAddressUser
    {
        Task<IEnumerable<Address>> GetByUserId(int userId);
        Task<IEnumerable<AddressUser>> GetAll();
        Task <bool> save(Models.AddressUser addressUser);
        
    }
}
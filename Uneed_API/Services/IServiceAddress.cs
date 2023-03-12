using Uneed_API.Models;

namespace Uneed_API.Services
{
    public interface IServiceAddress
    {
        Task<IEnumerable<Address>> GetAll();
        Task<Models.Address> GetById(int id);
        Task<IEnumerable<Address>> GetByUser(int userId);
        Task<bool> Save(Models.Address address);
        Task<bool> Update(int idAddress, Models.Address address);
    }
}
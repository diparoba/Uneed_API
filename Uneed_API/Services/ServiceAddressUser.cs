using Microsoft.EntityFrameworkCore;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceAddressUser : IServiceAddressUser
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceAddressUser(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }

        public async Task<IEnumerable<AddressUser>> GetAll()
        {
            return (await _dataContext.AddressUser.ToListAsync()).AsEnumerable();
        }



        public async Task<IEnumerable<Address>> GetByUserId(int userId)
        {
            var addresses = await _dataContext.Address
                .Include(a => a.AddressUser)
                .Where(a => a.AddressUser.Any(au => au.UserId == userId))
                .ToListAsync();

            return addresses;
        }





        public async Task<bool> save(AddressUser addressUser)
        {
            try
            {
                await _dataContext.AddressUser.AddAsync(addressUser);
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
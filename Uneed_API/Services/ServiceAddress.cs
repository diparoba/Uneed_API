using Microsoft.EntityFrameworkCore;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceAddress : IServiceAddress
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceAddress(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<IEnumerable<Address>> GetAll()
        {
            try
            {
                List<Address> result = await _dataContext.Address.ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Address> GetById(int id)
        {
            try
            {
                Address address = await _dataContext.Address.FindAsync(id);
                return address;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Address>> GetByUser(int userId)
        {
            var addresses = await _dataContext.Address
                .Include(a => a.AddressUser)
                .ThenInclude(ua => ua.User)
                .Where(a => a.AddressUser.Any(ua => ua.UserId == userId))
                .ToListAsync();

            return addresses;
        }


        public async Task<bool> Save(Address address)
        {
            try
            {
                _dataContext.Address.Add(address);
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(int idAddress, Address address)
        {
            try
            {
                var addressInfo = await GetById(idAddress);
                if (addressInfo == null)
                {
                    return false;
                }

                addressInfo.PrincipalStreet = address.PrincipalStreet;
                addressInfo.SecondaryStreet = address.SecondaryStreet;
                addressInfo.City = address.City;

                _dataContext.Entry(addressInfo).State = EntityState.Modified;
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
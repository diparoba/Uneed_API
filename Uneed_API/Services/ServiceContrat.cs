using Microsoft.EntityFrameworkCore;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceContrat : IServiceContrat
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceContrat(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        public async Task<bool> ContratService(int providerId, int userId, DateTime dayDate, string direction, string finish, decimal price)
        {
            try
            {
                var provider = await _dataContext.Provider.FindAsync(providerId);
                var user = await _dataContext.User.FindAsync(userId);

                if (provider == null || user == null)
                {
                    return false;
                }

                var contratService = new ContratService()
                {
                    DayDate = dayDate,
                    CreateDate = DateTime.Now,
                    Direction = direction,
                    Finish = finish,
                    Price = price,
                    User = user,
                    Provider = provider
                };

                _dataContext.ContratService.Add(contratService);
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
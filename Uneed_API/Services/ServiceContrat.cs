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

        public async Task<bool> AcceptContractByProvider(int providerId, int contratServiceId)
        {
            try
            {
                var contratService = await _dataContext.ContratService.FindAsync(contratServiceId);

                if (contratService == null)
                    return false;

                if (contratService.ProviderId != providerId)
                    return false;

                contratService.State = "A";
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CancelContractByProvider(int providerId, int contratServiceId)
        {
            try
            {
                var contrat = await _dataContext.ContratService.FindAsync(contratServiceId);

                if (contrat == null)
                    return false;

                if (contrat.ProviderId != providerId)
                    return false;

                if (contrat.State == "F")
                    return false;

                contrat.State = "C";
                _dataContext.ContratService.Update(contrat);
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> CancelContractByUser(int userId, int contratServiceId)
        {
            try
            {
                var contratService = await _dataContext.ContratService.FindAsync(contratServiceId);

                if (contratService == null)
                    return false;

                if (contratService.User.Id != userId)
                    return false;

                if (contratService.State != "P")
                    return false;

                contratService.State = "C";

                _dataContext.ContratService.Update(contratService);
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> FinishContractByUser(int userId, int contratServiceId)
        {
            try
            {
                var contratService = await _dataContext.ContratService
                    .Include(c => c.Provider)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == contratServiceId && c.User.Id == userId);

                if (contratService == null)
                {
                    return false;
                }

                contratService.State = "F"; // Set state to Finished
                contratService.Finish = DateTime.UtcNow;

                // Save changes
                _dataContext.ContratService.Update(contratService);
                return await _dataContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<ContratService>> GetContratsByProviderId(int proveedorId)
        {
            var contrats = await _dataContext.ContratService
                                              .Include(c => c.User)
                                              .Include(c => c.Provider)
                                              .Include(c => c.Calification)
                                              .Where(c => c.Provider.Id == proveedorId)
                                              .ToListAsync();
            return contrats;
        }

        public async Task<IEnumerable<ContratService>> GetContratsByUserId(int userId)
        {
            var contracts = await _dataContext.ContratService
                .Include(c => c.User)
                .Include(c => c.Provider)
                .Include(c => c.Calification)
                .Include(c => c.AddressUser)
                    .ThenInclude(a => a.Address)
                .Where(c => c.User.Id == userId)
                .ToListAsync();

            return contracts;
        }


        public async Task<bool> RateProviderByUser(int userId, int contratServiceId, int calificationValue, string comment)
        {
            try
            {
                var contratService = await _dataContext.ContratService
                    .Include(c => c.Provider)
                    .FirstOrDefaultAsync(c => c.Id == contratServiceId && c.User.Id == userId);

                if (contratService == null)
                    return false;

                contratService.Calification ??= new List<Calification>();
                contratService.Calification.Add(new Calification
                {
                    Value = calificationValue,
                    Comment = comment,
                    User = contratService.User,
                    Date = DateTime.UtcNow
                });

                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<ContratService> RequestContract(int userId, int providerId, DateTime dayDate, decimal price, int addressId)
        {
            // Obtener los usuarios involucrados en el contrato
            var user = await _dataContext.User.FindAsync(userId);
            var provider = await _dataContext.Provider.FindAsync(providerId);

            // Verificar que ambos usuarios existan
            if (user == null || provider == null)
            {
                throw new ArgumentException("User or provider does not exist");
            }

            // Obtener la dirección del usuario
            var addressUser = await _dataContext.AddressUser
                                                .Include(au => au.Address)
                                                .FirstOrDefaultAsync(au => au.UserId == userId && au.AddressId == addressId);

            if (addressUser == null)
            {
                throw new ArgumentException("The user address does not exist");
            }

            // Crear el contrato
            var contratService = new ContratService
            {
                DayDate = dayDate,
                CreateDate = DateTime.UtcNow,
                AddressUser = addressUser,
                State = "p",
                Price = price,
                User = user,
                Provider = provider
            };

            _dataContext.ContratService.Add(contratService);
            await _dataContext.SaveChangesAsync();

            return contratService;
        }

    }
}
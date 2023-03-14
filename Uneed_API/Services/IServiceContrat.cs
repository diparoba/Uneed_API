using System.Collections.Generic;
using System.Threading.Tasks;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public interface IServiceContrat
    {
        Task<ContratService> RequestContract(int userId, int providerId, DateTime dayDate, decimal price, int addressId);
        Task<bool> CancelContractByUser(int userId, int contratServiceId);
        Task<bool> CancelContractByProvider(int providerId, int contratServiceId);
        Task<bool> AcceptContractByProvider(int providerId, int contratServiceId);
        Task<bool> FinishContractByUser(int userId, int contratServiceId);
        Task<bool> RateProviderByUser(int userId, int contratServiceId, int calificationValue, string comment);
        Task<IEnumerable<ContratService>> GetContratsByUserId(int userId);
        Task<IEnumerable<ContratService>> GetContratsByProviderId(int proveedorId);
        Task<ContratService> GetById(int contratId);
    }
}

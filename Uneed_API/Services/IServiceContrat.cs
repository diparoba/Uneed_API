namespace Uneed_API.Services
{
    public interface IServiceContrat
    {
        Task<bool> ContratService(int providerId, int userId, DateTime dayDate, string direction, string finish, decimal price);
    }
}

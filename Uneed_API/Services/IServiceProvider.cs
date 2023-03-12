namespace Uneed_API.Services
{
    public interface IServiceProvider
    {
        Task<IEnumerable<DTO.ProviderResponse>> GetAll();
        Task<IEnumerable<DTO.ProviderResponse>> GetByCategory(string categoryName);
        Task<IEnumerable<DTO.ProviderResponse>> GetByUserName(string userName);
        Task<DTO.ProviderResponse> GetByProviderName(string providerName);
        Task<bool> Save(Models.Provider provider);
        Task<bool> Update(int idProvider, Models.Provider provider);
        Task<bool> Delete(int idProvider);
        Task<bool> Restore(int idProvider);
    }
}
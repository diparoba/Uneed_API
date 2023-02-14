namespace Uneed_API.Services
{
    public interface IServiceProvider
    {
        Task<IEnumerable<DTO.ProviderResponse>> GetProviders();
        Task<IEnumerable<DTO.ProviderResponse>> GetProvidersByCategory(string categoryName);
        Task<IEnumerable<DTO.ProviderResponse>> GetProvidersByUserName(string userName);
        Task<DTO.ProviderResponse> GetProviderByProviderName(string providerName);
        Task<bool> SaveProvider(Models.Provider provider);
        Task<bool> UpdateProvider(int idProvider, Models.Provider provider);
        Task<bool> DeleteProvider(int idProvider);
    }
}
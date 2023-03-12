namespace Uneed_API.Services
{
    public interface IServiceUser
    {
        Task<IEnumerable<DTO.UserResponse>> GetAll();
        Task<Models.User> GetByEmail(string email);
        Task<Models.User> GetById(int id);
        Task<bool> Save(Models.User user);
        Task<bool> Update(int idUser, Models.User user);
        Task<bool> Delete(int id);
        Task<bool> ChangePassword(string UserName, string CurrentPassword, string NewPassword, string ConfirmPassword);
        Task<bool> ChangeProviderToTrue(int id);

    }
}

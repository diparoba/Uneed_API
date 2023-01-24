namespace Uneed_API.Services
{
    public interface IServiceUser
    {
        Task<IEnumerable<DTO.UserResponse>> GetUsers();
        Task<Models.User> GetUserByEmail(string email);
        Task<Models.User> GetUserById(int id);
        Task<bool> SaveUser(Models.User user);
        Task<bool> UpdateUser(int idUser, Models.User user);
        Task<bool> DeleteUser(int id);
        Task<bool> ChangePassword(string UserName, string CurrentPassword, string NewPassword, string ConfirmPassword);

    }
}

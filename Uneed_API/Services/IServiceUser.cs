namespace Uneed_API.Services
{
    public interface IServiceUser
    {
        Task<IEnumerable<Models.User>> GetUsers();
        Task<Models.User> GetUserByEmail(string email);
        Task<Models.User> GetUserById(int id);
        Task<bool> SaveUser(Models.User user);
        Task<bool> UpdateUser(int idUser, Models.User user);
        Task<bool> DeleteUser(int id);
        //Task<bool> ChangePassword(string oldPassword, string newPassword);

    }
}

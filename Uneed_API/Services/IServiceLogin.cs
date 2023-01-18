namespace Uneed_API.Services
{
    public interface IServiceLogin
    {
        Task<Models.User> Login(Models.Login user);
        object generateToken(Models.User user);
    }
}

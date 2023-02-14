namespace Uneed_API.Services
{
    public interface IServiceLogin
    {
        Task<Models.User> Login(Models.Auth user);
        object generateToken(Models.User user);
    }
}

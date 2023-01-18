using Microsoft.EntityFrameworkCore;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceUser(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;

        }
       

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var result = await _dataContext.User.Where(data => data.Status.Equals("A")
                                                                    && data.Email.Equals(email)).FirstOrDefaultAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                User result = await _dataContext.User.Where(data => data.Status.Equals("A")
                                                                    && data.Id.Equals(id)).FirstOrDefaultAsync();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                List<User> result = await _dataContext.User.Where(data => data.Status.Equals("A")).ToListAsync();
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> SaveUser(User user)
        {
            try
            {
                var infoUser = await GetUserByEmail(user.Email);
                if (infoUser != null)
                {
                    return false;
                }
                user.CreatedDate = DateTime.Now;
                //user.UpdateDate = DateTime.Now;
                user.Status = "A";
                _dataContext.User.Add(user);
                return await _dataContext.SaveChangesAsync() > 0;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUser(int idUser, User user)
        {
            try
            {
                var userInfo = await GetUserById(idUser);
                if (userInfo == null)
                {
                    return false;
                }
                userInfo.UpdateDate = DateTime.Now;
                userInfo.Email = user.Email;
                userInfo.RolId = user.RolId;
                userInfo.Lastname = user.Lastname;
                userInfo.Name = user.Name;
                userInfo.Phone = user.Phone;
                userInfo.Password = user.Password;

                _dataContext.Entry(userInfo).State = EntityState.Modified;
                return await _dataContext.SaveChangesAsync() > 0;

            }

            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userInfo = await GetUserById(id);
                if (userInfo == null)
                {
                    return false;
                }
                userInfo.Status = "I";
                _dataContext.Entry(userInfo).State = EntityState.Modified;
                return await _dataContext.SaveChangesAsync() > 0;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

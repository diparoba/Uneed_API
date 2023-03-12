using Microsoft.EntityFrameworkCore;
using Uneed_API.DTO;
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


        public async Task<User> GetByEmail(string email)
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

        public async Task<User> GetById(int id)
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

        public async Task<IEnumerable<UserResponse>> GetAll()
        {
            try
            {
                //List<User> result = await _dataContext.User.Where(data => data.Status.Equals("A")).ToListAsync();
                List<UserResponse> result = await _dataContext.User.Include(u => u.Rol).Select
                    (u => new UserResponse()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Lastname = u.Lastname,
                        Email = u.Email,
                        Status = u.Status,
                        Password = u.Password,
                        Identification = u.Identification,
                        Gender = u.Gender,
                        BirthDate = u.BirthDate,
                        Phone = u.Phone,
                        RolId = u.RolId,
                        RolName = u.Rol.Description
                    }).ToListAsync();
                return (IEnumerable<UserResponse>)result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> Save(User user)
        {
            try
            {
                var infoUser = await GetByEmail(user.Email);
                if (infoUser != null)
                {
                    return false;
                }
                user.CreatedDate = DateTime.Now;
                user.Status = "A";
                user.RolId = 2;
                _dataContext.User.Add(user);
                return await _dataContext.SaveChangesAsync() > 0;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(int id, User user)
        {
            var existingUser = await GetById(id);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Lastname = user.Lastname;
                existingUser.Status = user.Status;
                existingUser.Email = user.Email;
                existingUser.Password = user.Password;
                existingUser.Identification = user.Identification;
                existingUser.Phone = user.Phone;
                existingUser.IsProvider = user.IsProvider;
                existingUser.Gender = user.Gender;
                existingUser.BirthDate = user.BirthDate;
                existingUser.RolId = user.RolId;
                existingUser.UpdateDate = DateTime.Now;

                _dataContext.User.Update(existingUser);
                await _dataContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var userInfo = await GetById(id);
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

        public Task<bool> ChangePassword(string UserName, string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}

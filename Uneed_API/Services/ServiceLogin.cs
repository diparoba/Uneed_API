using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Uneed_API.Models;

namespace Uneed_API.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly DataContext _dataContext;
        private readonly IConfiguration _configuration;
        public ServiceLogin(DataContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }



        public async Task<User> Login(Auth user)
        {
            var userInfo = await authUser(user.UserName, user.Password);
            if (userInfo == null)
            {
                return null;
            }
            else
            {
                return userInfo;
            }
        }
        private async Task<User> authUser(string username, string password)
        {
            try
            {
                var userInfo = await _dataContext.User.Where(data => data.Email.Equals(username)
                                                        && data.Password.Equals(password)
                                                        && data.Status.Equals("A")).FirstOrDefaultAsync();
                return userInfo;
            }
            catch
            {
                return null;
            }


        }
        public object generateToken(User user)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:Password"])
                );
            var _signingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );
            var _header = new JwtHeader(_signingCredentials);
            //Claims
            var _claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("lastname", user.Lastname),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };
            //Payload
            var _payLoad = new JwtPayload(
                issuer: _configuration["JWT:Dominio"],
                audience: _configuration["JWT:appApi"],
                claims: _claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60)
                );
            //Token
            var _token = new JwtSecurityToken(_header, _payLoad);
            return new JwtSecurityTokenHandler().WriteToken(_token);
        }
    }
}

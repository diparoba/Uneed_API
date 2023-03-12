using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("All")]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _serviceLogin;
        private readonly IServiceUser _serviceUser;
        public LoginController(IServiceLogin serviceLogin, IServiceUser serviceUser)
        {
            _serviceLogin = serviceLogin;
            _serviceUser = serviceUser;
        }
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> Login(Models.Auth login)
        {
            var user = await _serviceLogin.Login(login);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                token = _serviceLogin.generateToken(user),
                userId = user.Id,
                userEmail = user.Email,
                userName = user.Name,
            });
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("renew")]
        public async Task<ActionResult> RenewToken()
        {
            var userId = AuthHelper.GetUserId(HttpContext);

            var user = await _serviceUser.GetById(userId);
            if (user != null)
            {
                return Ok(new
                {
                    token = _serviceLogin.generateToken(user),
                    userId = user.Id,
                    userEmail = user.Email,
                    userName = user.Name,
                });
            }
            else
            {
                return Unauthorized();
            }

        }

    }

}

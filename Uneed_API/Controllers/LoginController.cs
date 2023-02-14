using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [EnableCors("All")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _serviceLogin;
        public LoginController(IServiceLogin serviceLogin)
        {
            _serviceLogin = serviceLogin;
        }
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult> Login(Models.Auth login)
        {
            var user = await _serviceLogin.Login(login);
            if(user == null)
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
       
    }
       
    }

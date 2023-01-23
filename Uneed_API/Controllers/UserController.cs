using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uneed_API.DTO;
using Uneed_API.Models;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {
            
            return Ok(await _serviceUser.GetUsers());
        }
        [HttpPost]
        public async Task<ActionResult> Save(DTO.UserResponse user)
        {
            Models.User userData = new Models.User
            {
                Id= user.Id,
                Name = user.Name,
                Lastname= user.Lastname,
                Email= user.Email,
                Status= user.Status,
                Password = user.Password,
                Phone= user.Phone,
                RolId = user.RolId
            };
            var result = await _serviceUser.SaveUser(userData);
            return Ok(
                new
                {
                    result = result
                });
        }
        [HttpPut]
        public async Task<ActionResult> Update(DTO.UserResponse user)
        {
            Models.User userData = new Models.User
            {
                Id= user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                Status = user.Status,
                Password = user.Password,
                Phone = user.Phone,
                RolId = user.RolId
            };
            var result = await _serviceUser.UpdateUser(user.Id, userData);
            return Ok(
                new
                {
                    result = result
                });
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(DTO.UserResponse user)
        {
            var result = await _serviceUser.DeleteUser(user.Id);
            return Ok(
                new
                {
                    result = result
                });
        }

    }
}

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uneed_API.DTO;
using Uneed_API.Models;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [EnableCors("All")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;
        private readonly IServiceAddress _serviceAddress;
        private readonly Services.IServiceProvider _serviceProvider;
        public UserController(IServiceUser serviceUser, IServiceAddress serviceAddress, Services.IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceUser = serviceUser;
            _serviceAddress = serviceAddress;
        }



        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {

            return Ok(await _serviceUser.GetAll());
        }
        [HttpPost]
        public async Task<ActionResult> Save(DTO.UserResponse user)
        {
            Models.User userData = new Models.User
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                Status = user.Status,
                Password = user.Password,
                Identification = user.Identification,
                Phone = user.Phone,
                RolId = user.RolId,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
            };
            var result = await _serviceUser.Save(userData);
            return Ok(
                new
                {
                    result = result
                });
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Update(DTO.UserResponse user)
        {
            Models.User userData = new Models.User
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                Email = user.Email,
                Status = user.Status,
                Password = user.Password,
                Phone = user.Phone,
            };
            var result = await _serviceUser.Update(user.Id, userData);
            return Ok(
                new
                {
                    result = result
                });
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(DTO.UserResponse user)
        {
            var result = await _serviceUser.Delete(user.Id);
            return Ok(
                new
                {
                    result = result
                });
        }
        [HttpPost("address")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AddAddress(AddressResponse addressResponse)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);

                var address = new Address
                {
                    Name = addressResponse.Name,
                    PrincipalStreet = addressResponse.PrincipalStreet,
                    SecondaryStreet = addressResponse.SecondaryStreet,
                    City = addressResponse.City,
                    AddressUser = new List<AddressUser> { new AddressUser { UserId = userId } }
                };


                var result = await _serviceAddress.Save(address);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("provider")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RegisterAsProvider(ProviderResponse providerResponse)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);

                var provider = new Provider
                {
                    ServName = providerResponse.ServName,
                    Description = providerResponse.Description,
                    UserId = userId,
                    CategoryId = providerResponse.CategoryId,
                    Status = "P"
                };

                var result = await _serviceProvider.Save(provider);

                if (result)
                {
                    return Ok(new { message = "Registrado correctamente como proveedor" });
                }
                else
                {
                    return BadRequest("No se pudo registrar como proveedor");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

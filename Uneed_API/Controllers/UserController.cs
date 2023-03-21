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
using System.Linq;


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
        private readonly IServiceContrat _serviceContrat;
        private readonly IServiceAddressUser _serviceAddressUser;
        public UserController(IServiceUser serviceUser, IServiceAddress serviceAddress, Services.IServiceProvider serviceProvider, IServiceContrat serviceContrat, IServiceAddressUser serviceAddressUser)
        {
            _serviceProvider = serviceProvider;
            _serviceUser = serviceUser;
            _serviceAddress = serviceAddress;
            _serviceContrat = serviceContrat;
            _serviceAddressUser = serviceAddressUser;

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
        [HttpGet("addresses")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<AddressUserResponse>>> GetByUserId()
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);
                var addresses = await _serviceAddress.GetByUser(userId);

                if (addresses == null)
                {
                    return NotFound("No addresses found for the user.");
                }

                var response = addresses.Select(address => new AddressUserResponse
                {
                    AddressId = address.Id,
                    AddressName = address.Name,
                    PrincipalStreet = address.PrincipalStreet,
                    SecondaryStreet = address.SecondaryStreet,
                    UserId = userId,
                    UserName = address.AddressUser.First(au => au.UserId == userId).User.Name,
                    Lastname = address.AddressUser.First(au => au.UserId == userId).User.Lastname
                });

                return Ok(response);
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
                    Status = "A"
                };

                var result = await _serviceProvider.Save(provider);

                if (result)
                {
                    // Cambiar IsProvider a true
                    await _serviceUser.ChangeProviderToTrue(userId);

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





        [HttpPost("contract")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RequestContract(ContratResponse contratRequest)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);

                var contratService = await _serviceContrat.RequestContract(
                    userId,
                    contratRequest.ProviderId,
                    contratRequest.DayDate,
                    contratRequest.Price,
                    contratRequest.AddressId
                    );

                if (contratService == null)
                {
                    return BadRequest("Error al solicitar el contrato.");
                }

                return Ok(new { contratServiceId = contratService.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("contracts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<ContratResponse>>> GetContratsByUserId()
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);

                var contrats = await _serviceContrat.GetContratsByUserId(userId);

                var contratsResponse = contrats.Select(c => new ContratResponse
                {
                    Id = c.Id,
                    UserId = c.User.Id,
                    ProviderId = c.Provider.Id,
                    DayDate = c.DayDate,
                    Price = c.Price,
                    State = c.State,
                    AddressId = c.AddressUser.Address.Id,
                    AddressPrincipalStreet = c.AddressUser.Address.PrincipalStreet,
                    AddressSecondaryStreet = c.AddressUser.Address.SecondaryStreet,
                    AddressCity = c.AddressUser.Address.City
                });

                return Ok(contratsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("contract/cancel")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CancelContractByUser(int contratServiceId)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);
                var user = await _serviceUser.GetById(userId);

                if (user == null)
                    return BadRequest("The user does not exist.");

                var contratService = await _serviceContrat.GetById(contratServiceId);

                if (contratService == null || contratService.User.Id != userId)
                    return BadRequest("The contract doesn't exist or the user is not the owner.");

                var result = await _serviceContrat.CancelContractByUser(userId, contratServiceId);

                if (result)
                {
                    return Ok(new { message = "The contract was cancelled." });
                }
                else
                {
                    return BadRequest("The contract could not be cancelled.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("contract/finish")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> FinishContratByUser(int contratServiceId)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);

                var result = await _serviceContrat.FinishContractByUser(userId, contratServiceId);

                if (result)
                {
                    return Ok(new { message = "The contract was finished." });
                }
                else
                {
                    return BadRequest("The contract could not be finished.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("rate")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> RateProviderByUser(int contratServiceId, int calificationValue, string comment)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);
                var result = await _serviceContrat.RateProviderByUser(userId, contratServiceId, calificationValue, comment);

                if (result)
                {
                    return Ok(new { message = "The provider was rated successfully." });
                }
                else
                {
                    return BadRequest("The provider could not be rated.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

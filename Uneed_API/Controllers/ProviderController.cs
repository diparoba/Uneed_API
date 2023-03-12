using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uneed_API.DTO;
using Uneed_API.Models;
using Uneed_API.Services;
using IServiceProvider = Uneed_API.Services.IServiceProvider;

namespace Uneed_API.Controllers
{
    [EnableCors("All")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceContrat _serviceContrat;
        private readonly IServiceUser _serviceUser;
        public ProviderController(IServiceProvider serviceProvider, IServiceUser serviceUser, IServiceContrat serviceContrat)
        {
            _serviceProvider = serviceProvider;
            _serviceContrat = serviceContrat;
            _serviceUser = serviceUser;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {
            return Ok(await _serviceProvider.GetAll());
        }
        [HttpGet("contracts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<ContratResponse>>> GetContratsByProviderId()
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);
                var user = await _serviceUser.GetById(userId);

                if (user == null || !user.IsProvider.HasValue || !user.IsProvider.Value)
                    return BadRequest("The user is not a provider.");

                var providerId = user.Provider.FirstOrDefault()?.Id;

                if (!providerId.HasValue)
                    return BadRequest("The provider doesn't have any contract.");

                var contrats = await _serviceContrat.GetContratsByProviderId(providerId.Value);

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
        /*[HttpPost("contract/accept")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> AcceptContractByProvider(ContratAcceptResponse contratAcceptResponse)
        {
            try
            {
                var userId = AuthHelper.GetUserId(HttpContext);
                var user = await _serviceUser.GetById(userId);

                if (user == null || !user.IsProvider.HasValue || !user.IsProvider.Value)
                    return BadRequest("The user is not a provider.");

                var contratService = await _serviceContrat.GetById(contratAcceptResponse.ContratServiceId);

                if (contratService == null || contratService.Provider.Id != user.Provider.FirstOrDefault()?.Id)
                    return BadRequest("The contract doesn't exist or the user is not the provider.");

                var result = await _serviceContrat.AcceptContractByProvider(user.Provider.FirstOrDefault()?.Id ?? 0, contratAcceptResponse.ContratServiceId);

                if (result)
                {
                    return Ok(new { message = "The contract was accepted." });
                }
                else
                {
                    return BadRequest("The contract could not be accepted.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/



    }
}
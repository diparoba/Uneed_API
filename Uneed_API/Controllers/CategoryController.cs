using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Uneed_API.Models;
using Uneed_API.Services;

namespace Uneed_API.Controllers
{
    [EnableCors("All")]
    [Route("api/[Controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
    private readonly IServiceCategory _serviceCategory;

    public CategoryController(IServiceCategory serviceCategory)
        {
        _serviceCategory = serviceCategory;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get()
        {

            return Ok(await _serviceCategory.GetCategories());
        }
    }
}

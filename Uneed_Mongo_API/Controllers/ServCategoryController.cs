using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Uneed_Mongo_API.Models;
using Uneed_Mongo_API.Services;

namespace Uneed_Mongo_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ServCategoryController : Controller
    {
      private ICategoryCollection db = new CategoryCollection();
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await db.GetAllCategories());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            return Ok(await db.GetCategoryById(id));
        }
        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] ServCategory servCategory)
        {
            if(servCategory == null)
            {
                return BadRequest();
            }
            if(servCategory.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The category shouldn't be empty");
            }
            await db.InsertCategory(servCategory);
            return Created("Created", true);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory([FromBody] ServCategory servCategory, string id)
        {
            if (servCategory == null)
            {
                return BadRequest();
            }
            if (servCategory.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The category shouldn't be empty");
            }
            servCategory.Id = new MongoDB.Bson.ObjectId(id);
            await db.UpdateCategory(servCategory);
            return Created("Created", true);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await db.DeleteCategory(id);
            return NoContent();
        }
    }
}

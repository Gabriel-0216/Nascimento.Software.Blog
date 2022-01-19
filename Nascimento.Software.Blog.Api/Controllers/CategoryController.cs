using Domain.Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IRepository<Category> _categoryRepo;
        public CategoryController(IRepository<Category> _categoryRepo)
        {
            this._categoryRepo = _categoryRepo;
        }
        [HttpGet]
        [Route("get-categories")]
        public async Task<ActionResult> GetCategoriesAsync()
        {
            return Ok(await _categoryRepo.GetAllAsync());
        }
        [HttpPost]
        [Route("Insert-category")]
        public async Task<ActionResult> InsertCategory([FromBody] Category category)
        {
            var inserted = await _categoryRepo.Create(category);
            if (inserted) return Ok();

            return BadRequest();
        }
    }
}

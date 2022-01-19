using Domain.Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IRepository<Tag> _tagRepo;

        public TagsController(IRepository<Tag> tagRepo, IRepository<Role> roleReo)
        {
            _tagRepo = tagRepo;
        }
        [HttpGet]
        [Route("get-all-tags")]
        public async Task<ActionResult> GetTagsAsync()
        {
            return Ok(await _tagRepo.GetAllAsync());
        }
        [HttpPost]
        [Route("insert-tag")]
        public async Task<ActionResult> InsertTag([FromBody] Tag tag)
        {
            return Ok(await _tagRepo.Create(tag));
        }


    }
}

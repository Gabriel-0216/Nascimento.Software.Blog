using Infra.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;

        /*ToDo: 
         
         */
        public PostsController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }
        [HttpGet]
        [Route("get-posts")]
        public async Task<ActionResult> GetPostAsync()
        {
            return Ok(await _postRepo.GetPostWithAuthorCategory());
        }
        [HttpGet]
        [Route("get-posts-with-tags")]
        public async Task<ActionResult> GetPostsWithTags()
        {
            return Ok(await _postRepo.GetPostWithTags());
        }
        [HttpGet]
        [Route("get-posts-by-category")]
        public async Task<ActionResult> GetPostsByCategoryIdAsync([FromHeader] int Id)
        {
            return Ok(await _postRepo.GetPostsByCategoryId(Id));
        }
    }
}

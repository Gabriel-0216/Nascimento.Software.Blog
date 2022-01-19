using Domain.Domain;
using Infra;
using Infra.Posts;
using Microsoft.AspNetCore.Mvc;
using Nascimento.Software.Blog.Api.Dto;

namespace Nascimento.Software.Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;
        private readonly IRepository<PostTag> _postTagRepo;
        public PostsController(IPostRepository postRepo, IRepository<PostTag> postTagRepo)
        {
            _postTagRepo = postTagRepo;
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
        [HttpGet]
        [Route("get-posts-complete")]
        public async Task<ActionResult> GetPostsComplete()
        {
            return Ok(await _postRepo.GetPostsComplete());
        }
        [HttpPost]
        [Route("insert-posts")]
        public async Task<ActionResult> InsertPosts(PostDto post)
        {
            var postModel = new Post()
            {
                Slug = post.Slug,
                Summary = post.Summary,
                AuthorId = post.AuthorId,
                Body = post.Body,
                CategoryId = post.CategoryId,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                Title = post.Title,
            };
            var insertPost = await _postRepo.CreatePost(postModel);
            foreach (var item in post.Tags)
            {
                var postTag = new PostTag()
                {
                    PostId = insertPost,
                    TagId = item.TagId,
                };
                var insertTags = await _postTagRepo.Create(postTag);
            }
            return Ok();
        }

    }
}

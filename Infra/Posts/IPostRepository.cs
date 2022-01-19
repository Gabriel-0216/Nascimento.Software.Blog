using Domain.Domain;
namespace Infra.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<int> CreatePost(Post post);
        Task<IEnumerable<Post>> GetPostWithTags();
        Task<IEnumerable<Post>> GetPostWithAuthorCategory();
        Task<IEnumerable<Post>> GetPostsByCategoryId(int id);
        Task<IEnumerable<Post>> GetPostsComplete();
    }
}

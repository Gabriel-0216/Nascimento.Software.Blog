using Domain.Domain;
namespace Infra.Posts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostWithTags();
        Task<IEnumerable<Post>> GetPostWithAuthorCategory();
        Task<IEnumerable<Post>> GetPostsByCategoryId(int id);
    }
}

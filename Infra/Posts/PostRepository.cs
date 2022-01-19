using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra.Posts
{
    public class PostRepository : IPostRepository
    {
        private readonly SqlConnection _conn;
        public PostRepository()
        {
            _conn = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(Post entity) => await _conn.InsertAsync(entity) > 0;

        public async Task<IEnumerable<Post>> GetAllAsync() => await _conn.GetAllAsync<Post>();

        public async Task<Post?> GetAsync(int Id) => await _conn.GetAsync<Post>(Id);

        public string GetConnection() => Settings.ConnectionString;

        public async Task<IEnumerable<Post>> GetPostsByCategoryId(int id)
        {
            var query = @"SELECT * FROM Post
                    INNER JOIN [User] ON [Post].AuthorId = [User].[ID] 
                    INNER JOIN [Category] ON [Category].Id = [Post].[CategoryId]
                    WHERE CategoryId = @id";
            var param = new DynamicParameters();
            param.Add("Id", id);
            var items = await _conn.QueryAsync<Post, User, Category, Post>(query, (post, user, category) =>
            {
                post.Category = category;
                post.Author = user;
                return post;
            }, splitOn: "Id", param: param);
            return items;

        }

        public async Task<IEnumerable<Post>> GetPostWithAuthorCategory()
        {
            var query = @"SELECT * FROM Post
                    INNER JOIN [User] ON [Post].AuthorId = [User].[ID] 
                    INNER JOIN [Category] ON [Category].Id = [Post].[CategoryId]";

            var items = await _conn.QueryAsync<Post, User, Category, Post>(query, (post, user, category) =>
            {
                post.Category = category;
                post.Author = user;
                return post;
            }, splitOn: "Id");
            return items;
        }
        public async Task<IEnumerable<Post>> GetPostsComplete()
        {
            var query = @"Select [Post].*, [Category].*, [User].*, [Tag].*
                                    FROM 
                                    [Post]
                    LEFT JOIN [Category] on [Category].Id = [Post].[CategoryId]
                    LEFT JOIN [User] on [User].[ID] = [Post].[AuthorId]
                    Left JOIN [PostTag] on [PostTag].PostId = [Post].[Id]
                    LEFT JOIN [Tag] ON [PostTag].TagId = [Tag].[Id]";
            var posts = new List<Post>();
            var items = await _conn.QueryAsync<Post, Category, User, Tag, Post>(query, (post, category, user, tag) =>
            {
                var pst = posts.FirstOrDefault(p => p.Id == post.Id);
                if (pst == null)
                {
                    pst = post;
                    if (tag != null)
                    {
                        pst.Tags.Add(tag);
                    }
                    posts.Add(pst);
                    pst.Category = category;
                    pst.Author = user;
                }
                else
                {
                    pst.Tags.Add(tag);
                }
                return post;
            }, splitOn: "Id");
            return posts;
        }
        public async Task<IEnumerable<Post>> GetPostWithTags()
        {
            var query = @"Select [Post].*, [Tag].*
                                    FROM 
                                    [Post]
                    Left JOIN [PostTag] on [PostTag].PostId = [Post].[Id]
                    LEFT JOIN [Tag] ON [PostTag].TagId = [Tag].[Id]";

            var posts = new List<Post>();
            var items = await _conn.QueryAsync<Post, Tag, Post>(query, (post, tag) =>
            {
                var pst = posts.FirstOrDefault(p => p.Id == post.Id);
                if (pst == null)
                {
                    pst = post;
                    if (tag != null)
                    {
                        pst.Tags.Add(tag);
                    }
                    posts.Add(pst);
                }
                else
                {
                    pst.Tags.Add(tag);
                }
                return post;
            }, splitOn: "Id");

            return posts;
        }

        public async Task<bool> Remove(Post entity) => await _conn.DeleteAsync<Post>(entity);

        public async Task<bool> Update(Post entity) => await _conn.UpdateAsync<Post>(entity);

        public async Task<int> CreatePost(Post post)
        {
            var idInserted = await _conn.InsertAsync<Post>(post);
            return idInserted;
        }
    }
}

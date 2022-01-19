using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;

namespace Infra
{
    public class PostTagRepository : IRepository<PostTag>
    {
        private readonly SqlConnection _connection;
        public PostTagRepository()
        {
            _connection = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(PostTag entity) => await _connection.InsertAsync<PostTag>(entity) > 0;
        public async Task<IEnumerable<PostTag>> GetAllAsync() => await _connection.GetAllAsync<PostTag>();
        public async Task<PostTag?> GetAsync(int Id) => await _connection.GetAsync<PostTag>(Id);
        public string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Remove(PostTag entity) => await _connection.DeleteAsync(entity);
        public async Task<bool> Update(PostTag entity) => await _connection.UpdateAsync(entity);
    }
}

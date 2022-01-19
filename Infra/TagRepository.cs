using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra
{
    public class TagRepository : IRepository<Tag>
    {
        private readonly SqlConnection _connection;
        public TagRepository()
        {
            _connection = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(Tag entity) => await _connection.InsertAsync<Tag>(entity) > 0;

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _connection.GetAllAsync<Tag>();

        public async Task<Tag?> GetAsync(int Id) => await _connection.GetAsync<Tag>(Id);

        public string GetConnection() => Settings.ConnectionString;

        public async Task<bool> Remove(Tag entity) => await _connection.DeleteAsync(entity);

        public async Task<bool> Update(Tag entity) => await _connection.UpdateAsync(entity);
    }
}

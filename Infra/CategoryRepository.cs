using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly SqlConnection _connection;
        public CategoryRepository()
        {
            _connection = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(Category entity) => await _connection.InsertAsync<Category>(entity) > 0;
        public async Task<IEnumerable<Category>> GetAllAsync() => await _connection.GetAllAsync<Category>();
        public async Task<Category?> GetAsync(int Id) => await _connection.GetAsync<Category>(Id);
        public string GetConnection() => Settings.ConnectionString;
        public async Task<bool> Remove(Category entity) => await _connection.DeleteAsync(entity);
        public async Task<bool> Update(Category entity) => await _connection.UpdateAsync(entity);
    }
}

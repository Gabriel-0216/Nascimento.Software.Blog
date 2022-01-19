using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        private readonly SqlConnection _connection;
        public UserRoleRepository()
        {
            _connection = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(UserRole entity) => await _connection.InsertAsync<UserRole>(entity) > 0;

        public async Task<IEnumerable<UserRole>> GetAllAsync() => await _connection.GetAllAsync<UserRole>();

        public async Task<UserRole?> GetAsync(int Id) => await _connection.GetAsync<UserRole>(Id);

        public string GetConnection() => Settings.ConnectionString;

        public async Task<bool> Remove(UserRole entity) => await _connection.DeleteAsync(entity);

        public async Task<bool> Update(UserRole entity) => await _connection.UpdateAsync(entity);
    }
}

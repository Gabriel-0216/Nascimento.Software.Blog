using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly SqlConnection _connection;
        public RoleRepository()
        {
            _connection = new SqlConnection(GetConnection());
        }
        public async Task<bool> Create(Role entity) => await _connection.InsertAsync<Role>(entity) >= 0;

        public async Task<IEnumerable<Role>> GetAllAsync() => await _connection.GetAllAsync<Role>();

        public async Task<Role?> GetAsync(int Id) => await _connection.GetAsync<Role>(Id);

        public string GetConnection() => Settings.ConnectionString;

        public async Task<bool> Remove(Role entity) => await _connection.DeleteAsync(entity);

        public async Task<bool> Update(Role entity) => await _connection.UpdateAsync(entity);
    }
}

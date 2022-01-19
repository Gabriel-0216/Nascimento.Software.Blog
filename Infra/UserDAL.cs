using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Domain;
using System.Data.SqlClient;
namespace Infra
{
    public class UserDAL : IUserDAL
    {
        private readonly SqlConnection _connection;
        public UserDAL() => _connection = new SqlConnection(Settings.ConnectionString);
        public async Task<bool> CreateUser(User user) => await _connection.InsertAsync<User>(user) > 0;
        public async Task<User> GetUserAsync(int id) => await _connection.GetAsync<User>(id);
        public async Task<IEnumerable<User>> GetUsersAsync() => await _connection.GetAllAsync<User>();
        public async Task<bool> RemoveUser(User user) => await _connection.DeleteAsync<User>(user);
        public async Task<bool> UpdateUser(User user) => await _connection.UpdateAsync<User>(user);
        public async Task<IEnumerable<User>> GetUsersWithRoles()
        {
            var query = @"SELECT [User].*, [Role].*
                            FROM
                        [User]
                        LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                        left join [Role] ON [UserRole].[RoleId] = [Role].[Id]";


            var users = new List<User>();

            var items = _connection.Query<User, Role, User>(query, (user, role) =>
             {
                 var usr = users.FirstOrDefault(p => p.Id == user.Id);
                 if (usr == null)
                 {
                     usr = user;
                     if (role != null)
                     {
                         usr.Roles.Add(role);
                     }
                     users.Add(usr);
                 }
                 else
                 {
                     usr.Roles.Add(role);
                 }
                 return user;
             }, splitOn: "Id");


            return users;
        }
    }
}
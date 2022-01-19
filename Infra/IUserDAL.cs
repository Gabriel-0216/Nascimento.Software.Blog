using Domain.Domain;
namespace Infra
{
    public interface IUserDAL
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<bool> CreateUser(User user);
        Task<bool> RemoveUser(User user);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<User>> GetUsersWithRoles();
    }
}

using Dapper.Contrib.Extensions;
namespace Domain.Domain
{
    [Table("[UserRole]")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}

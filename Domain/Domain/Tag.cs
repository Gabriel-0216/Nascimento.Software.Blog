using Dapper.Contrib.Extensions;

namespace Domain.Domain
{
    [Table("[Tag]")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;

    }
}

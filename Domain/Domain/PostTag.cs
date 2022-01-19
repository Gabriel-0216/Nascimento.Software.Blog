using Dapper.Contrib.Extensions;

namespace Domain.Domain
{
    [Table("PostTag")]
    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}

using Dapper.Contrib.Extensions;
namespace Domain.Domain
{
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Write(false)]
        public Category? Category { get; set; }
        public int AuthorId { get; set; }
        [Write(false)]
        public User? Author { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        public List<Tag> Tags { get; set; }
    }
}

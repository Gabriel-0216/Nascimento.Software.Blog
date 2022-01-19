namespace Nascimento.Software.Blog.Api.Dto
{
    public class PostDto
    {
        public PostDto()
        {
            Tags = new List<TagDto>();
        }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public List<TagDto> Tags { get; set; }
    }
    public class TagDto
    {
        public int TagId { get; set; }
    }
}

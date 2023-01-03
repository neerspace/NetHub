namespace NetHub.Application.Models.Mezha;

public class PostDto
{
    public long Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public RenderedModel Guid { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Link { get; set; } = default!;
    public RenderedModel Title { get; set; } = default!;
    public RenderedModel Content { get; set; } = default!;
    public RenderedModel Excerpt { get; set; } = default!;
    public int Author { get; set; }
    public int FeaturedMedia { get; set; }
    public string CommentStatus { get; set; }
    public IEnumerable<string> Categories { get; set; }
    public IEnumerable<string> Tags { get; set; }
    public string FeaturedMediaUrl { get; set; }
    public bool SharingEnabled { get; set; }
}
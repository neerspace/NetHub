using System.Text.Json.Serialization;

namespace NetHub.Application.Models.Mezha;

public class PostModel
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

    [JsonPropertyName("featured_media")]
    public int FeaturedMedia { get; set; }

    [JsonPropertyName("comment_status")]
    public string CommentStatus { get; set; } = default!;

    public IEnumerable<int> Categories { get; set; } = null!;
    public IEnumerable<int> Tags { get; set; } = null!;

    [JsonPropertyName("jetpack_featured_media_url")]
    public string FeaturedMediaUrl { get; set; } = default!;

    [JsonPropertyName("jetpack_sharing_enabled")]
    public bool SharingEnabled { get; set; }
}

public record RenderedModel(string Rendered, bool? Protected);
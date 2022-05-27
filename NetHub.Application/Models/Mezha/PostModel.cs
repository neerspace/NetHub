using Newtonsoft.Json;

namespace NetHub.Application.Models.Mezha;

public class PostModel
{
	public long Id { get; set; }
	public DateTime Date { get; set; }
	public RenderedModel Guid { get; set; } = default!;
	public string Slug { get; set; } = default!;
	public string Status { get; set; } = default!;
	public string Type { get; set; } = default!;
	public string Link { get; set; } = default!;
	public RenderedModel Title { get; set; } = default!;
	public RenderedModel Content { get; set; } = default!;
	public RenderedModel Excerpt { get; set; } = default!;
	public int Author { get; set; }

	[JsonProperty(PropertyName = "featured_media")]
	public int FeaturedMedia { get; set; }

	[JsonProperty(PropertyName = "comment_status")]
	public string CommentStatus { get; set; }

	public IEnumerable<int> Categories { get; set; }
	public IEnumerable<int> Tags { get; set; }

	[JsonProperty(PropertyName = "jetpack_featured_media_url")]
	public string FeaturedMediaUrl { get; set; }
	
	[JsonProperty(PropertyName = "jetpack_sharing_enabled")]
	public bool SharingEnabled { get; set; }
	
}

public record RenderedModel(string Rendered, bool? Protected);
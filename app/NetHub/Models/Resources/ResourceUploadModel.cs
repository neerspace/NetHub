using Microsoft.AspNetCore.Http;

namespace NetHub.Models.Resources;

public sealed class ResourceUploadModel
{
    public required ResourceUploadItemModel[] Resources { get; set; }
}

public sealed class ResourceUploadItemModel
{
    public required IFormFile File { get; set; }
public required string Link { get; set; }
}

public enum ResourceUploadTarget
{
    ProfilePicture,
    Article
}